using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Models.Reponses;
using SuperHeroAPI_DotNet6.Repositories.Implementations;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Implementations;
using SuperHeroAPI_DotNet6.Services.Interfaces;
using SuperHeroAPI_DotNet6.Validators;
using System;

var builder = WebApplication.CreateBuilder(args);

// ==================== SERVICE REGISTRATION (ALL BEFORE Build!) ====================

// Add controllers and configure validation error response
// First: Add controllers (required for Web API)
builder.Services.AddControllers();

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

// Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// FluentValidatoin
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

// Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// DI
builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();
builder.Services.AddScoped<ISuperheroService, SuperheroService>();

// Auth DI
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// AutoMapper
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

app.UseAuthorization();

app.MapControllers();

app.Run();