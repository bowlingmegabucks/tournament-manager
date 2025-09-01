using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;

/// <summary>
/// Defines an adapter for retrieving the details of a specific tournament by its unique identifier.
/// </summary>
public interface IGetTournamentByIdAdapter
{
    /// <summary>
    /// Executes a request to retrieve the details of a tournament by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains either:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// A successful result with the tournament detail view model.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// An error result with details about what went wrong.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    Task<ErrorOr<TournamentDetailViewModel>> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);
}
