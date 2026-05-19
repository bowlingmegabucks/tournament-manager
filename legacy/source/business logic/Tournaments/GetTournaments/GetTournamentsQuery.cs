using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;

/// <summary>
/// Represents a query to retrieve tournaments.
/// </summary>
public sealed record GetTournamentsQuery
    : IQuery<IEnumerable<Models.Tournament>>;