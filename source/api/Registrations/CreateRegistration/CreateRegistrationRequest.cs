namespace NortheastMegabuck.Api.Registrations.CreateRegistration;

/// <summary>
/// Request to create a new registration.
/// </summary>
public sealed record CreateRegistrationRequest
{
    /// <summary>
    /// The registration input data for the new registration.
    /// This includes details about the bowler, squads, and other registration information.
    /// </summary>
    public required RegistrationInput Registration { get; init; }
}