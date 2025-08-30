namespace BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

/// <summary>
/// Represents the response for a paginated query with offset-based pagination.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed record OffsetPaginationQueryResponse<T>
{
    /// <summary>
    /// Gets the total number of items available across all pages.
    /// </summary>
    public required int TotalItems { get; init; }

    /// <summary>
    /// Gets the total number of pages available based on the page size.
    /// </summary>
    public required int TotalPages { get; init; }

    /// <summary>
    /// Gets the current page number (1-indexed).
    /// </summary>
    public required int CurrentPage { get; init; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    public required int PageSize { get; init; }

    /// <summary>
    /// Gets the collection of items for the current page.
    /// </summary>
    public required IReadOnlyCollection<T> Items { get; init; }
}
