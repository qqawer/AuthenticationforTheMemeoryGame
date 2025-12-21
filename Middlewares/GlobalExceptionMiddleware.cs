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
            // Set the response content type to JSON
            context.Response.ContentType = "application/json";

            // Set the status code to 500 Internal Server Error
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Construct the standard error response object
            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error occurred. Please try again later.",
                // In production, avoid exposing 'exception.Message' to prevent leaking sensitive info.
                // For development, it's useful for debugging.
                DetailedError = exception.Message
            };

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, jsonOptions);

            return context.Response.WriteAsync(json);
        }
    }
}