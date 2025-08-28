using BowlingMegabucks.TournamentManager.Application.Tournaments.GetAllTournaments;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments;

/// <summary>
/// Provides query operations for tournament data.
/// </summary>
public interface ITournamentQueries
{
    /// <summary>
    /// Retrieves all tournaments asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A read-only collection of tournament summaries.</returns>
    Task<IReadOnlyCollection<TournamentSummaryDto>> GetAllTournamentsAsync(CancellationToken cancellationToken);
}
