namespace BowlingMegabucks.TournamentManager.Api.Registrations.DeleteRegistration;

/// <summary>
/// Represents a request to delete a registration.
/// </summary>
public sealed record DeleteRegistrationRequest
{
    /// <summary>
    /// The unique identifier of the registration to delete.
    /// </summary>
    public required RegistrationId Id { get; init; }
}