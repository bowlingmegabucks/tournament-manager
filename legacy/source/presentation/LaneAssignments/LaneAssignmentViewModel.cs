using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using BowlingMegabucks.TournamentManager.Squads;

namespace BowlingMegabucks.TournamentManager.LaneAssignments;

/// <summary>
/// Represents a view model for lane assignments and registrations.
/// </summary>
internal class ViewModel : IViewModel
{
    /// <inheritdoc/>
    public BowlerId BowlerId { get; internal set; }

    /// <inheritdoc/>
    public string BowlerName { get; internal set; }

    /// <inheritdoc/>
    public string DivisionName { get; }

    /// <inheritdoc/>
    public int DivisionNumber { get; }

    /// <inheritdoc/>
    public string LaneAssignment { get; internal set; }

    /// <inheritdoc/>
    public int Average { get; }

    /// <inheritdoc/>
    public int Handicap { get; }

    private readonly bool? _superSweeper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a lane assignment.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment model.</param>
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

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class from a registration.
    /// </summary>
    /// <param name="registration">The registration model.</param>
    public ViewModel(Models.Registration registration) : this(registration, new HandicapCalculator()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class for unit testing.
    /// </summary>
    /// <param name="registration">The registration model.</param>
    /// <param name="handicapCalculator">The handicap calculator.</param>
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
    /// Initializes a new instance of the <see cref="ViewModel"/> class for unit testing.
    /// </summary>
    /// <param name="bowlerName">The bowler's name.</param>
    /// <param name="divisionName">The division name.</param>
    /// <param name="average">The average score.</param>
    /// <param name="handicap">The handicap value.</param>
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
    /// Initializes a new instance of the <see cref="ViewModel"/> class for unit testing.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment string.</param>
    internal ViewModel(string laneAssignment)
    {
        BowlerName = string.Empty;
        DivisionName = string.Empty;
        LaneAssignment = laneAssignment;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModel"/> class for unit testing.
    /// </summary>
    internal ViewModel() : this(string.Empty)
    {

    }

    /// <summary>
    /// Returns a string representation of the view model using the current lane assignment.
    /// </summary>
    /// <returns>A string representation of the view model.</returns>
    /// <remarks>
    /// Calls <see cref="ToString(string)"/> with the current lane assignment.
    /// </remarks>
    public override string ToString()
        => ToString(LaneAssignment);

    /// <summary>
    /// Returns a string representation of the view model using the specified lane assignment.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment string.</param>
    /// <returns>A string representation of the view model.</returns>
    /// <remarks>
    /// Includes bowler ID, name, division, and handicap.
    /// </remarks>
    public string ToString(string laneAssignment)
        => new StringBuilder(laneAssignment)
            .Append('\t').Append(BowlerId)
            .Append('\t').Append(BowlerName)
            .Append('\t').Append(_superSweeper.HasValue ? IsSuperSweeper(_superSweeper.Value) : DivisionNumber)
            .Append('\t').Append(Handicap).ToString();

    private static string IsSuperSweeper(bool superSweeper)
        => superSweeper ? "Y" : "N";

    /// <summary>
    /// Compares this view model to another by lane number and letter.
    /// </summary>
    /// <param name="other">The other view model to compare to.</param>
    /// <returns>An integer indicating the relative order.</returns>
    /// <remarks>
    /// Compares by lane number first, then by lane letter.
    /// </remarks>
    public int CompareTo(IViewModel? other)
    {
        ArgumentNullException.ThrowIfNull(other);

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

/// <summary>
/// Represents a lane assignment or registration view model.
/// </summary>
public interface IViewModel 
    : IComparable<IViewModel>
{
    /// <summary>
    /// Gets the bowler identifier.
    /// </summary>
    BowlerId BowlerId { get; }

    /// <summary>
    /// Gets the bowler's name.
    /// </summary>
    string BowlerName { get; }

    /// <summary>
    /// Gets the division name.
    /// </summary>
    string DivisionName { get; }

    /// <summary>
    /// Gets the division number.
    /// </summary>
    int DivisionNumber { get; }

    /// <summary>
    /// Gets the lane assignment string.
    /// </summary>
    string LaneAssignment { get; }

    /// <summary>
    /// Gets the average score.
    /// </summary>
    int Average { get; }

    /// <summary>
    /// Gets the handicap value.
    /// </summary>
    int Handicap { get; }

    /// <summary>
    /// Returns a string representation of the view model using the specified lane assignment.
    /// </summary>
    /// <param name="laneAssignment">The lane assignment string.</param>
    /// <returns>A string representation of the view model.</returns>
    string ToString(string laneAssignment);
}

internal static class Extensions
{
    /// <summary>
    /// Gets the lane number from the view model's lane assignment.
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    /// <returns>The lane number.</returns>
    internal static short LaneNumber(this IViewModel viewModel)
        => Convert.ToInt16(viewModel.LaneAssignment.Substring(0, viewModel.LaneAssignment.Length - 1), CultureInfo.CurrentCulture);

    /// <summary>
    /// Gets the lane letter from the view model's lane assignment.
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    /// <returns>The lane letter.</returns>
    internal static string LaneLetter(this IViewModel viewModel)
        => viewModel.LaneAssignment.Substring(viewModel.LaneAssignment.Length - 1, 1);
}
