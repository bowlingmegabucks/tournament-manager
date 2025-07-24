using System.Text.Json;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication;
using BowlingMegabucks.TournamentManager;
using BowlingMegabucks.TournamentManager.Api;
using BowlingMegabucks.TournamentManager.Api.Authentication;
using BowlingMegabucks.TournamentManager.Api.Extensions;
using BowlingMegabucks.TournamentManager.Database;

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

builder.Services.AddOpenTelemetry(builder.Logging, builder.Environment);

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
