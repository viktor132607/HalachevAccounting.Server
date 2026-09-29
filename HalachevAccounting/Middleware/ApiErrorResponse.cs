namespace HalachevAccounting.Api.Middleware;

public sealed record ApiErrorResponse(
    int StatusCode,
    string Code,
    string Message,
    string TraceId,
    string? Details = null);

public static class ApiErrorResponseWriter
{
    public static Task WriteAsync(
        HttpContext context,
        int statusCode,
        string code,
        string message,
        string? details = null)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(new ApiErrorResponse(
            statusCode,
            code,
            message,
            context.TraceIdentifier,
            details));
    }
}
