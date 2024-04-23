using System.Net;
using System.Text.Json;
using ReadingIsGood.Models.ResponseModels;

namespace ReadingIsGood.Helper;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ResponseModel<string?>(JsonSerializer.Serialize(exception.Data), exception.Message);

        if (exception is ApplicationException)
            response.StatusCode = (int)HttpStatusCode.BadRequest;
        else
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

        _logger.LogError(exception, "Unhandled exception occurred.");
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}