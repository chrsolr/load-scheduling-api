public class UseTraceIdMiddleware
{
    private readonly RequestDelegate _next;

    public UseTraceIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string traceId = Guid.NewGuid().ToString();

        context.Response.Headers.Append("LS-Trace-Id", traceId);

        await _next(context);
    }
}

public static class UseTraceIdMiddlewareExtensions
{
    public static IApplicationBuilder UseTraceIdMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseTraceIdMiddleware>();
    }
}
