namespace HalachevAccounting.Api.Middleware;

public sealed class GlobalExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionHandlingMiddleware> logger,
    IWebHostEnvironment environment)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            if (context.Response.HasStarted)
            {
                logger.LogError(exception, "Unhandled exception after response started. TraceId: {TraceId}", context.TraceIdentifier);
                throw;
            }

            int statusCode = exception switch
            {
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ArgumentException => StatusCodes.Status400BadRequest,
                InvalidOperationException => StatusCodes.Status409Conflict,
                TimeoutException => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status500InternalServerError
            };

            string code = statusCode switch
            {
                400 => "ValidationError",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "NotFound",
                409 => "Conflict",
                429 => "RateLimitExceeded",
                503 => "DependencyUnavailable",
                _ => "UnexpectedError"
            };

            logger.LogError(
                exception,
                "Unhandled exception. Method: {Method}, Path: {Path}, StatusCode: {StatusCode}, TraceId: {TraceId}",
                context.Request.Method,
                context.Request.Path,
                statusCode,
                context.TraceIdentifier);

            await ApiErrorResponseWriter.WriteAsync(
                context,
                statusCode,
                code,
                statusCode == 500 ? "An unexpected error occurred." : "Request failed.",
                environment.IsDevelopment() ? exception.Message : null);
        }
    }
}
