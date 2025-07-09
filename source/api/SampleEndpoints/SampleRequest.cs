using FastEndpoints;

namespace NortheastMegabuck.Api.SampleEndpoints;

/// <summary>
/// Represents a request for sample data.
/// </summary>
public class SampleRequest
{
    /// <summary>
    /// The name of the person.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The age of the person.
    /// </summary>
    public int Age { get; init; }

    /// <summary>
    /// A flag to force an error response for testing purposes.
    /// </summary>
    [FromHeader("x-force-error", isRequired: false)]
    public bool? ForceError { get; init; }
}