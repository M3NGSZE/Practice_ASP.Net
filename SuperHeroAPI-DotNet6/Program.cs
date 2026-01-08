using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI_DotNet6.Auth;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Reponses;
using SuperHeroAPI_DotNet6.Repositories.Implementations;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Implementations;
using SuperHeroAPI_DotNet6.Services.Interfaces;
using SuperHeroAPI_DotNet6.Validators;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================== SERVICE REGISTRATION (ALL BEFORE Build!) ====================

// 1. Controllers
// Add controllers and configure validation error response
// First: Add controllers (required for Web API)
builder.Services.AddControllers();

// 2. Authentication (JWT)
// Configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true,
        };

        // ?? BOTH 401 and 403 HANDLED HERE
        options.Events = new JwtBearerEvents
        {
            // 401 – unauthenticated / invalid token
            OnChallenge = context =>
            {
                context.HandleResponse();

                var response = new ApiErrorResponse
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Type = "Unauthorized",
                    Title = "Authentication failed",
                    Message = "You must be authenticated to access this resource.",
                    Errors = null,
                    Path = context.HttpContext.Request.Path
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                return context.Response.WriteAsJsonAsync(response);
            },

            // 403 – authenticated but forbidden
            OnForbidden = context =>
            {
                var response = new ApiErrorResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    Type = "Forbidden",
                    Title = "Access denied",
                    Message = "You do not have permission to access this resource.",
                    Errors = null,
                    Path = context.HttpContext.Request.Path
                };

                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                return context.Response.WriteAsJsonAsync(response);
            }
        };
    });

// 3. Authorization
builder.Services.AddAuthorization();

// 4. Swagger 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Add Security Definition
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter JWT like this: Bearer {your token}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// 5. FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();


// Second: Customize validation error response (optional but recommended)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value!.Errors.Count > 0)
            .ToDictionary(
                x => x.Key,
                x => x.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        var response = new ApiErrorResponse
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationError",
            Title = "Validation failed",
            Message = "One or more validation errors occurred",
            Errors = errors,
            Path = context.HttpContext.Request.Path
        };

        return new BadRequestObjectResult(response);
    };
});


// 6. Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 7. Dependency Injection

// DI Jwt
builder.Services.AddScoped<JwtService>();


// DI
builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();
builder.Services.AddScoped<ISuperheroService, SuperheroService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// Auth DI
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// 8. AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// ==================== BUILD THE APP ====================
var app = builder.Build();

// ==================== MIDDLEWARE PIPELINE (AFTER Build!) ====================

// Custom global exception handler middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();