
namespace NortheastMegabuck.Scores;
internal class RecapSheetViewModel : IRecapSheetViewModel
{
    public string BowlerName { get; set; }

    public string DivisionName { get; set; }

    public IDictionary<short, string> Cross { get; set; }

    public int Handicap { get; set; }

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

internal interface IRecapSheetViewModel
{
    string BowlerName { get; }

    string DivisionName { get; }

    IDictionary<short, string> Cross { get; }

    int Handicap { get; }
}