global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var builder = WebApplication.CreateBuilder(args);

var Version =
    Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "N/A";

builder.Configuration.AddEnvironmentVariables();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 5 * 1024 * 1024; // 5mb
});

builder.Services.AddDbContextPool<DataContext>(options =>
    options.UseNpgsql(
        Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
    )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Load Scheduling API",
            Version = $"v{Version}",
        }
    );
});

builder.Services.AddResponseCompression(options =>
    options.EnableForHttps = true
);

builder.Services.AddScoped<IOrgConfigService, OrgConfigService>();

var app = builder.Build();

bool.TryParse(
    Environment.GetEnvironmentVariable("SHOW_SWAGGER"),
    out bool showApiDocumentation
);

if (showApiDocumentation)
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.DocumentTitle = "Load Scheduling API";
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            $"Load Scheduling API v{Version}"
        );
    });
}

app.UseResponseCompression();
app.UseAuthMiddleware();
app.UseHeadersMiddleware();
app.UseRequestBodyLimitMiddleware();
app.UseCors();

app.UseTraceIdMiddleware();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
