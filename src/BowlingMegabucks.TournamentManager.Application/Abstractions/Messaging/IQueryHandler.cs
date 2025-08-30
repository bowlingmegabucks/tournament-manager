using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Defines a handler for queries that return a single response.
/// </summary>
/// <typeparam name="TQuery">The type of the query that implements <see cref="IQuery{TResponse}"/>.</typeparam>
/// <typeparam name="TResponse">The type of the response returned by the query handler.</typeparam>
public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Handles the specified query asynchronously and returns a response.
    /// </summary>
    /// <param name="query">The query to be handled containing the request parameters.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains either a successful
    /// response of type <typeparamref name="TResponse"/>, or error information if the operation fails.
    /// </returns>
    Task<ErrorOr<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
