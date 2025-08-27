using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Domain.Tournaments;

internal static class RatioErrors
{
    public static Error RatioMustBeGreaterThanOne
        => Error.Validation(
            code: "Ratio.RatioMustBeGreaterThanOne",
            description: "Ratio must be greater than one.");
}
