using Ardalis.GuardClauses;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Domain.Tournaments;

/// <summary>
/// Represents a ratio value that must be greater than one.
/// </summary>
public sealed record Ratio
{
    /// <summary>
    /// Gets the ratio value.
    /// </summary>
    public decimal Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ratio"/> class.
    /// </summary>
    /// <param name="value">The ratio value, which must be greater than one.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="value"/> is not greater than one.
    /// </exception>
    private Ratio(decimal value)
    {
        Guard.Against.InvalidInput(value, nameof(value), x => x > 1, "Ratio must be greater than one.");

        Value = value;
    }

    /// <summary>
    /// Creates a new ratio with validation.
    /// </summary>
    /// <param name="value">The ratio value to validate.</param>
    /// <returns>
    /// An <see cref="ErrorOr{T}"/> containing either a valid <see cref="Ratio"/>
    /// or an error if the value is not greater than one.
    /// </returns>
    public static ErrorOr<Ratio> Create(decimal value)
    {
        if (value <= 1)
        {
            return RatioErrors.RatioMustBeGreaterThanOne;
        }

        return new Ratio(value);
    }
}
