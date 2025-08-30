using Asp.Versioning.Builder;
using BowlingMegabucks.TournamentManager.Api.Versioning;

namespace BowlingMegabucks.TournamentManager.Api.Endpoints.TournamentEndpoints;

internal static class TournamentEndpointsExtensions
{
    public static IEndpointRouteBuilder MapTournamentEndpoints(this IEndpointRouteBuilder app)
    {
        ApiVersionSet versionSet = app.BuildVersionSet("Tournaments", 1);

        RouteGroupBuilder tournamentGroup = app.MapGroup("tournaments")
            .WithTags("Tournaments")
            .WithApiVersionSet(versionSet);

        tournamentGroup
            .MapGetAllTournamentsEndpoint();

        return app;
    }
}
