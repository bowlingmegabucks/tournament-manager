
namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournament;

/// <summary>
/// Request to retrieve a specific tournament by its ID.
/// </summary>
public sealed record GetTournamentRequest
{
    /// <summary>
    /// The unique identifier of the tournament to retrieve.
    /// </summary>
    public required TournamentId Id { get; init; }
}