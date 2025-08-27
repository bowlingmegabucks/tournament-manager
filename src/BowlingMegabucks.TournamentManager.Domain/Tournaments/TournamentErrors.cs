using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Domain.Tournaments;

internal static class TournamentErrors
{
    public static Error TournamentNameIsRequired
        => Error.Validation(
            code: "Tournament.TournamentNameIsRequired",
            description: "Tournament name is required.");

    public static Error TournamentNameIsTooLong(string value)
        => Error.Validation(
            code: "Tournament.TournamentNameIsTooLong",
            description: "Tournament name exceeds maximum length.",
            metadata: new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "MaxLength", Tournament.MaxNameLength },
                { "ActualLength", value.Length },
            });

    public static Error TournamentGamesMustBeGreaterThanZero
        => Error.Validation(
            code: "Tournament.TournamentGamesMustBeGreaterThanZero",
            description: "Tournament games must be greater than zero.");

    public static Error TournamentBowlingCenterIsRequired
        => Error.Validation(
            code: "Tournament.TournamentBowlingCenterIsRequired",
            description: "Tournament bowling center is required.");

    public static Error TournamentBowlingCenterIsTooLong(string value)
        => Error.Validation(
            code: "Tournament.TournamentBowlingCenterIsTooLong",
            description: "Tournament bowling center exceeds maximum length.",
            metadata: new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { "MaxLength", Tournament.MaxBowlingCenterLength },
                { "ActualLength", value.Length },
            });
}
