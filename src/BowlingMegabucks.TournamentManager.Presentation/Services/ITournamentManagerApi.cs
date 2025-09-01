using BowlingMegabucks.TournamentManager.Contracts;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using Refit;

namespace BowlingMegabucks.TournamentManager.Presentation.Services;

internal interface ITournamentManagerApi
{
    [Get("/api/v1/tournaments")]
    Task<OffsetPaginationResponse<TournamentSummary>> GetTournamentsAsync([Query] int? page, [Query] int? pageSize, CancellationToken cancellationToken);

    [Get("/api/v1/tournaments/{id}")]
    Task<Contracts.ApiResponse<TournamentDetail>> GetTournamentByIdAsync(TournamentId id, CancellationToken cancellationToken);
}
