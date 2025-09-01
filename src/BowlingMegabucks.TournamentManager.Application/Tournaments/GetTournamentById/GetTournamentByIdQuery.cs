using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;

/// <summary>
/// Query to retrieve the details of a tournament by its unique identifier.
/// </summary>
public sealed record GetTournamentByIdQuery
    : IQuery<TournamentDetailDto?>
{
    /// <summary>
    /// Gets or sets the unique identifier of the tournament to retrieve.
    /// </summary>
    public TournamentId Id { get; init; }
}
