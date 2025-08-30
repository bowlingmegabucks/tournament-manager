using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments;

internal static class TournamentMappingExtensions
{
    public static TournamentSummaryViewModel ToViewModel(this TournamentSummary tournamentSummary)
    {
        return new TournamentSummaryViewModel
        {
            Id = tournamentSummary.Id,
            Name = tournamentSummary.Name,
            StartDate = tournamentSummary.StartDate,
            EndDate = tournamentSummary.EndDate,
            BowlingCenter = tournamentSummary.BowlingCenter,
            EntryFee = tournamentSummary.EntryFee,
            Completed = tournamentSummary.Completed,
        };
    }
}
