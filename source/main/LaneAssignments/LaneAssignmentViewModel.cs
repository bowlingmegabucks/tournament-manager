using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using NortheastMegabuck.LaneAssignments;
using NortheastMegabuck.Models;
using NortheastMegabuck.Squads;

namespace NortheastMegabuck.LaneAssignments;
internal class ViewModel : IViewModel
{
    public BowlerId BowlerId { get; internal set; }

    public string BowlerName { get; internal set; }

    public string DivisionName { get; }

    public int DivisionNumber { get; }

    public string LaneAssignment { get; internal set; }

    public int Average { get; }

    public int Handicap { get; }

    private readonly bool? _superSweeper;

    public ViewModel(Models.LaneAssignment laneAssignment)
    {
        BowlerId = laneAssignment.Bowler.Id;
        BowlerName = laneAssignment.Bowler.ToString();

        DivisionName = laneAssignment.Division.Name;
        DivisionNumber = laneAssignment.Division.Number;

        LaneAssignment = laneAssignment.Position;
        Average = laneAssignment.Average.GetValueOrDefault(0);
        Handicap = laneAssignment.Handicap;

        _superSweeper = laneAssignment.SuperSweeper;
    }

    public ViewModel(Models.Registration registration) : this(registration, new HandicapCalculator()) { }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="registration"></param>
    /// <param name="handicapCalculator"></param>
    internal ViewModel(Models.Registration registration, IHandicapCalculator handicapCalculator)
    {
        BowlerId = registration.Bowler.Id;
        BowlerName = registration.Bowler.ToString();

        DivisionName = registration.Division.Name;
        DivisionNumber = registration.Division.Number;

        LaneAssignment = string.Empty;
        Average = registration.Average.GetValueOrDefault(0);
        Handicap = handicapCalculator.Calculate(registration);

        _superSweeper = registration.SuperSweeper;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel(string bowlerName, string divisionName, int average, int handicap)
    {
        BowlerId = BowlerId.New();

        BowlerName = bowlerName;
        DivisionName = divisionName;
        DivisionNumber = RandomNumberGenerator.GetInt32(1, 4);
        LaneAssignment = string.Empty;
        Average = average;
        Handicap = handicap;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel(string laneAssignment)
    {
        BowlerName = string.Empty;
        DivisionName = string.Empty;
        LaneAssignment = laneAssignment;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel() : this(string.Empty)
    {

    }

    public override string ToString()
        => ToString(LaneAssignment);

    public string ToString(string laneAssignment)
        => new StringBuilder(laneAssignment)
            .Append('\t').Append(BowlerId)
            .Append('\t').Append(BowlerName)
            .Append('\t').Append(_superSweeper.HasValue ? _superSweeper.Value ? "Y" : "N" : DivisionNumber)
            .Append('\t').Append(Handicap).ToString();

    public int CompareTo(IViewModel? other)
    {
        if (other == null)
        {
            throw new ArgumentNullException(nameof(other));
        }

        var lane = this.LaneNumber();
        var otherLane = other.LaneNumber();

        if (lane != otherLane)
        {
            return lane.CompareTo(otherLane);
        }

        var letter = this.LaneLetter();
        var otherLetter = other.LaneLetter();

        return string.CompareOrdinal(letter, otherLetter);
    }
}

public interface IViewModel : IComparable<IViewModel>
{
    BowlerId BowlerId { get; }

    string BowlerName { get; }

    string DivisionName { get; }

    int DivisionNumber { get; }

    string LaneAssignment { get; }

    int Average { get; }

    int Handicap { get; }

    string ToString(string laneAssignment);
}

internal static class Extensions
{
    internal static short LaneNumber(this IViewModel viewModel)
        => Convert.ToInt16(viewModel.LaneAssignment.Substring(0, viewModel.LaneAssignment.Length - 1));

    internal static string LaneLetter(this IViewModel viewModel)
        => viewModel.LaneAssignment.Substring(viewModel.LaneAssignment.Length - 1, 1);
}
