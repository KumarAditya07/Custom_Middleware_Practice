namespace Custom_Middleware_Practice.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;


        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Pass request to next middleware
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}"); // Log the error
                context.Response.StatusCode = 500; // Internal Server Error
                context.Response.ContentType = "application/json";

                var errorResponse = new { message = "Internal Server Error. Please try again later." };
                await context.Response.WriteAsJsonAsync(errorResponse);
            }

        }

    }
}
