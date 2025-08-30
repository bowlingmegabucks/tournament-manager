using Asp.Versioning.Builder;
using BowlingMegabucks.TournamentManager.Api.Versioning;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;

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

    internal static TournamentSummary ToTournamentSummary(this TournamentSummaryDto tournament)
    {
        ArgumentNullException.ThrowIfNull(tournament);

        return new TournamentSummary
        {
            Id = tournament.Id.Value,
            Name = tournament.Name,
            StartDate = tournament.StartDate,
            EndDate = tournament.EndDate,
            EntryFee = tournament.EntryFee,
            BowlingCenter = tournament.BowlingCenter,
            Completed = tournament.Completed,
        };
    }
}
