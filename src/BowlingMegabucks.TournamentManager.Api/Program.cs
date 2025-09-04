using BowlingMegabucks.TournamentManager.Api.Endpoints.TournamentEndpoints;
using BowlingMegabucks.TournamentManager.Api.Logging;
using BowlingMegabucks.TournamentManager.Api.OpenApi;
using BowlingMegabucks.TournamentManager.Api.Versioning;
using BowlingMegabucks.TournamentManager.Application;
using BowlingMegabucks.TournamentManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment() &&
    builder.Configuration.GetValue<bool>("DOTNET_USE_DOCKER_JSON"))
{
    builder.Configuration.AddJsonFile("appsettings.Docker.Development.json", optional: true, reloadOnChange: true);
}

builder.Host.UseDefaultServiceProvider(config =>
    config.ValidateOnBuild = true);

builder.Services.AddHttpContextAccessor();

builder
    .AddLogging()
    .AddOpenApi()
    .AddVersioning();

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration, builder.Environment);

WebApplication app = builder.Build();

await app
    .UseOpenApi()
    .UseInfrastructure();

RouteGroupBuilder group = app.MapGroup("api/v{version:apiVersion}");

group
    .MapTournamentEndpoints();

app.MapGet("/", (IConfiguration config)
    => TypedResults.Ok($"Tournament Manager API Health UI: {config["HealthChecksUI:HealthChecks:0:Uri"]}"))
    .WithTags("Initial")
    .Deprecated();

app.MapGet("/error", (ILoggerFactory loggerFactory) =>
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
    .WithTags("Initial");

await app.RunAsync();
