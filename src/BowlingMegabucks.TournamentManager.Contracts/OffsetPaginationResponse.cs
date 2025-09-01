using System.Diagnostics.CodeAnalysis;

namespace BowlingMegabucks.TournamentManager.Contracts;

/// <summary>
/// Represents a paginated response with offset-based pagination for API contracts.
/// </summary>
/// <typeparam name="T">The type of items contained in the paginated response.</typeparam>
/// <example>
/// {
///   "totalItems": 100,
///   "totalPages": 10,
///   "currentPage": 1,
///   "pageSize": 10,
///   "items": [ { "id": "550e8400-e29b-41d4-a716-446655440000", "name": "Spring Championship 2025" } ]
/// }
/// </example>
[SuppressMessage(
    "Design",
    "CA1515:Consider making public types internal",
    Justification = "Required to be public for OpenAPI documentation generation and external API consumers.")]
public sealed record OffsetPaginationResponse<T>
{
    /// <summary>
    /// Gets the total number of items available across all pages.
    /// </summary>
    /// <example>100</example>
    public required int TotalItems { get; init; }

    /// <summary>
    /// Gets the total number of pages available based on the page size.
    /// </summary>
    /// <example>10</example>
    public required int TotalPages { get; init; }

    /// <summary>
    /// Gets the current page number (1-indexed).
    /// </summary>
    /// <example>1</example>
    public required int CurrentPage { get; init; }

    /// <summary>
    /// Gets the number of items per page.
    /// </summary>
    /// <example>10</example>
    public required int PageSize { get; init; }

    /// <summary>
    /// Gets the collection of items for the current page.
    /// </summary>
    /// <example>[{ "id": "550e8400-e29b-41d4-a716-446655440000", "name": "Spring Championship 2025" }]</example>
    public required IReadOnlyCollection<T> Items { get; init; }
}
