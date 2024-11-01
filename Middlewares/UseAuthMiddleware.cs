public class UseAuthMiddleware
{
    private readonly RequestDelegate _next;

    public UseAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue(
            "Authorization",
            out var authorizationHeader
        );

        if (String.IsNullOrEmpty(authorizationHeader))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(
                "Authorization header is missing"
            );
            return;
        }

        var parts = authorizationHeader.ToString().Split(' ');

        var tokenType = parts[0];

        if (parts.Length == 1 && tokenType != "Bearer")
        {
            var inlineApiKey = Environment.GetEnvironmentVariable(
                "INLINE_API_KEY"
            );

            if (tokenType != inlineApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(
                    "Invalid Authorization Token"
                );
                return;
            }

            await _next(context);
            return;
        }

        if (parts.Length != 2 || parts[0] != "Bearer")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid Authorization header");
            return;
        }

        var accessToken = parts[1];
        var token = JwtUtils.ConvertToSecurityToken(accessToken);
        var decodedToken = JwtUtils.DecodeToken(token);

        if (decodedToken == null)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid Authorization Token");
            return;
        }

        foreach (var org in decodedToken.Organizations)
        {
            Console.WriteLine($"Organization: {org}");
        }

        // Console.WriteLine(JsonSerializer.Serialize(decodedToken));

        await _next(context);
    }
}

public static class UseAuthMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<UseAuthMiddleware>();
    }
}
