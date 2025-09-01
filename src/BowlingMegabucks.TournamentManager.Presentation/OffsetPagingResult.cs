namespace BowlingMegabucks.TournamentManager.Presentation;

/// <summary>
/// Represents a paginated result set using offset-based pagination.
/// </summary>
/// <typeparam name="T">The type of items contained in the result set.</typeparam>
public sealed record OffsetPagingResult<T>
{
    /// <summary>
    /// Gets or initializes the collection of items for the current page.
    /// </summary>
    /// <value>A read-only collection of items. Defaults to an empty collection.</value>
    public IReadOnlyCollection<T> Items { get; init; } = [];

    /// <summary>
    /// Gets or initializes the total number of pages available.
    /// </summary>
    /// <value>The total number of pages based on the page size and total items.</value>
    public int TotalPages { get; init; }

    /// <summary>
    /// Gets or initializes the current page number (1-based).
    /// </summary>
    /// <value>The current page number, typically starting from 1.</value>
    public int CurrentPage { get; init; }

    /// <summary>
    /// Gets or initializes the number of items per page.
    /// </summary>
    /// <value>The maximum number of items that can be returned in a single page.</value>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets or initializes the total number of items across all pages.
    /// </summary>
    /// <value>The total count of items in the entire result set, not just the current page.</value>
    public int TotalItems { get; init; }
}
