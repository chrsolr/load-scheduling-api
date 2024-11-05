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

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Description =
                "Enter 'Bearer' [space] and then your token in the text input below.\nExample: \"Bearer abc123xyz\"",
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[]
                {
                    "eyJraWQiOiJncW5NYURUaUd3N3prOEx5YktYelI4ZzlNd2tnVks3QmlleXpPNnlZMUxnIiwiYWxnIjoiUlMyNTYifQ.eyJ2ZXIiOjEsImp0aSI6IkFULnZlV0FtTFJ3RUdDa1FteHRvMXBHUzJLTkNqU3pKMk5LVkRyWmdHVFNrRnMub2FyMmlqZjU1bm84QTdaN0c0eDciLCJpc3MiOiJodHRwczovL2lubm93YXR0c2N1c3RvbWVyLm9rdGEuY29tL29hdXRoMi9kZWZhdWx0IiwiYXVkIjoiYXBpOi8vZGVmYXVsdCIsInN1YiI6ImNocmlzdGlhbitub25wcm9kQGlubm93YXR0cy5jb20iLCJpYXQiOjE3MzAzOTQ3MjMsImV4cCI6MTczMDM5ODMyMywiY2lkIjoiMG9hajBva2hkUEVobXdPWTA0eDYiLCJ1aWQiOiIwMHU1eDJscXdtRlZJVXVJRzR4NyIsInNjcCI6WyJvZmZsaW5lX2FjY2VzcyIsInByb2ZpbGUiLCJlbWFpbCIsIm9wZW5pZCJdLCJhdXRoX3RpbWUiOjE3MzAzOTQ1NzgsInRvb2xQb2xpY2llcyI6WyJhbGVydHNNYW5hZ2VyOmVkaXRvciIsImFsZXJ0c01hbmFnZXI6cmVhZGVyIiwiZGFzaGJvYXJkc01hbmFnZXI6cmVhZGVyIiwiZGF0YUV4cGxvcmF0aW9uOnJlYWRlciIsImltcG9ydE1hbmFnZXI6ZWRpdG9yIiwiaW5zaWdodHNNYW5hZ2VyOmVkaXRvciIsImluc2lnaHRzTWFuYWdlcjpyZWFkZXIiLCJsb2FkU2NoZWR1bGluZzplZGl0b3IiLCJsb2FkU2NoZWR1bGluZzppYnQiLCJsb2FkU2NoZWR1bGluZzpyZWFkZXIiXSwib3JncyI6WyJvcmdfbm9ucHJvZCJdLCJwb2RUeXBlU3Vic2NyaXB0aW9ucyI6WyJhdHRyaWJ1dGVJbmZvOm1ldGVyIiwiY29tYmluZWRUcmFkZTpzZXR0bGVtZW50IiwibG9hZFNjaGVkdWxpbmc6ZGF5QWhlYWRCaWRzIiwibG9uZ1Rlcm1Gb3JlY2FzdDpibG9ja1R5cGUiLCJsb25nVGVybUZvcmVjYXN0OmN1c3RvbWVyVHlwZSIsImxvbmdUZXJtRm9yZWNhc3Q6bWV0ZXIiLCJsb25nVGVybUZvcmVjYXN0OnByb2R1Y3RUeXBlIiwibG9uZ1Rlcm1Gb3JlY2FzdDpwcm9maWxlIiwibG9uZ1Rlcm1Gb3JlY2FzdDpzZXR0bGVtZW50Iiwicmlza01hbmFnZW1lbnQ6ZXhwbG9kZWRUcmFkZXMiLCJyaXNrTWFuYWdlbWVudDpuZXRPcGVuUG9zaXRpb24iLCJyaXNrTWFuYWdlbWVudDpuZXRPcGVuUG9zaXRpb25MdGYiLCJyaXNrTWFuYWdlbWVudDpzdXBwbHlUcmFkZXMiLCJzaG9ydFRlcm1Gb3JlY2FzdDpjdXN0b21lclR5cGUiLCJzaG9ydFRlcm1Gb3JlY2FzdDpjdXN0b21lclR5cGVXZWF0aGVyIiwic2hvcnRUZXJtRm9yZWNhc3RHZW5lcmF0aW9uOmRheUFoZWFkTWV0ZXIiLCJzaG9ydFRlcm1Gb3JlY2FzdEdlbmVyYXRpb246ZGF5QWhlYWRTZXR0bGVtZW50Iiwic2hvcnRUZXJtRm9yZWNhc3RHZW5lcmF0aW9uOmhvdXJBaGVhZE1ldGVyIiwic2hvcnRUZXJtRm9yZWNhc3RHZW5lcmF0aW9uOmhvdXJBaGVhZFNldHRsZW1lbnQiLCJzaG9ydFRlcm1Gb3JlY2FzdDptZXRlciIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2U6YWxsQnJhbmRQZXJmb3JtYW5jZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2U6YWxsUGVyZm9ybWFuY2UiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlOmFsbFBvcnRmb2xpb1BlcmZvcm1hbmNlIiwic2hvcnRUZXJtRm9yZWNhc3RQZXJmb3JtYW5jZTpkYWlseUJyYW5kTWFwZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2U6ZGFpbHlNYXBlIiwic2hvcnRUZXJtRm9yZWNhc3RQZXJmb3JtYW5jZTpkYWlseVBvcnRmb2xpb01hcGUiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlOm1vbnRobHlCcmFuZE1hcGUiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlOm1vbnRobHlNYXBlIiwic2hvcnRUZXJtRm9yZWNhc3RQZXJmb3JtYW5jZTptb250aGx5UG9ydGZvbGlvTWFwZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2VTeXN0ZW06c3lzdGVtRVJDT1RBbGxQZXJmb3JtYW5jZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2VTeXN0ZW06c3lzdGVtRVJDT1REYWlseVBlcmZvcm1hbmNlIiwic2hvcnRUZXJtRm9yZWNhc3RQZXJmb3JtYW5jZVN5c3RlbTpzeXN0ZW1FUkNPVE1vbnRobHlQZXJmb3JtYW5jZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2VTeXN0ZW06c3lzdGVtUEpNQWxsUGVyZm9ybWFuY2UiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlU3lzdGVtOnN5c3RlbVBKTURhaWx5UGVyZm9ybWFuY2UiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlU3lzdGVtOnN5c3RlbVBKTU1vbnRobHlQZXJmb3JtYW5jZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2VUb3BEb3duOmFsbEJyYW5kUGVyZm9ybWFuY2UiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlVG9wRG93bjphbGxQZXJmb3JtYW5jZSIsInNob3J0VGVybUZvcmVjYXN0UGVyZm9ybWFuY2VUb3BEb3duOmFsbFBvcnRmb2xpb1BlcmZvcm1hbmNlIiwic2hvcnRUZXJtRm9yZWNhc3RQZXJmb3JtYW5jZVRvcERvd246ZGFpbHlCcmFuZE1hcGUiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlVG9wRG93bjpkYWlseU1hcGUiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlVG9wRG93bjpkYWlseVBvcnRmb2xpb01hcGUiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlVG9wRG93bjptb250aGx5QnJhbmRNYXBlIiwic2hvcnRUZXJtRm9yZWNhc3RQZXJmb3JtYW5jZVRvcERvd246bW9udGhseU1hcGUiLCJzaG9ydFRlcm1Gb3JlY2FzdFBlcmZvcm1hbmNlVG9wRG93bjptb250aGx5UG9ydGZvbGlvTWFwZSIsInNob3J0VGVybUZvcmVjYXN0OnByb2R1Y3RUeXBlIiwic2hvcnRUZXJtRm9yZWNhc3Q6cHJvZmlsZSIsInNob3J0VGVybUZvcmVjYXN0OnByb2ZpbGVXZWF0aGVyIiwic2hvcnRUZXJtRm9yZWNhc3Q6c2V0dGxlbWVudCIsInNob3J0VGVybUZvcmVjYXN0OnNldHRsZW1lbnRXZWF0aGVyIiwic2hvcnRUZXJtRm9yZWNhc3RTeXN0ZW06c3lzdGVtRVJDT1QiLCJzaG9ydFRlcm1Gb3JlY2FzdFN5c3RlbTpzeXN0ZW1QSk0iLCJzaG9ydFRlcm1Gb3JlY2FzdDp0b3BEb3duIiwic2hvcnRUZXJtRm9yZWNhc3Q6dG9wRG93bldlYXRoZXIiLCJ3aG9sZXNhbGVTZXR0bGVtZW50OmFsbG9jYXRpb25FUkNPVCIsIndob2xlc2FsZVNldHRsZW1lbnQ6c3VtbWFyeUVSQ09UIl0sImRhc2hib2FyZFR5cGUiOlsiT3ZlcnZpZXciLCJTdXBwbHkiXX0.krFIe1Rg5yeCuveDMFf4wK7b8yZfgX8gZbsaOUUsafAynla8H3RbeVUgnsn0sAn8vUQGXwLHsFpFubRBfa-fQplOyfiNYxGOZakc7RHA_1ov5J_fbonYR9BksutMBQ5tJ7RJGB8x_W40xQhJBq0ZnuG3s0jiyVGM_JZTg_Zf9pzwb-PQlYwswdndK91Dg_UWwqDVRULgpHS7AhUtMuu97V8B7lr4xZ3x5JkgmC_kk7B2VbfmBfMcdXYo-F5dOne06v0elpDW1f3WqFchZ_TCYxGluZLeikzQzlmxTvt7OEjUmKWFvuyqEjSfQ-P3aaxo9hoqRLI0YvD-5XxevuWrdg",
                }
            },
        }
    );
});

builder.Services.AddResponseCompression(options =>
    options.EnableForHttps = true
);

builder
    .Services.AddScoped<IConfigService, ConfigService>()
    .AddScoped<ICredentialService, CredentialService>()
    .AddScoped<ILocationAttributeService, LocationAttributeService>();

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
