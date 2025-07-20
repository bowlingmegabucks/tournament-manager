namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;

/// <summary>
/// Data Transfer Object for retrieving tournament details.
/// </summary>
public sealed record TournamentSummaryDto
{
    /// <summary>
    /// The unique identifier for the tournament.
    /// </summary>
    public required TournamentId Id { get; init; }

    /// <summary>
    /// The name of the tournament.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The date when the tournament starts.
    /// </summary>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// The date when the tournament ends.
    /// </summary>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// The entry fee for the tournament.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// The bowling center where the tournament is held.
    /// </summary>
    public required string BowlingCenter { get; init; }

    /// <summary>
    /// Number of entries required to take advance an additional participant to the finals.
    /// </summary>
    public required decimal FinalsRatio { get; init; }

    /// <summary>
    /// The ratio of cash prizes to the total entry fees collected in the tournament.
    /// </summary>
    public required decimal CashRatio { get; init; }
    
    /// <summary>
    /// The ratio of cash prizes for the Super Sweeper to the total entry fees collected in the tournament.
    /// </summary>
    public required decimal SuperSweeperCashRatio { get; init; }
}