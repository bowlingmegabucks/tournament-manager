using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

internal interface IOffsetPaginationQueryHandler<TQuery, TResponse>
    where TQuery : IOffsetPaginationQuery<TResponse>
{
    Task<ErrorOr<OffsetPaginationQueryResponse<TResponse>>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
