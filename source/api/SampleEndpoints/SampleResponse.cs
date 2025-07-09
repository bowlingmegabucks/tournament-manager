namespace NortheastMegabuck.Api.SampleEndpoints;

/// <summary>
/// Represents a response containing sample data.
/// </summary>
public record SampleResponse
{
    /// <summary>
    /// The unique identifier for the registration.
    /// </summary>
    public required RegistrationId RegistrationId { get; init; }

    /// <summary>
    /// The name of the person.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The age of the person.
    /// </summary>
    public required int Age { get; init; }

    /// <summary>
    /// A message containing the person's name and age.
    /// </summary>
    public string Message => $"Hello, {Name}! You are {Age} years old.";
}