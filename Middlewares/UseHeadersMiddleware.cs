public class UseHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public UseHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");

        context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
        context.Response.Headers["Pragma"] = "no-cache";
        context.Response.Headers["Expires"] = "0";

        await _next(context);
    }
}

public static class UseHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseHeadersMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<UseHeadersMiddleware>();
    }
}
