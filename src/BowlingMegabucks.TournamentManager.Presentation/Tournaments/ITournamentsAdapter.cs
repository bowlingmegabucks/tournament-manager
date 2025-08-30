using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments;

internal interface ITournamentsAdapter
{
    Task<ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>>> GetTournamentsAsync(int? page, int? pageSize, CancellationToken cancellationToken);
}
