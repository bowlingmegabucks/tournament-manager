namespace BowlingMegabucks.TournamentManager.Scores;

/// <summary>
/// Represents a recap sheet view model for a bowler's scores and lane assignments.
/// </summary>
internal class RecapSheetViewModel : IRecapSheetViewModel
{
    /// <summary>
    /// Gets or sets the bowler's name.
    /// </summary>
    public string BowlerName { get; set; }

    /// <summary>
    /// Gets or sets the division name.
    /// </summary>
    public string DivisionName { get; set; }

    /// <summary>
    /// Gets or sets the cross (lane assignments per game).
    /// </summary>
    public IDictionary<short, string> Cross { get; set; }

    /// <summary>
    /// Gets or sets the handicap value.
    /// </summary>
    public int Handicap { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RecapSheetViewModel"/> class.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment view model.</param>
    /// <param name="cross">The collection of lane assignments for each game.</param>
    /// <remarks>
    /// This constructor populates the bowler's name, division, handicap, and cross (lane assignments per game) from the provided lane assignment and cross sequence.
    /// </remarks>
    public RecapSheetViewModel(LaneAssignments.IViewModel laneAssignment, IEnumerable<string> cross)
    {
        BowlerName = laneAssignment.BowlerName;
        DivisionName = laneAssignment.DivisionName;
        Handicap = laneAssignment.Handicap;

        Cross = new Dictionary<short, string>();

        short game = 1;

        foreach (var lane in cross)
        {
            Cross.Add(game++, lane);
        }
    }
}

/// <summary>
/// Represents a recap sheet view model interface for a bowler's scores and lane assignments.
/// </summary>
public interface IRecapSheetViewModel
{
    /// <summary>
    /// Gets the bowler's name.
    /// </summary>
    string BowlerName { get; }

    /// <summary>
    /// Gets the division name.
    /// </summary>
    string DivisionName { get; }

    /// <summary>
    /// Gets the cross (lane assignments per game).
    /// </summary>
    IDictionary<short, string> Cross { get; }

    /// <summary>
    /// Gets the handicap value.
    /// </summary>
    int Handicap { get; }
}