using BowlingMegabucks.TournamentManager.Api.OpenApi;
using BowlingMegabucks.TournamentManager.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddOpenApi();

builder.AddInfrastructureServices();

WebApplication app = builder.Build();

app.UseOpenApi();

app.UseInfrastructure();

app.MapGet("/", () => "Tournament Manager API")
    .Produces<string>(StatusCodes.Status200OK)
    .WithTags("Initial");

app.MapGet("/error", () =>
{
    if (2 + 2 == 4)
    {
        throw new InvalidOperationException("Test exception");
    }

})
    .Produces<string>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError)
    .WithTags("Initial");

await app.RunAsync();
