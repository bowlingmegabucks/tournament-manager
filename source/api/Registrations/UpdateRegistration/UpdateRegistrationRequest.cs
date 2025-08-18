using FastEndpoints;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

/// <summary>
/// Request for updating a registration.
/// </summary>
public sealed record UpdateRegistrationRequest
{
    /// <summary>
    /// The registration to update.
    /// </summary>
    public required UpdateRegistrationInput Registration { get; init; }
}