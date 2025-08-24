namespace BowlingMegabucks.TournamentManager.Infrastructure.Health;

/// <summary>
/// Represents the result of a single health check, including its status, description, and execution duration.
/// </summary>
/// <remarks>
/// <para>Example:</para>
/// <code language="json">
/// {
///   "status": "Healthy",
///   "description": "Database connection is healthy.",
///   "duration": "00:00:00.1234567"
/// }
/// </code>
/// </remarks>
public sealed record HealthCheckDetail
{
    /// <summary>
    /// The name of the health check.
    /// </summary>
    /// <example>Database</example>
    public required string Name { get; init; }

    /// <summary>
    /// The status of the health check (e.g., "Healthy", "Unhealthy").
    /// </summary>
    /// <example>Healthy</example>
    public required string Status { get; init; }

    /// <summary>
    /// A description providing additional details about the health check result.
    /// </summary>
    /// <example>Database connection is healthy.</example>
    public required string? Description { get; init; }

    /// <summary>
    /// The time taken to execute the health check.
    /// </summary>
    /// <example>00:00:00.1234567</example>
    public required TimeSpan Duration { get; init; }
}
