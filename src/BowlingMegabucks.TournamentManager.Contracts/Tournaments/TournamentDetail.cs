using BowlingMegabucks.TournamentManager.Domain.Tournaments;


namespace BowlingMegabucks.TournamentManager.Contracts.Tournaments;

/// <summary>
/// Represents the details of a bowling tournament, including schedule, location, and payout information.
/// </summary>
public sealed record TournamentDetail
{
    /// <summary>
    /// Gets the unique identifier for the tournament.
    /// </summary>
    /// <example>550e8400-e29b-41d4-a716-446655440000</example>
    public required TournamentId Id { get; init; }

    /// <summary>
    /// Gets the name of the tournament.
    /// </summary>
    /// <example>Spring Championship 2025</example>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the date when the tournament starts.
    /// </summary>
    /// <example>2025-03-15</example>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// Gets the date when the tournament ends.
    /// </summary>
    /// <example>2025-03-17</example>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// Gets the number of games in a squad in the tournament.
    /// </summary>
    /// <value>4</value>
    public required short Games { get; init; }

    /// <summary>
    /// Gets the entry fee for the tournament.
    /// </summary>
    /// <example>125.00</example>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// Gets the name of the bowling center hosting the tournament.
    /// </summary>
    /// <example>Lucky Strike Lanes</example>
    public required string BowlingCenter { get; init; }

    /// <summary>
    /// Gets the standard ratio of players who advance to the finals.
    /// </summary>
    /// <example>8.0</example>
    public required decimal FinalsRatio { get; init; }

    /// <summary>
    /// Gets the standard ratio of cash prizes awarded in the tournament.
    /// </summary>
    /// <example>5.0</example>
    public required decimal CashRatio { get; init; }

    /// <summary>
    /// Gets the standard ratio of cash prizes awarded to Super Sweeper participants.
    /// </summary>
    /// <example>6.0</example>
    public required decimal SuperSweeperCashRatio { get; init; }

    /// <summary>
    /// Gets a value indicating whether the tournament has been completed.
    /// </summary>
    /// <value><langword>true</langword> if the tournament has been completed; otherwise, <langword>false</langword>.</value>
    /// <example>false</example>
    public required bool Completed { get; init; }
}
