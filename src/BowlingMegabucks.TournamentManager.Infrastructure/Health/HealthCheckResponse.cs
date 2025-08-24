
namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

/// <summary>
/// Represents the overall health check response, including the global status and details for each health check component.
/// </summary>
/// <remarks>
/// <para>Example:</para>
/// <code language="json">
/// {
///   "status": "Healthy",
///   "details": {
///     "database": {
///       "status": "Healthy",
///       "description": "Database connection is healthy.",
///       "duration": "00:00:00.1234567"
///     },
///     "redis": {
///       "status": "Unhealthy",
///       "description": "Redis is not reachable.",
///       "duration": "00:00:00.0100000"
///     }
///   }
/// }
/// </code>
/// </remarks>
public sealed record HealthCheckResponse
{
    /// <summary>
    /// The overall status of the health check (e.g., "Healthy", "Unhealthy").
    /// </summary>
    /// <example>Healthy</example>
    public required string Status { get; init; }

    /// <summary>
    /// A dictionary containing the results of individual health checks, keyed by component name.
    /// </summary>
    public required IEnumerable<HealthCheckDetail> Details { get; init; }
}
