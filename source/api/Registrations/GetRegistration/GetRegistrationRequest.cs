namespace NortheastMegabuck.Api.Registrations.GetRegistration;

/// <summary>
/// 
/// </summary>
public sealed record GetRegistrationRequest
{
    /// <summary>
    /// The unique identifier of the registration to retrieve.
    /// </summary>
    public required RegistrationId Id { get; init; }
}