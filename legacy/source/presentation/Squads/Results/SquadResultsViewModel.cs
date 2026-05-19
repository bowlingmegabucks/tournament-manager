namespace BowlingMegabucks.TournamentManager.Squads.Results;

/// <summary>
/// Represents the view model for displaying squad results, including bowler and score details.
/// </summary>
internal class ViewModel : IViewModel
{
    /// <inheritdoc/>
    public short Place { get; }

    /// <inheritdoc/>
    public SquadId SquadId { get; }

    /// <inheritdoc/>
    public DateTime SquadDate { get; }

    /// <inheritdoc/>
    public string DivisionName { get; init; }

    /// <inheritdoc/>
    public string BowlerName { get; init; }

    /// <inheritdoc/>
    public int Score { get; }

    /// <inheritdoc/>
    public int ScratchScore { get; }

    /// <inheritdoc/>
    public int HighGame { get; }

    /// <inheritdoc/>
    public int HighGameScratch { get; }

    /// <inheritdoc/>
    public bool Advancer { get; }

    /// <inheritdoc/>
    public bool Casher { get; }

    /// <inheritdoc/>
    public int Handicap { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a bowler squad score.
    /// </summary>
    /// <param name="bowlerScore">The bowler squad score model to map from.</param>
    /// <param name="squadDate">The date of the squad.</param>
    /// <param name="place">The place/rank of the bowler.</param>
    /// <param name="advancer">Indicates if the bowler is an advancer.</param>
    /// <param name="casher">Indicates if the bowler is a casher.</param>
    /// <remarks>
    /// This constructor maps the properties from the domain model to the view model for presentation.
    /// </remarks>
    public ViewModel(Models.BowlerSquadScore bowlerScore, DateTime squadDate, short place, bool advancer, bool casher)
    {
        Place = place;
        SquadId = bowlerScore.SquadId;
        SquadDate = squadDate;
        DivisionName = bowlerScore.Division.Name;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        ScratchScore = bowlerScore.ScratchScore;
        HighGame = bowlerScore.HighGame;
        HighGameScratch = bowlerScore.HighGameScratch;
        Advancer = advancer;
        Casher = casher;
        Handicap = bowlerScore.Handicap;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {
        DivisionName = string.Empty;
        BowlerName = string.Empty;
    }
}

/// <summary>
/// Represents a view model interface for squad results.
/// </summary>
public interface IViewModel
{
    /// <summary>
    /// Gets a value indicating whether the bowler is an advancer.
    /// </summary>
    bool Advancer { get; }

    /// <summary>
    /// Gets the name of the bowler.
    /// </summary>
    string BowlerName { get; }

    /// <summary>
    /// Gets a value indicating whether the bowler is a casher.
    /// </summary>
    bool Casher { get; }

    /// <summary>
    /// Gets the name of the division.
    /// </summary>
    string DivisionName { get; }

    /// <summary>
    /// Gets the highest game score.
    /// </summary>
    int HighGame { get; }

    /// <summary>
    /// Gets the highest scratch game score.
    /// </summary>
    int HighGameScratch { get; }

    /// <summary>
    /// Gets the place/rank of the bowler.
    /// </summary>
    short Place { get; }

    /// <summary>
    /// Gets the total score.
    /// </summary>
    int Score { get; }

    /// <summary>
    /// Gets the total scratch score.
    /// </summary>
    int ScratchScore { get; }

    /// <summary>
    /// Gets the unique identifier for the squad.
    /// </summary>
    SquadId SquadId { get; }

    /// <summary>
    /// Gets the date of the squad.
    /// </summary>
    DateTime SquadDate { get; }

    /// <summary>
    /// Gets the handicap value for the bowler.
    /// </summary>
    int Handicap { get; }
}
