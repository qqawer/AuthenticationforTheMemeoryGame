using System.Diagnostics; // For timing 
namespace AuthenticationforTheMemeoryGame.Middlewares
{
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
           
            // 1. When requesting to enter (before entering the Controller) 
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation($"[Incoming] {context.Request.Method} {context.Request.Path}");

            
            // 2. Call the next middleware (hand over control to Auth, Controller, etc.) 
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // If there is an error in the subsequent process, it can also be captured and recorded here.
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw; // Continue to throw the exception, do not swallow it
            }

             
            // 3. When the request processing is completed (after the Controller has executed) 
            stopwatch.Stop();
            var statusCode = context.Response.StatusCode;

            // Determine the log level based on the status code
            if (statusCode >= 400)
            {
               _logger.LogWarning($"[Finished] Status: {statusCode} | Time: {stopwatch.ElapsedMilliseconds}ms");
            }
            else
            {
                _logger.LogInformation($"[Finished] Status: {statusCode} | Time: {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}