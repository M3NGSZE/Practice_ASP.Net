using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SuperHeroAPI_DotNet6.Data;
using SuperHeroAPI_DotNet6.Middlewares;
using SuperHeroAPI_DotNet6.Repositories.Implementations;
using SuperHeroAPI_DotNet6.Repositories.Interfaces;
using SuperHeroAPI_DotNet6.Services.Implementations;
using SuperHeroAPI_DotNet6.Services.Interfaces;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();


// Middleware Exception Handler
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // Customize invalid model state response
        options.InvalidModelStateResponseFactory = context =>
        {
            var problemDetails = new ProblemDetails
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Instance = context.HttpContext.Request.Path,
                Detail = "See errors for details."
            };

            // Add all validation errors as extensions (or custom structure)
            problemDetails.Extensions["errors"] = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return new BadRequestObjectResult(problemDetails)
            {
                ContentTypes = { "application/json" }
            };
        };
    });

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
