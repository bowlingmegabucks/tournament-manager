namespace NortheastMegabuck.Api.Tournaments.GetTournament;

/// <summary>
/// Response containing the details of a specific tournament.
/// </summary>
public sealed record GetTournamentResponse
{
    /// <summary>
    /// The details of the requested tournament.
    /// </summary>
    public required TournamentDetailDto Tournament { get; init; }
}