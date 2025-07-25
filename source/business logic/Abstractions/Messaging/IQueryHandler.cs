using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Abstractions.Messaging;

/// <summary>
/// Represents a query that returns a response of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TQuery"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    /// <summary>
    /// Handles the query and returns a response of type <typeparamref name="TResponse"/>.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ErrorOr<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}