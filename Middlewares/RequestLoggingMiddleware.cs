using System.Diagnostics;

namespace AuthenticationforTheMemeoryGame.Middlewares
{
    /// <summary>
    /// Middleware to log details of incoming requests and outgoing responses, including execution time.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // 1. Start the timer to measure request processing duration
            var stopwatch = Stopwatch.StartNew();

            // 2. Log the incoming request method and path
            _logger.LogInformation("[Incoming] {Method} {Path}", context.Request.Method, context.Request.Path);

            // 3. Call the next middleware in the pipeline
            // Note: We do not use try-catch here. Exceptions are allowed to bubble up 
            // to the GlobalExceptionMiddleware, which will handle them and set the 500 status code.
            await _next(context);

            // 4. Stop the timer after the pipeline returns
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var statusCode = context.Response.StatusCode;

            // 5. Log the completion status with appropriate log levels
            if (statusCode >= 500)
            {
                // Log as Error for server-side issues
                _logger.LogError("[Failed] Status: {StatusCode} | Time: {Elapsed}ms", statusCode, elapsedMs);
            }
            else if (statusCode >= 400)
            {
                // Log as Warning for client-side errors (e.g., 400 Bad Request, 401 Unauthorized)
                _logger.LogWarning("[Client Error] Status: {StatusCode} | Time: {Elapsed}ms", statusCode, elapsedMs);
            }
            else
            {
                // Log as Information for successful requests
                _logger.LogInformation("[Finished] Status: {StatusCode} | Time: {Elapsed}ms", statusCode, elapsedMs);
            }
        }
    }
}