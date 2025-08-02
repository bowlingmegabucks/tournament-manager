namespace BowlingMegabucks.TournamentManager.Sweepers.Results;

/// <summary>
/// Represents the view model for displaying sweeper results, including bowler and score details.
/// </summary>
internal class ViewModel : IViewModel
{
    /// <inheritdoc/>
    public short Place { get; }

    /// <inheritdoc/>
    public string BowlerName { get; }

    /// <inheritdoc/>
    public int Score { get; }

    /// <inheritdoc/>
    public int ScratchScore { get; }

    /// <inheritdoc/>
    public int HighGame { get; }

    /// <inheritdoc/>
    public int HighGameScratch { get; }

    /// <inheritdoc/>
    public bool Casher { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a bowler squad score.
    /// </summary>
    /// <param name="bowlerScore">The bowler squad score model to map from.</param>
    /// <param name="place">The place/rank of the bowler.</param>
    /// <param name="cashingPositions">The number of cashing positions.</param>
    /// <remarks>
    /// This constructor maps the properties from the domain model to the view model for presentation.
    /// </remarks>
    internal ViewModel(Models.BowlerSquadScore bowlerScore, short place, int cashingPositions)
    {
        Place = place;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        ScratchScore = bowlerScore.ScratchScore;
        HighGame = bowlerScore.HighGame;
        HighGameScratch = bowlerScore.HighGameScratch;
        Casher = place <= cashingPositions;
    }
}

/// <summary>
/// Represents a view model interface for sweeper results.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets the place/rank of the bowler.
    /// </summary>
    short Place { get; }

    /// <summary>
    /// Gets the name of the bowler.
    /// </summary>
    string BowlerName { get; }

    /// <summary>
    /// Gets the total score.
    /// </summary>
    int Score { get; }

    /// <summary>
    /// Gets the total scratch score.
    /// </summary>
    int ScratchScore { get; }

    /// <summary>
    /// Gets the highest game score.
    /// </summary>
    int HighGame { get; }

    /// <summary>
    /// Gets the highest scratch game score.
    /// </summary>
    int HighGameScratch { get; }

    /// <summary>
    /// Gets a value indicating whether the bowler is a casher.
    /// </summary>
    bool Casher { get; }
}
