using BowlingMegabucks.TournamentManager.Api.Logging;
using BowlingMegabucks.TournamentManager.Api.OpenApi;
using BowlingMegabucks.TournamentManager.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder
    .AddLogging()
    .AddOpenApi()
    .AddInfrastructureServices();

WebApplication app = builder.Build();

app
    .UseOpenApi()
    .UseInfrastructure();

app.MapGet("/", () => TypedResults.Ok("Tournament Manager API"))
    .WithTags("Initial")
    .Deprecated();

app.MapGet("/error", () =>
{
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
