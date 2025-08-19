using FastEndpoints;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

/// <summary>
/// Request for updating a registration.
/// </summary>
public sealed record UpdateRegistrationRequest
{
    /// <summary>
    /// The unique identifier of the registration.
    /// </summary>
    /// <value></value>
    [BindFrom("Id")]
    public required RegistrationId RegistrationId { get; init; }

    /// <summary>
    /// The registration to update.
    /// </summary>
    [FromBody]
    public required UpdateRegistrationInput Registration { get; init; }
}