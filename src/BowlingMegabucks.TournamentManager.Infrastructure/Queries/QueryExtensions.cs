using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Queries;

internal static class QueryExtensions
{
    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> queryable, IOffsetPaginationQuery pagination)
        => queryable
            .Skip(pagination.Offset)
            .Take(pagination.PageSize);
}
