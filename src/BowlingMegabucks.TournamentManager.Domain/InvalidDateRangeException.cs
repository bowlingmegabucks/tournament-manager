using System.Globalization;

namespace BowlingMegabucks.TournamentManager.Domain;

/// <summary>
/// Exception thrown when a date range is invalid (e.g., start date is after end date).
/// </summary>
/// <typeparam name="T">The date type, must implement IComparable and have a parameterless constructor.</typeparam>
public sealed class InvalidDateRangeException<T>
    : Exception
    where T : struct, IComparable<T>, IFormattable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDateRangeException{T}"/> class.
    /// </summary>
    /// <param name="startDate">The start date of the invalid range.</param>
    /// <param name="endDate">The end date of the invalid range.</param>
    public InvalidDateRangeException(T startDate, T endDate)
        : base(CreateMessage(startDate, endDate))
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDateRangeException{T}"/> class.
    /// </summary>
    public InvalidDateRangeException()
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDateRangeException{T}"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidDateRangeException(string message)
        : base(message)
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidDateRangeException{T}"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public InvalidDateRangeException(string message, Exception innerException)
        : base(message, innerException)
    { }

    /// <summary>
    /// Gets the start date of the invalid range.
    /// </summary>
    public T StartDate { get; }

    /// <summary>
    /// Gets the end date of the invalid range.
    /// </summary>
    public T EndDate { get; }

    /// <summary>
    /// Creates the error message for the exception.
    /// </summary>
    /// <param name="startDate">The start date of the invalid range.</param>
    /// <param name="endDate">The end date of the invalid range.</param>
    /// <returns>A formatted error message.</returns>
    private static string CreateMessage(T startDate, T endDate)
    {
        string format = typeof(T) == typeof(DateOnly) ? "yyyy-MM-dd" : "yyyy-MM-dd HH:mm:ss";

        return $"Invalid date range: start date '{startDate.ToString(format, CultureInfo.InvariantCulture)}' " +
               $"must be before or equal to end date '{endDate.ToString(format, CultureInfo.InvariantCulture)}'.";
    }
}
