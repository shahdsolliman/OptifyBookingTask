using OptifyBookingTask.API.Controllers.Errors;
using OptifyBookingTask.Application.Exceptions;
using System.Net;

namespace OptifyBookingTask.API.Middlewares
{
    // Convention-based class for custom middleware
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
                // Execute next middleware or the controller itself
                await _next(httpContext);



            }
            catch (Exception ex)
            {
                #region Logging
                if (_env.IsDevelopment())
                {
                    _logger.LogError(ex, ex.Message);
                }
                else
                {
                    // TODO: log to external service or file
                }
                #endregion

                var response = HandleExceptionsAsync(httpContext, ex);

                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(response.ToString());
            }
        }

        private static ApiResponse HandleExceptionsAsync(HttpContext httpContext, Exception ex)
        {
            ApiResponse response;

            switch (ex)
            {
                case NotFoundException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new ApiResponse((int)HttpStatusCode.NotFound, ex.Message);
                    break;

                case BadRequestException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new ApiResponse((int)HttpStatusCode.BadRequest, ex.Message);
                    break;

                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    response = new ApiExceptionResponse(
                        (int)HttpStatusCode.InternalServerError,
                        ex.Message,
                        ex.StackTrace ?? "No stack trace available."
                    );
                    break;
            }

            return response;
        }
    }
}
