using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

internal interface IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
    Task<ErrorOr<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
