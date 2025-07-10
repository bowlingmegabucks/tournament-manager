namespace NortheastMegabuck.Api.Registrations.GetRegistration;

/// <summary>
/// Response for retrieving a specific registration.
/// </summary>
public sealed record GetRegistrationResponse
{
    /// <summary>
    /// The unique identifier of the registration being retrieved.
    /// </summary>
    public required RegistrationId Id { get; init; }
}