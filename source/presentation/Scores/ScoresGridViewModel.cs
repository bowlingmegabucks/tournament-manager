using System.Globalization;

namespace BowlingMegabucks.TournamentManager.Scores;

/// <summary>
/// Represents the grid view model for displaying bowler scores and lane assignments.
/// </summary>
public class GridViewModel 
    : IGridViewModel
{
    /// <inheritdoc/>
    public BowlerId BowlerId { get; set; }

    /// <inheritdoc/>
    public string LaneAssignment { get; set; }

    /// <inheritdoc/>
    public string BowlerName { get; set; }

    /// <inheritdoc/>
    public IDictionary<short, int> Scores { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GridViewModel"/> class using a lane assignment view model.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment view model.</param>
    public GridViewModel(LaneAssignments.IViewModel laneAssignment)
    {
        ArgumentNullException.ThrowIfNull(laneAssignment);

        BowlerId = laneAssignment.BowlerId;
        LaneAssignment = laneAssignment.LaneAssignment;
        BowlerName = laneAssignment.BowlerName;
        Scores = new Dictionary<short, int>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GridViewModel"/> class using clipboard data and the number of games per squad.
    /// </summary>
    /// <param name="clipboardData">The clipboard data string, tab-delimited.</param>
    /// <param name="gamesPerSquad">The number of games per squad.</param>
    /// <remarks>
    /// This constructor parses the clipboard data to populate the bowler and score information.
    /// </remarks>
    public GridViewModel(string clipboardData, short gamesPerSquad)
    {
        ArgumentNullException.ThrowIfNull(clipboardData);

        Scores = new Dictionary<short, int>();

        var items = clipboardData.Split('\t');

        LaneAssignment = items[0];
        BowlerId = new BowlerId(new Guid(items[1]));
        BowlerName = items[2];

        short currentGame = 1;

        for (var i = 5; currentGame <= gamesPerSquad && i < items.Length; i++)
        {
            Scores.Add(currentGame++, Convert.ToInt32(items[i], CultureInfo.CurrentCulture));
        }
    }
}

/// <summary>
/// Represents a grid view model interface for bowler scores and lane assignments.
/// </summary>
public interface IGridViewModel
{
    /// <summary>
    /// Gets or sets the unique identifier for the bowler.
    /// </summary>
    BowlerId BowlerId { get; set; }

    /// <summary>
    /// Gets or sets the lane assignment for the bowler.
    /// </summary>
    string LaneAssignment { get; set; }

    /// <summary>
    /// Gets or sets the name of the bowler.
    /// </summary>
    string BowlerName { get; set; }

    /// <summary>
    /// Gets the scores for each game, keyed by game number.
    /// </summary>
    IDictionary<short, int> Scores { get; }
}
