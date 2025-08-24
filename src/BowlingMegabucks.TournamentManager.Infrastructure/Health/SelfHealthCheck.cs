using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

internal sealed class SelfHealthCheck
    : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
        => Task.FromResult(
            HealthCheckResult.Healthy("Basic liveness check for Tournament Manager API.")
        );
}
