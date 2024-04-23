public class RequestLoggingMiddleware
{
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var startTime = DateTime.UtcNow;

        _logger.LogInformation($"Request {context.Request.Method}: {context.Request.Path} received.");

        await _next(context);

        var endTime = DateTime.UtcNow;
        var duration = endTime - startTime;

        _logger.LogInformation(
            $"Response {context.Response.StatusCode} for {context.Request.Method}: {context.Request.Path} returned in {duration.TotalMilliseconds} ms.");
    }
}