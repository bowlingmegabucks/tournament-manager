namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Represents the response for a paginated query with offset-based pagination.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IOffsetPaginationQueryResponse<T>
{
    /// <summary>
    /// Gets the total number of items available across all pages.
    /// </summary>
    int TotalItems { get; init; }

    /// <summary>
    /// Gets the total number of pages available based on the page size.
    /// </summary>
    int TotalPages { get; init; }

    /// <summary>
    /// Gets the current page number (1-indexed).
    /// </summary>
    int CurrentPage { get; init; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    int PageSize { get; init; }

    /// <summary>
    /// Gets the collection of items for the current page.
    /// </summary>
    IReadOnlyCollection<T> Items { get; init; }
}
