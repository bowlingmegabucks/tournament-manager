using System.Security.Cryptography;
using System.Text;

namespace NortheastMegabuck.LaneAssignments;
internal class ViewModel : IViewModel
{
    public BowlerId BowlerId { get; }

    public string BowlerName { get; }

    public string DivisionName { get; }

    public int DivisionNumber { get; }

    public string LaneAssignment { get; }

    public int Average { get; }

    public int Handicap { get; }

    public ViewModel(Models.LaneAssignment laneAssignment)
    {
        BowlerId = laneAssignment.Bowler.Id;
        BowlerName = laneAssignment.Bowler.ToString();

        DivisionName = laneAssignment.Division.Name;
        DivisionNumber = laneAssignment.Division.Number;

        LaneAssignment = laneAssignment.Position;
        Average = laneAssignment.Average.GetValueOrDefault(0);
        Handicap = laneAssignment.Handicap;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    public ViewModel(string bowlerName, string divisionName, int average, int handicap)
    {
        BowlerId = BowlerId.New();

        BowlerName = bowlerName;
        DivisionName = divisionName;
        DivisionNumber = RandomNumberGenerator.GetInt32(1, 4);
        LaneAssignment = string.Empty;
        Average = average;
        Handicap = handicap;
    }

    public override string ToString()
        => new StringBuilder(LaneAssignment)
            .Append('\t').Append(BowlerId)
            .Append('t').Append(BowlerName)
            .Append('\t').Append(DivisionNumber)
            .Append('\t').Append(Handicap).ToString();
}

public interface IViewModel
{
    BowlerId BowlerId { get; }

    string BowlerName { get; }

    string DivisionName { get; }

    int DivisionNumber { get; }

    string LaneAssignment { get; }

    int Average { get; }

    int Handicap { get; }
}
