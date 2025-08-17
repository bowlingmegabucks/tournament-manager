using BowlingMegabucks.TournamentManager.Abstractions.Messaging;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Registrations.DeleteRegistration;

/// <summary>
/// Represents a command to delete a registration.
/// </summary>
public sealed record DeleteRegistrationCommand
    : ICommand<Deleted>
{
    /// <summary>
    /// Identifier of the registration to delete.
    /// </summary>
    public required RegistrationId Id { get; init; }
}