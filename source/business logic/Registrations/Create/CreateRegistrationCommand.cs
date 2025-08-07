using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Registrations.Create;

/// <summary>
/// Command to create a new registration.
/// </summary>
public record CreateRegistrationCommand
    : ICommand<RegistrationId>
{ 
    /// <summary>
    /// Gets or sets the registration details.
    /// </summary>
    public required CreateRegistrationInput Registration { get; init; }
}