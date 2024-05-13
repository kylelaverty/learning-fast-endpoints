using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using FastEndpoints.Security;
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Unleash;

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

    var assembly = Assembly.GetExecutingAssembly();
    var assemblyName = assembly.GetName();
    var version = assemblyName.Version;
    var buildDate = new System.IO.FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;

    builder.Services.AddSerilog((services, loggerConfig) =>
        loggerConfig
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Version", version)
            .Enrich.WithProperty("BuildDate", buildDate.ToString("f", CultureInfo.InvariantCulture))
    );

    builder.Services
        .AddAuthenticationJwtBearer(
            s =>
            {
                s.SigningKey = "The secret used to sign tokens that is longer.";
            },
            b =>
            {
                b.SaveToken = true;
                b.RequireHttpsMetadata = false;
                b.TokenValidationParameters.ValidateIssuer = true;
                b.TokenValidationParameters.ValidateAudience = true;
                b.TokenValidationParameters.ValidateLifetime = true;
                b.TokenValidationParameters.RequireExpirationTime = true;
                b.TokenValidationParameters.ValidAudience = "The audience";
                b.TokenValidationParameters.ValidIssuer = "The issuer";
            })
        .AddAuthorization(options =>
        {
            options.AddPolicy("ManagersOnly", policy => policy.RequireRole("Manager"));
        })
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

    builder.Services.AddAuthentication(o => o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme);

    builder.Services.AddSingleton<IUnleash>(s =>
    {
        var appName = _applicationName;
        var unleashSettings = new UnleashSettings
        {
            AppName = appName,
            UnleashApi = new Uri("http://localhost:4242/api"),
            FetchTogglesInterval = TimeSpan.FromSeconds(30),
            SendMetricsInterval = TimeSpan.FromSeconds(30),
            CustomHttpHeaders = new Dictionary<string, string>
            {
                { "Authorization", "*:environmentnamegoeshere.somethingsomethingsomethingfun" }
            }
        };
        return new DefaultUnleash(settings: unleashSettings);
    });

    await using var app = builder.Build();
    app.UseSerilogRequestLogging()
       .UseAuthentication()
       .UseAuthorization()
       .UseFastEndpoints(c =>
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