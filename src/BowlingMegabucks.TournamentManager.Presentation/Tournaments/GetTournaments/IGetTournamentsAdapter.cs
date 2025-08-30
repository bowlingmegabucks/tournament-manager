using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

internal interface IGetTournamentsAdapter
{
    Task<ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>>> ExecuteAsync(int? page, int? pageSize, CancellationToken cancellationToken);
}
