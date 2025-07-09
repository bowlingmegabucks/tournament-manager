using System.Reflection.PortableExecutable;
using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using NJsonSchema.Generation.TypeMappers;
using NortheastMegabuck;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
    o.ReleaseVersion = 1;
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

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(RegistrationId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = RegistrationId.New();
        }));
    };

    o.UsePropertyNamingPolicy = true;
    o.EnableJWTBearerAuth = false;
    o.ShortSchemaNames = true;

    o.SerializerSettings = s
        => s.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

app.UseSwaggerGen(options => options.Path = "/openapi/{documentName}.json");
app.MapScalarApiReference("/spec");

app.UseHttpsRedirection();

app.UseFastEndpoints(c =>
{
    c.Versioning.Prefix = "v";
    c.Versioning.DefaultVersion = 1;
    c.Versioning.PrependToRoute = true;

    c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

    c.Endpoints.ShortNames = true;
    c.Errors.UseProblemDetails(pd =>
    {
        pd.IndicateErrorCode = true;
        pd.IndicateErrorSeverity = true;
    });
});

await app.RunAsync();
