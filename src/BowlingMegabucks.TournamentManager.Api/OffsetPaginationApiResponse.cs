using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Contracts.Tournaments;

namespace BowlingMegabucks.TournamentManager.Api;

/// <summary>
/// Represents a paginated API response containing a subset of items along with pagination metadata.
/// </summary>
/// <typeparam name="T">The type of items contained in the paginated response.</typeparam>
[SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "Required to be public for OpenAPI documentation generation and external API consumers.")]
public sealed record OffsetPaginationApiResponse<T>
{
    /// <summary>
    /// Gets or sets the total number of items available across all pages.
    /// </summary>
    /// <value>The total count of items in the entire dataset.</value>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the total number of pages based on the page size and total items.
    /// </summary>
    /// <value>The total number of pages available for pagination.</value>
    public int TotalPages { get; set; }

    /// <summary>
    /// Gets or sets the current page number being returned (1-based indexing).
    /// </summary>
    /// <value>The current page number, starting from 1.</value>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets or sets the number of items requested per page.
    /// </summary>
    /// <value>The maximum number of items that can be returned in a single page.</value>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the collection of items for the current page.
    /// </summary>
    /// <value>The items included in the current page of results.</value>
    public IEnumerable<T> Items { get; set; } = [];
}
