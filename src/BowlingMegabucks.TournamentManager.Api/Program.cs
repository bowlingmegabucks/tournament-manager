using Asp.Versioning.Builder;
using BowlingMegabucks.TournamentManager.Api.Logging;
using BowlingMegabucks.TournamentManager.Api.OpenApi;
using BowlingMegabucks.TournamentManager.Api.Versioning;
using BowlingMegabucks.TournamentManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add Docker config conditionally after environment variables are loaded

builder.Configuration.AddJsonFile("appsettings.Infrastructure.json", optional: false, reloadOnChange: true);

if (builder.Environment.IsDevelopment() &&
    builder.Configuration.GetValue<bool>("DOTNET_USE_DOCKER_JSON"))
{
    builder.Configuration.AddJsonFile("appsettings.Docker.Development.json", optional: true, reloadOnChange: true);
}

builder.Services.AddHttpContextAccessor();

builder
    .AddLogging()
    .AddOpenApi()
    .AddVersioning()
    .AddInfrastructureServices();

WebApplication app = builder.Build();

app
    .UseOpenApi()
    .UseInfrastructure();

ApiVersionSet initialVersionSet = app.BuildVersionSet(1);

RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(initialVersionSet); // This is set as an example for now. When entity routes are added, the group should set the API version set accordingly.

group.MapGet("/", (IConfiguration config)
    => TypedResults.Ok($"Tournament Manager API Health UI: {config["HealthChecksUI:HealthChecks:0:Uri"]}"))
    .WithTags("Initial")
    .MapToApiVersion(1)
    .Deprecated();

group.MapGet("/error", (ILoggerFactory loggerFactory) =>
{
    ILogger logger = loggerFactory.CreateLogger("Error");
#pragma warning disable CA1848 // Use the LoggerMessage delegates
    logger.LogInformation("Testing Log");
#pragma warning restore CA1848 // Use the LoggerMessage delegates

    if (2 + 2 == 4)
    {
        throw new InvalidOperationException("Test exception");
    }

})
    .WithSummary("Throws an exception for testing purposes.")
    .WithDescription("This endpoint is used to test the error handling.")
    .WithName("Error Handling Test")
    .Accepts<List<int>>("application/json")
    .Produces<List<int>>(StatusCodes.Status200OK)
    .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
    .WithTags("Initial")
    .MapToApiVersion(1);

await app.RunAsync();
