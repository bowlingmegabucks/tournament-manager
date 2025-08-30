using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using Refit;

namespace BowlingMegabucks.TournamentManager.Presentation.Services;

internal interface ITournamentManagerApi
{
    [Get("api/v1/tournaments")]
    Task<OffsetPaginationResponse<TournamentSummary>> GetAllTournamentsAsync([Query] int? page, [Query] int? pageSize, CancellationToken cancellationToken);
}
