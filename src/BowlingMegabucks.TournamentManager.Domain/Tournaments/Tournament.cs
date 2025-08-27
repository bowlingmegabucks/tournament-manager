using BowlingMegabucks.TournamentManager.Domain.Abstractions;

namespace BowlingMegabucks.TournamentManager.Domain.Tournaments;

/// <summary>
/// Represents a bowling tournament.
/// </summary>
public sealed class Tournament
    : Entity<TournamentId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Tournament"/> class. Needed for EF Core.
    /// </summary>
    private Tournament()
        : base(TournamentId.New())
    {
        // These will be set by EF Core
        TournamentDates = null!;
        FinalsRatio = null!;
        CashRatio = null!;
    }

    /// <summary>
    /// Gets the name of the tournament.
    /// </summary>
    /// <value>The name of the tournament.</value>
    public string Name { get; } = string.Empty;

    /// <summary>
    /// Gets the dates of the tournament.
    /// </summary>
    /// <value>The dates of the tournament.</value>
    public DateOnlyRange TournamentDates { get; }

    /// <summary>
    /// Gets the entry fee for the tournament.
    /// </summary>
    /// <value>The entry fee for the tournament.</value>
    public decimal EntryFee { get; }

    /// <summary>
    /// Gets the number of games during qualifying in the tournament.
    /// </summary>
    /// <value>The number of games during qualifying in the tournament.</value>
    public short Games { get; }

    /// <summary>
    /// Gets the standard ratio of players who advance to the finals.
    /// </summary>
    /// <value>The standard ratio of players who advance to the finals.</value>
    public Ratio FinalsRatio { get; }

    /// <summary>
    /// Gets the standard ratio of cash prizes awarded in the tournament.
    /// </summary>
    /// <value>The standard ratio of cash prizes awarded in the tournament.</value>
    public Ratio CashRatio { get; }

    /// <summary>
    /// Gets the name of the bowling center hosting the tournament.
    /// </summary>
    /// <value>The name of the bowling center hosting the tournament.</value>
    public string BowlingCenter { get; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the tournament has been completed.
    /// </summary>
    /// <value><langword>true</langword> if the tournament has been completed; otherwise, <langword>false</langword>.</value>
    public bool Completed { get; }

    /// <summary>
    /// This is a temporary method for creating a new tournament.
    /// </summary>
    /// <returns>A new instance of the <see cref="Tournament"/> class.</returns>
    public static Tournament Create()
        => new();
}
