using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Defines a handler for offset-based pagination queries that return paginated responses.
/// </summary>
/// <typeparam name="TQuery">The type of the pagination query that implements <see cref="IOffsetPaginationQuery{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of items to be returned in the paginated response.</typeparam>
public interface IOffsetPaginationQueryHandler<TQuery, TResponse>
    where TQuery : IOffsetPaginationQuery<TResponse>
{
    /// <summary>
    /// Handles the specified pagination query asynchronously and returns a paginated response.
    /// </summary>
    /// <param name="query">The pagination query containing page number, page size, and other query parameters.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains either a successful
    /// <see cref="OffsetPaginationQueryResponse{TResponse}"/> with paginated data and metadata,
    /// or error information if the operation fails.
    /// </returns>
    Task<ErrorOr<OffsetPaginationQueryResponse<TResponse>>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
