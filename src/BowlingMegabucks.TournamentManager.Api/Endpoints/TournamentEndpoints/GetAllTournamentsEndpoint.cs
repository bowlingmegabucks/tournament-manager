namespace BowlingMegabucks.TournamentManager.Api.Endpoints.TournamentEndpoints;

internal static class GetAllTournamentsEndpoint
{
    public static IEndpointRouteBuilder MapGetAllTournamentsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", () => Results.Ok("Hello from GetAllTournamentsEndpoint!"))
            .WithName("GetAllTournaments")
            .WithSummary("Retrieves all tournaments.")
            .WithDescription("Gets a list of all tournaments available in the system.")
            .Produces<string>(StatusCodes.Status200OK);

        return app;
    }
}
