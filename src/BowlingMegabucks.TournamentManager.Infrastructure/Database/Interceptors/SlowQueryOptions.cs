using System.Diagnostics.CodeAnalysis;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Database.Interceptors;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class SlowQueryOptions
{
    public int ThresholdMilliseconds { get; init; } = 100;
}
