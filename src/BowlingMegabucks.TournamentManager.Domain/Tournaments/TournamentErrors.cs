using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Domain.Tournaments;

/// <summary>
/// Provides error definitions related to tournament validation and lookup operations.
/// </summary>
public static class TournamentErrors
{
    internal static Error TournamentNameIsRequired
        => Error.Validation(
            code: "Tournament.TournamentNameIsRequired",
            description: "Tournament name is required.");

    internal static Error TournamentNameIsTooLong(string value)
        => Error.Validation(
            code: "Tournament.TournamentNameIsTooLong",
            description: "Tournament name exceeds maximum length.",
            metadata: new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "MaxLength", Tournament.MaxNameLength },
                { "ActualLength", value.Length },
            });

    internal static Error TournamentGamesMustBeGreaterThanZero
        => Error.Validation(
            code: "Tournament.TournamentGamesMustBeGreaterThanZero",
            description: "Tournament games must be greater than zero.");

    internal static Error TournamentBowlingCenterIsRequired
        => Error.Validation(
            code: "Tournament.TournamentBowlingCenterIsRequired",
            description: "Tournament bowling center is required.");

    internal static Error TournamentBowlingCenterIsTooLong(string value)
        => Error.Validation(
            code: "Tournament.TournamentBowlingCenterIsTooLong",
            description: "Tournament bowling center exceeds maximum length.",
            metadata: new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "MaxLength", Tournament.MaxBowlingCenterLength },
                { "ActualLength", value.Length },
            });

    /// <summary>
    /// Returns an error indicating that a tournament with the specified identifier was not found.
    /// </summary>
    /// <param name="id">The unique identifier of the tournament that was not found.</param>
    /// <returns>An <see cref="Error"/> representing the not found result.</returns>
    public static Error TournamentNotFound(TournamentId id)
        => Error.NotFound(
            code: "Tournament.TournamentNotFound",
            description: "Tournament was not found.",
            metadata: new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "TournamentId", id }
            });

}
