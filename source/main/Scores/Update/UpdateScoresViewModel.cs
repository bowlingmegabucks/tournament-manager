
using System.Globalization;

namespace NortheastMegabuck.Scores.Update;
internal class ViewModel : IViewModel
{
    public BowlerId BowlerId { get; set; }

    public string LaneAssignment { get; set; }

    public string BowlerName { get; set; }

    public IDictionary<int, int> Scores { get; set; }

    public ViewModel(LaneAssignments.IViewModel laneAssignment)
    {
        BowlerId = laneAssignment.BowlerId;
        LaneAssignment = laneAssignment.LaneAssignment;
        BowlerName = laneAssignment.BowlerName;
        Scores = new Dictionary<int, int>();
    }
}

public interface IViewModel
{ 
    BowlerId BowlerId { get; set; }

    string LaneAssignment { get; set; }

    string BowlerName { get; set; }

    IDictionary<int, int> Scores { get; set; }
}
