using System.Diagnostics;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Serilog;
using Serilog.Events;

const string _applicationName = "Learning - FastEndpoints";

Activity.DefaultIdFormat = ActivityIdFormat.W3C;

InitializeBootUpLogger();

try
{
    Log.Logger.Information("Main - Init {ApplicationName}", _applicationName);
    Log.Logger.Information("Executing in user context: {User}", Environment.UserName);

    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        Args = args
    });

    builder.Services.AddSerilog((services, loggerConfig) =>
        loggerConfig
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
    );

    builder.Services
        .AddFastEndpoints()
        .SwaggerDocument(o =>
        {
            o.RemoveEmptyRequestSchema = true;

            o.SerializerSettings = s =>
            {
                s.PropertyNamingPolicy = null;
                s.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            };

            o.DocumentSettings = s =>
            {
                s.Title = $"{_applicationName} API";
                s.Description = $"My {_applicationName} API description";
                s.DocumentName = "v1.0";
                s.Version = "v1.0";
            };
        });

    await using var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseFastEndpoints(c =>
        {
            c.Versioning.Prefix = "v";
            c.Endpoints.RoutePrefix = "api";
        })
    .UseSwaggerGen();

    await app.RunAsync();
}
catch (Exception ex)
{
    // Catch errors in project setup that result in an unusable system.
    Log.Logger.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Logger.Information("Main - Shutdown {ApplicationName}", _applicationName);

    // Ensure to flush and stop internal timers/threads before application-exit
    // (Avoid segmentation fault on Linux).
    Log.CloseAndFlush();
}

static void InitializeBootUpLogger()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console(new Serilog.Formatting.Compact.RenderedCompactJsonFormatter())
        .CreateLogger();
}

public partial class Program { }