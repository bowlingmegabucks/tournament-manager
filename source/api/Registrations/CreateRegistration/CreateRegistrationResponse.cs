namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

/// <summary>
/// Response for creating a new registration.
/// This response contains the ID of the newly created registration.
/// </summary>
public sealed record CreateRegistrationResponse
{
    /// <summary>
    /// The ID of the newly created registration.
    /// </summary>
    public required RegistrationId RegistrationId { get; init; }
}