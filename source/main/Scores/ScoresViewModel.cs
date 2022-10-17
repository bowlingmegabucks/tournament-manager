
namespace NortheastMegabuck.Scores;

internal class ViewModel : IViewModel
{
    public BowlerId BowlerId { get; set; }

    public string LaneAssignment { get; set; }

    public string BowlerName { get; set; }

    public IDictionary<short, int> Scores { get; set; }

    public ViewModel(LaneAssignments.IViewModel laneAssignment)
    {
        BowlerId = laneAssignment.BowlerId;
        LaneAssignment = laneAssignment.LaneAssignment;
        BowlerName = laneAssignment.BowlerName;
        Scores = new Dictionary<short, int>();
    }

    public ViewModel(string clipboardData, short gamesPerSquad)
    {
        Scores = new Dictionary<short, int>();

        var items = clipboardData.Split('\t');

        LaneAssignment = items[0];
        BowlerId = new BowlerId(new Guid(items[1]));
        BowlerName = items[2];

        short currentGame = 1;

        for (var i = 5; currentGame <= gamesPerSquad && i < items.Length; i++)
        {
            Scores.Add(currentGame++, Convert.ToInt32(items[i]));
        }
    }
}

public interface IViewModel
{ 
    BowlerId BowlerId { get; set; }

    string LaneAssignment { get; set; }

    string BowlerName { get; set; }

    IDictionary<short, int> Scores { get; set; }
}
