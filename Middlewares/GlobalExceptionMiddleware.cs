using System.Net;
using System.Text.Json;

namespace AuthenticationforTheMemeoryGame.Middlewares
{
    /// <summary>
    /// Middleware to catch unhandled exceptions globally and return a standardized JSON error response.
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Attempt to execute the rest of the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Catch any unhandled exception occurred in the pipeline
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

                // Handle the exception and generate a JSON response
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Ensure the content type is JSON
            context.Response.ContentType = "application/json";

            // Determine the HTTP status code based on the specific exception type
            switch (exception)
            {
                case ArgumentException:
                    // 400 Bad Request (e.g., missing parameters, empty username)
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case UnauthorizedAccessException:
                    // 401 Unauthorized (e.g., wrong password, invalid token)
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;

                default:
                    // 500 Internal Server Error (e.g., database connection failed, null pointer)
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            // Construct the response object to match the 'ApiResponse<T>' structure
            // This ensures the frontend receives a consistent format for both success and error.
            var response = new
            {
                Code = context.Response.StatusCode,

                // For 400/401, we want to show the specific message (e.g., "Password wrong").
                // For 500, you might want to hide the real message in production to prevent leaking sensitive info.
                Message = exception.Message,

                // Data is usually null for errors, or you can put StackTrace here for debugging in Development environment
                Data = (object)null
            };

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase // ensures "code", "message" in JSON
            };

            var json = JsonSerializer.Serialize(response, jsonOptions);

            return context.Response.WriteAsync(json);
        }
    }
}