namespace BowlingMegabucks.TournamentManager.Contracts.Tournaments;

/// <summary>
/// Data Transfer Object for retrieving tournament summary information.
/// </summary>
public sealed record TournamentSummary
{
    /// <summary>
    /// The unique identifier for the tournament.
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    public required Guid Id { get; init; }

    /// <summary>
    /// The name of the tournament.
    /// </summary>
    /// <example>Spring Championship 2025</example>
    public required string Name { get; init; }

    /// <summary>
    /// The date when the tournament starts.
    /// </summary>
    /// <example>2025-03-15</example>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// The date when the tournament ends.
    /// </summary>
    /// <example>2025-03-17</example>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// The entry fee for the tournament.
    /// </summary>
    /// <example>75.00</example>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// The bowling center where the tournament is held.
    /// </summary>
    /// <example>Strike Zone Bowling Center</example>
    public required string BowlingCenter { get; init; }

    /// <summary>
    /// Indicates whether the tournament has been completed.
    /// </summary>
    /// <example>false</example>
    public required bool Completed { get; init; }
}
