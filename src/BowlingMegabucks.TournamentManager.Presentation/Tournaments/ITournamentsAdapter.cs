using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments;

internal interface ITournamentsAdapter
{
    Task<ErrorOr<IReadOnlyCollection<TournamentSummaryViewModel>>> GetTournamentsAsync(int? page, int? pageSize, CancellationToken cancellationToken);
}
