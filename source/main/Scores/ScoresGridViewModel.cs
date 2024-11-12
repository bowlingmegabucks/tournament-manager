
using System.Globalization;

namespace NortheastMegabuck.Scores;

internal class GridViewModel : IGridViewModel
{
    public BowlerId BowlerId { get; set; }

    public string LaneAssignment { get; set; }

    public string BowlerName { get; set; }

    public IDictionary<short, int> Scores { get; set; }

    public GridViewModel(LaneAssignments.IViewModel laneAssignment)
    {
        BowlerId = laneAssignment.BowlerId;
        LaneAssignment = laneAssignment.LaneAssignment;
        BowlerName = laneAssignment.BowlerName;
        Scores = new Dictionary<short, int>();
    }

    public GridViewModel(string clipboardData, short gamesPerSquad)
    {
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

public interface IGridViewModel
{
    BowlerId BowlerId { get; set; }

    string LaneAssignment { get; set; }

    string BowlerName { get; set; }

    IDictionary<short, int> Scores { get; }
}
