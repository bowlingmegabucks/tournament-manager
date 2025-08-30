using System.Diagnostics.CodeAnalysis;

namespace BowlingMegabucks.TournamentManager.Contracts;

/// <summary>
/// Represents a paginated response with offset-based pagination for API contracts.
/// </summary>
/// <typeparam name="T">The type of items contained in the paginated response.</typeparam>
[SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "Required to be public for OpenAPI documentation generation and external API consumers.")]
public sealed record OffsetPaginationResponse<T>
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
