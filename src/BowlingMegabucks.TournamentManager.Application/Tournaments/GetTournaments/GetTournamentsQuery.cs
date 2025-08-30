using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournaments;

/// <summary>
/// Represents a query to retrieve all tournaments with offset-based pagination support.
/// </summary>
public sealed record GetTournamentsQuery
    : IOffsetPaginationQuery<TournamentSummaryDto>
{
    /// <inheritdoc />
    public required int Page { get; init; }

    /// <inheritdoc />
    public required int PageSize { get; init; }
}
