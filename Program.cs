using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Learning.FastEndpoionts.Services;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, loggerConfig) =>
        {
            Log.Information("Starting Full Log Configuration");
            loggerConfig
                .ReadFrom.Configuration(builder.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        }
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
                s.Title = "My Testing FastEndpoints API";
                s.Description = "My API description";
                s.DocumentName = "v1.0";
                s.Version = "v1.0";
            };
        });

    
    builder.Services.AddSingleton<IBookService, BookService>();

    await using var app = builder.Build();
    app.UseSerilogRequestLogging();
    app.UseFastEndpoints(c =>
        {
            c.Versioning.Prefix = "v";
            c.Endpoints.RoutePrefix = "api";
        })
    .UseSwaggerGen();

    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}