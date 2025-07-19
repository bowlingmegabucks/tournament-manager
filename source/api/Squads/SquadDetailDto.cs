namespace BowlingMegabucks.TournamentManager.Api.Squads;

/// <summary>
/// Data Transfer Object for retrieving detailed information about a squad in a tournament.
/// </summary>
public sealed record SquadDetailDto
{
    /// <summary>
    /// Unique identifier for the squad.
    /// </summary>
    public required SquadId Id { get; init; }

    /// <summary>
    /// Date and time when the squad is scheduled to take place (Eastern Time).
    /// </summary>
    public required DateTime Date { get; init; }

    /// <summary>
    /// Entry fee for the squad, if different from the tournament's entry fee.  If null, the squad uses the tournament's entry fee.
    /// </summary>
    public required decimal? EntryFee { get; init; }
}