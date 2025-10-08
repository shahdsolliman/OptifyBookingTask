using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OptifyBookingTask.API.Controllers.Errors;
using OptifyBookingTask.Application.Exceptions;
using System.Net;

namespace OptifyBookingTask.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // Logging
                if (_env.IsDevelopment())
                    _logger.LogError(ex, ex.Message);
                else
                    _logger.LogError("An unexpected error occurred.");

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            ApiResponse response;
            int statusCode;

            switch (ex)
            {
                case NotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiResponse(statusCode, ex.Message);
                    break;

                case BadRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse(statusCode, ex.Message);
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new ApiExceptionResponse(
                        statusCode,
                        _env.IsDevelopment() ? ex.Message : "Internal server error.",
                        _env.IsDevelopment() ? ex.StackTrace ?? "No stack trace available." : null
                    );
                    break;
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
