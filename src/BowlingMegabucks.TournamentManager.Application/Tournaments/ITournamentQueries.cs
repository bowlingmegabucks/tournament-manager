using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;

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
    /// <remarks>
    /// <para>Use this method to fetch a paginated list of tournaments for display or processing.</para>
    /// </remarks>
    /// <example>
    /// var tournaments = await queries.GetAllTournamentsAsync(pagination, cancellationToken);
    /// </example>
    Task<IReadOnlyCollection<TournamentSummaryDto>> GetAllTournamentsAsync(
        IOffsetPaginationQuery pagination,
        CancellationToken cancellationToken);


    /// <summary>
    /// Retrieves the total count of tournaments asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The total count of tournaments.</returns>
    /// <remarks>
    /// <para>This method is useful for pagination calculations and reporting.</para>
    /// </remarks>
    /// <example>
    /// int count = await queries.GetTotalTournamentCountAsync(cancellationToken);
    /// </example>
    Task<int> GetTotalTournamentCountAsync(CancellationToken cancellationToken);


    /// <summary>
    /// Retrieves the details of a specific tournament asynchronously.
    /// </summary>
    /// <param name="tournamentId">The unique identifier of the tournament to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The details of the specified tournament, or <langword>null</langword> if not found.</returns>
    /// <remarks>
    /// <para>Returns <langword>null</langword> if the tournament does not exist.</para>
    /// </remarks>
    /// <example>
    /// var details = await queries.GetTournamentAsync(tournamentId, cancellationToken);
    /// </example>
    Task<TournamentDetailDto?> GetTournamentAsync(
        TournamentId tournamentId,
        CancellationToken cancellationToken);
}
