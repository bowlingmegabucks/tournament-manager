using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.Application.Tournaments.GetTournamentById;

/// <summary>
/// Data transfer object representing the details of a bowling tournament.
/// </summary>
public sealed record TournamentDetailDto
{
    /// <summary>
    /// Gets the unique identifier for the tournament.
    /// </summary>
    public required TournamentId Id { get; init; }

    /// <summary>
    /// Gets the name of the tournament.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the start date of the tournament.
    /// </summary>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// Gets the end date of the tournament.
    /// </summary>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// Gets the number of games in a squad in the tournament.
    /// </summary>
    /// <value></value>
    public required short Games { get; init; }

    /// <summary>
    /// Gets the entry fee for the tournament.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// Gets the name of the bowling center hosting the tournament.
    /// </summary>
    public required string BowlingCenter { get; init; }

    /// <summary>
    /// Gets the standard ratio of players who advance to the finals.
    /// </summary>
    public required decimal FinalsRatio { get; init; }

    /// <summary>
    /// Gets the standard ratio of cash prizes awarded in the tournament.
    /// </summary>
    public required decimal CashRatio { get; init; }

    /// <summary>
    /// Gets the standard ratio of cash prizes awarded to Super Sweeper participants.
    /// </summary>
    public required decimal SuperSweeperCashRatio { get; init; }

    /// <summary>
    /// Gets a value indicating whether the tournament has been completed.
    /// </summary>
    /// <value><langword>true</langword> if the tournament has been completed; otherwise, <langword>false</langword>.</value>
    public required bool Completed { get; init; }
}
