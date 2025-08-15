using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

/// <summary>
/// Represents a query to retrieve a registration by its ID.
/// </summary>
public sealed record GetTournamentByIdQuery
    : IQuery<Models.Registration?>
{
    /// <summary>
    /// The ID of the tournament to retrieve.
    /// </summary>
    public TournamentId Id { get; init; }
}