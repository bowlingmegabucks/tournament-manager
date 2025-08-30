using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

/// <summary>
/// Adapter interface for retrieving tournaments with pagination support.
/// </summary>
public interface IGetTournamentsAdapter
{
    /// <summary>
    /// Executes a request to retrieve tournaments with optional pagination parameters.
    /// </summary>
    /// <param name="page">The page number to retrieve (optional). If null, returns the first page.</param>
    /// <param name="pageSize">The number of items per page (optional). If null, uses default page size.</param>
    /// <param name="cancellationToken">Token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains either:
    /// - A successful result with tournament summary data and pagination information
    /// - An error result with details about what went wrong
    /// </returns>
    Task<ErrorOr<OffsetPagingResult<TournamentSummaryViewModel>>> ExecuteAsync(int? page, int? pageSize, CancellationToken cancellationToken);
}
