using BowlingMegabucks.TournamentManager.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddInfrastructureServices();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseInfrastructure();

app.MapGet("/", () => "Tournament Manager API")
    .Produces<string>(StatusCodes.Status200OK);

app.MapGet("/error", () =>
{
    if (2 + 2 == 4)
    {
        throw new InvalidOperationException("Test exception");
    }

})
    .Produces<string>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status500InternalServerError);

await app.RunAsync();
