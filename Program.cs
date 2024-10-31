global using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "N/A";

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 5 * 1024 * 1024; // 5mb
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "Load Scheduling API", Version = $"v{Version}" }
    );
});

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.DocumentTitle = "Load Scheduling API";
        config.RoutePrefix = string.Empty;
        config.SwaggerEndpoint("/swagger/v1/swagger.json", $"Load Scheduling API v{Version}");
    });
}

app.UseResponseCompression();
app.UseHeadersMiddleware();
app.UseRequestBodyLimitMiddleware();
app.UseCors();

app.UseTraceIdMiddleware();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
