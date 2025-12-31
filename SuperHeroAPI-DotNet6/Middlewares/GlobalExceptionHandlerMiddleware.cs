using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace SuperHeroAPI_DotNet6.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = ex switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var problem = new ProblemDetails
            {
                Title = statusCode == 500 ? "An unexpected error occurred" : "An error occurred",
                Detail = statusCode == 500 ? "Internal server error" : ex.Message, // Hide details in prod for 500
                Status = statusCode,
                Instance = context.Request.Path
            };

            // Optional: add errors dictionary for custom exceptions too
            if (ex is ValidationException validationEx)
            {
                problem.Extensions["errors"] = validationEx.Errors; // if you have a dict
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = problem.Status.Value;


            return context.Response.WriteAsJsonAsync(problem);
        }
    }
}
