namespace NortheastMegabuck.Api.Sweepers;

/// <summary>
/// Data Transfer Object for retrieving detailed information about a sweeper in a tournament.
/// </summary>
public sealed record SweeperDetailDto
{
    /// <summary>
    /// The unique identifier for the sweeper.
    /// </summary>
    public required SquadId Id { get; init; }

    /// <summary>
    /// Date and time when the sweeper is scheduled to take place (Eastern Time).
    /// </summary>
    public required DateTime Date { get; init; }

    /// <summary>
    /// Entry fee for the sweeper.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// Number of games bowled during the sweeper.
    /// </summary>
    public required short Games { get; init; }
}