using System.Text.Json;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;
using NJsonSchema.Generation.TypeMappers;
using BowlingMegabucks.TournamentManager;
using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Api.Authentication;
using BowlingMegabucks.TournamentManager.Database;
using BowlingMegabucks.TournamentManager.Models;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddFastEndpoints()
    .AddAuthorization()
    .AddAuthentication(ApiKeyAuthentication.SchemeName)
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthentication>(ApiKeyAuthentication.SchemeName, null);

builder.Services.AddBusinessLogic(builder.Configuration);
    
builder.Services.SwaggerDocument(o =>
{
    o.ReleaseVersion = 1;
    o.DocumentSettings = s =>
    {
        s.DocumentName = "v1";
        s.Title = "Northeast Megabuck Tournament API";
        s.Version = "v1";
        s.AddAuth(ApiKeyAuthentication.SchemeName, new()
        {
            Name = ApiKeyAuthentication.HeaderName,
            In = NSwag.OpenApiSecurityApiKeyLocation.Header,
            Type = NSwag.OpenApiSecuritySchemeType.ApiKey
        });

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(TournamentId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = TournamentId.New();
        }));

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(SquadId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = SquadId.New();
        }));

        s.SchemaSettings.TypeMappers.Add(new PrimitiveTypeMapper(typeof(RegistrationId), schema =>
        {
            schema.Type = NJsonSchema.JsonObjectType.String;
            schema.Format = "uuid";
            schema.Example = RegistrationId.New();
        }));

        s.SchemaSettings.TypeMappers.Add(new SmartEnumTypeMapper<Gender>());
    };

    o.UsePropertyNamingPolicy = true;
    o.EnableJWTBearerAuth = false;
    o.ShortSchemaNames = true;

    o.SerializerSettings = s
        => s.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(builder.Environment.ApplicationName))
    .WithTracing(tracing => tracing
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation())
    .WithMetrics(metrics => metrics
        .AddHttpClientInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddRuntimeInstrumentation());

builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeScopes = true;
    options.IncludeFormattedMessage = true;
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddOpenTelemetry().UseOtlpExporter();
}
else
{
    builder.Services.AddOpenTelemetry().UseAzureMonitor();
}

var app = builder.Build();

app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
app.MapScalarApiReference();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    await scope.ApplyMigrationsAsync();
}

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization()
    .UseFastEndpoints(c =>
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
    })
    .UseSwaggerGen();

await app.RunAsync();
