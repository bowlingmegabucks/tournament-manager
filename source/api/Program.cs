using System.Text.Json;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;
using NJsonSchema.Generation.TypeMappers;
using BowlingMegabucks.TournamentManager;
using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Api.Authentication;
using BowlingMegabucks.TournamentManager.Api.Extensions;
using BowlingMegabucks.TournamentManager.Database;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiDocumentation();

builder.Services.AddProblemDetails();

builder.Services.ConfigureRateLimiting(builder.Configuration);

builder.Services.AddFastEndpoints()
    .AddAuthorization()
    .AddAuthentication(ApiKeyAuthentication._schemeName)
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthentication>(ApiKeyAuthentication._schemeName, null);

builder.Services.AddBusinessLogic(builder.Configuration);

builder.Services.AddApiHealthChecks(builder.Configuration);

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

app.UseApiRateLimiting();

app.MapApiHealthChecks();

app.UseOpenApiDocumentation();

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();

    using var scope = app.Services.CreateScope();
    await scope.ApplyMigrationsAsync();
}

app.MapGet("/", () => Results.Json(new
{
    name = "Northeast Megabuck Tournament API",
    version = "v1",
    status = "OK",
    documentation = "/openapi/v1.json"
})).AllowAnonymous();

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
