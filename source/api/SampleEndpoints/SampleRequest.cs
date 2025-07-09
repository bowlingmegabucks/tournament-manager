using FastEndpoints;

namespace NortheastMegabuck.Api.SampleEndpoints;

/// <summary>
/// Represents a request for sample data.
/// </summary>
public record SampleRequest
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The age of the person.
    /// </summary>
    public required int Age { get; init; }

    /// <summary>
    /// A flag to force an error response for testing purposes.
    /// </summary>
    [FromHeader("X-Force-Error")]
    public bool ForceError { get; init; } = false;
}