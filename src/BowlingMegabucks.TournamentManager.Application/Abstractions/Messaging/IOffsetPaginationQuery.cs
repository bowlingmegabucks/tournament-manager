namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Represents a query that supports offset-based pagination for retrieving data in pages.
/// </summary>
/// <typeparam name="TResponse">The type of response returned by the query.</typeparam>
public interface IOffsetPaginationQuery<TResponse>
    : IQuery<TResponse>
{
    /// <summary>
    /// Gets the page number (1-indexed) to retrieve.
    /// </summary>
    /// <value>The page number, where 1 represents the first page.</value>
    int Page { get; init; }

    /// <summary>
    /// Gets the number of items to return per page.
    /// </summary>
    /// <value>The maximum number of items in each page.</value>
    int PageSize { get; init; }

    /// <summary>
    /// Gets the calculated offset for database queries based on the current page and page size.
    /// </summary>
    /// <value>The number of items to skip, calculated as (Page - 1) * PageSize.</value>
    int Offset
        => (Page - 1) * PageSize;
}
