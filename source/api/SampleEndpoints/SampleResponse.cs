namespace NortheastMegabuck.Api.SampleEndpoints;

/// <summary>
/// Represents a response containing sample data.
/// </summary>
public class SampleResponse
{
    /// <summary>
    /// The unique identifier for the registration.
    /// </summary>
    public RegistrationId RegistrationId { get; set; }

    /// <summary>
    /// The name of the person.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The age of the person.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// A message containing the person's name and age.
    /// </summary>
    public string Message => $"Hello, {Name}! You are {Age} years old.";
}