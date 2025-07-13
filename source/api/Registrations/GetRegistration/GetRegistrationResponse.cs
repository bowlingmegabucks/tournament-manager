namespace NortheastMegabuck.Api.Registrations.GetRegistration;

/// <summary>
/// Response for retrieving a specific registration.
/// </summary>
public sealed record GetRegistrationResponse
{
    /// <summary>
    /// The registration details for the requested registration.
    /// </summary>
    public required RegistrationDetailDto Registration { get; init; }
}