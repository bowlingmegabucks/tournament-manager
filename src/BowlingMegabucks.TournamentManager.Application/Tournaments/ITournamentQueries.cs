using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments;

/// <summary>
/// Provides query operations for tournament data.
/// </summary>
public interface ITournamentQueries
{
    /// <summary>
    /// Retrieves all tournaments asynchronously with pagination support.
    /// </summary>
    /// <param name="pagination">The pagination parameters to control page size and offset.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A read-only collection of tournament summaries for the requested page.</returns>
    Task<IReadOnlyCollection<TournamentSummaryDto>> GetAllTournamentsAsync(
        IOffsetPaginationQuery pagination,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves the total count of tournaments asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The total count of tournaments.</returns>
    Task<int> GetTotalTournamentCountAsync(CancellationToken cancellationToken);
}
