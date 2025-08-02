namespace BowlingMegabucks.TournamentManager.Tournaments.Seeding;
/// <summary>
/// Represents the view model for tournament seeding results.
/// </summary>
internal class ViewModel(int seed, bool qualified, bool atLargeCasher, Models.BowlerSquadScore bowlerScore) : IViewModel
{
    /// <inheritdoc/>
    public string DivisionName { get; } = bowlerScore.Division.Name;

    /// <inheritdoc/>
    public int Seed { get; } = seed;

    /// <inheritdoc/>
    public string BowlerName { get; } = bowlerScore.Bowler.ToString();

    /// <inheritdoc/>
    public int Score { get; } = bowlerScore.Score;

    /// <inheritdoc/>
    public int HighGame { get; } = bowlerScore.HighGame;

    /// <inheritdoc/>
    public bool Qualified { get; } = qualified;

    /// <inheritdoc/>
    public bool AtLargeCasher { get; } = atLargeCasher;
}

/// <summary>
/// Defines the contract for a tournament seeding view model.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets the name of the division.
    /// </summary>
    string DivisionName { get; }

    /// <summary>
    /// Gets the seed number of the bowler.
    /// </summary>
    int Seed { get; }

    /// <summary>
    /// Gets the name of the bowler.
    /// </summary>
    string BowlerName { get; }

    /// <summary>
    /// Gets the total score of the bowler.
    /// </summary>
    int Score { get; }

    /// <summary>
    /// Gets the highest game score of the bowler.
    /// </summary>
    int HighGame { get; }

    /// <summary>
    /// Gets a value indicating whether the bowler is qualified.
    /// </summary>
    bool Qualified { get; }

    /// <summary>
    /// Gets a value indicating whether the bowler is an at-large casher.
    /// </summary>
    bool AtLargeCasher { get; }
}