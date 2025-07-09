using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.DocumentSettings = s =>
    {
        s.DocumentName = "v1";
        s.Title = "Northeast Megabuck Tournament API";
        s.Version = "v1";
        s.AddAuth("Api Key", new()
        {
            Name = "x-api-key",
            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            Type = NSwag.OpenApiSecuritySchemeType.ApiKey
        });
    };

    o.UsePropertyNamingPolicy = true;
    o.EnableJWTBearerAuth = false;
    o.ShortSchemaNames = true;
});

var app = builder.Build();

app.UseSwaggerGen(options => options.Path = "/openapi/{documentName}.json");
app.MapScalarApiReference("/spec");

app.UseHttpsRedirection();

app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    c.Endpoints.ShortNames = true;
});

await app.RunAsync();
