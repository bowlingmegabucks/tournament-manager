namespace BowlingMegabucks.TournamentManager.Domain;

/// <summary>
/// Represents a date range with a start date and end date.
/// </summary>
public sealed record DateOnlyRange
{
    /// <summary>
    /// Gets the start date of the range.
    /// </summary>
    public DateOnly StartDate { get; init; }

    /// <summary>
    /// Gets the end date of the range.
    /// </summary>
    public DateOnly EndDate { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DateOnlyRange"/> class.
    /// </summary>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <exception cref="InvalidDateRangeException{DateOnly}">
    /// Thrown when <paramref name="endDate"/> is before <paramref name="startDate"/>.
    /// </exception>
    public DateOnlyRange(DateOnly startDate, DateOnly endDate)
    {
        if (endDate < startDate)
        {
            throw new InvalidDateRangeException<DateOnly>(startDate, endDate);
        }

        StartDate = startDate;
        EndDate = endDate;
    }
}
