using Ardalis.GuardClauses;
using BowlingMegabucks.TournamentManager.Domain.Abstractions;
using ErrorOr;

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
        SuperSweeperCashRatio = null!;
    }

    private Tournament(
        string name,
        DateOnlyRange tournamentDates,
        decimal entryFee,
        short games,
        Ratio finalsRatio,
        Ratio cashRatio,
        Ratio superSweeperCashRatio,
        string bowlingCenter)
        : this()
    {
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.NegativeOrZero(entryFee);
        Guard.Against.NegativeOrZero(games);
        Guard.Against.NullOrWhiteSpace(bowlingCenter);

        Name = name;
        TournamentDates = tournamentDates;
        EntryFee = entryFee;
        Games = games;
        FinalsRatio = finalsRatio;
        CashRatio = cashRatio;
        SuperSweeperCashRatio = superSweeperCashRatio;
        BowlingCenter = bowlingCenter;
    }

    /// <summary>
    /// The maximum length of the tournament name.
    /// </summary>
    public const int MaxNameLength = 150;

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
    /// Gets the standard ratio of cash prizes awarded to Super Sweeper participants.
    /// </summary>
    /// <value>The standard ratio of cash prizes awarded to Super Sweeper participants.</value>
    public Ratio SuperSweeperCashRatio { get; }

    /// <summary>
    /// The maximum length of the bowling center name.
    /// </summary>
    public const int MaxBowlingCenterLength = 150;

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
    /// Creates a new tournament with the specified parameters.
    /// </summary>
    /// <param name="name">The name of the tournament. Must not be null or empty and cannot exceed <see cref="MaxNameLength"/> characters.</param>
    /// <param name="tournamentDates">The date range when the tournament takes place.</param>
    /// <param name="entryFee">The entry fee for the tournament.</param>
    /// <param name="games">The number of games in the tournament. Must be greater than zero.</param>
    /// <param name="finalsRatio">The ratio used for determining finalists.</param>
    /// <param name="cashRatio">The ratio used for determining cash payouts.</param>
    /// <param name="superSweeperCashRatio">The ratio used for determining cash payouts for Super Sweeper participants.</param>
    /// <param name="bowlingCenter">The name of the bowling center hosting the tournament. Must not be null or empty and cannot exceed <see cref="MaxBowlingCenterLength"/> characters.</param>
    /// <returns>
    /// An <see cref="ErrorOr{T}"/> containing either a valid <see cref="Tournament"/>
    /// or validation errors if any parameters are invalid.
    /// </returns>
    public static ErrorOr<Tournament> Create(
        string name,
        DateOnlyRange tournamentDates,
        decimal entryFee,
        short games,
        Ratio finalsRatio,
        Ratio cashRatio,
        Ratio superSweeperCashRatio,
        string bowlingCenter)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return TournamentErrors.TournamentNameIsRequired;
        }

        if (name.Length > MaxNameLength)
        {
            return TournamentErrors.TournamentNameIsTooLong(name);
        }

        if (games <= 0)
        {
            return TournamentErrors.TournamentGamesMustBeGreaterThanZero;
        }

        if (string.IsNullOrWhiteSpace(bowlingCenter))
        {
            return TournamentErrors.TournamentBowlingCenterIsRequired;
        }

        if (bowlingCenter.Length > MaxBowlingCenterLength)
        {
            return TournamentErrors.TournamentBowlingCenterIsTooLong(bowlingCenter);
        }

        return new Tournament(
            name,
            tournamentDates,
            entryFee,
            games,
            finalsRatio,
            cashRatio,
            superSweeperCashRatio,
            bowlingCenter);
    }
}
