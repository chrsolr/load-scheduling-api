public class UseRequestBodyLimitMiddleware
{
    private readonly RequestDelegate _next;

    public UseRequestBodyLimitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        const long maxRequestBodySize = 1024 * 1024 * 5; // 5MB

        if (context.Request.ContentLength > maxRequestBodySize)
        {
            await context.Response.WriteAsync("Request payload too large");
            return;
        }

        await _next(context);
    }
}

public static class UseRequestBodyLimitMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestBodyLimitMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<UseRequestBodyLimitMiddleware>();
    }
}
