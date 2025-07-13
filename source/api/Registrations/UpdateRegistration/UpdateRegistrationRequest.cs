using FastEndpoints;

namespace NortheastMegabuck.Api.Registrations.UpdateRegistration;

/// <summary>
/// Request for updating a registration.
/// </summary>
public sealed record UpdateRegistrationRequest
{
    /// <summary>
    /// The ID of the registration to update.  This comes from the route parameter.
    /// </summary>
    public required RegistrationId Id { get; init; }

    /// <summary>
    /// The registration to update.
    /// </summary>
    public required UpdateRegistrationInput Registration { get; init; }
}