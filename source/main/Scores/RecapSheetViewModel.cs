
namespace NortheastMegabuck.Scores;
internal class RecapSheetViewModel : IRecapSheetViewModel
{
    public string BowlerName { get; set; }

    public string DivisionName { get; set; }

    public IDictionary<short, string> LaneAssignments { get; set; }

    public int Handicap { get; set; }

    public RecapSheetViewModel(LaneAssignments.IViewModel laneAssignment, IEnumerable<string> laneAssignments)
    {
        BowlerName = laneAssignment.BowlerName;
        DivisionName = laneAssignment.DivisionName;
        Handicap = laneAssignment.Handicap;

        LaneAssignments = new Dictionary<short, string>();

        short game = 1;

        foreach (var lane in laneAssignments)
        {
            LaneAssignments.Add(game++, lane);
        }
    }
}

internal interface IRecapSheetViewModel
{
    string BowlerName { get; }

    string DivisionName { get; }

    IDictionary<short, string> LaneAssignments { get; }

    int Handicap { get; }
}