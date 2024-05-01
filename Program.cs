using System.Text.Json.Serialization;
using FastEndpoints.Swagger;
using Learning.FastEndpoionts.Services;

var builder = WebApplication.CreateBuilder();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.RemoveEmptyRequestSchema = true;

        o.SerializerSettings = s => {
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

var app = builder.Build();
app.UseFastEndpoints(c => 
    {
        c.Versioning.Prefix = "v";
        c.Endpoints.RoutePrefix = "api";
    })
   .UseSwaggerGen();
app.Run();