using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;

/// <summary>
/// Represents a query to retrieve a registration by its ID.
/// </summary>
public sealed record GetRegistrationByIdQuery
    : IQuery<Models.Registration?>
{
    /// <summary>
    /// The ID of the registration to retrieve.
    /// </summary>
    public RegistrationId Id { get; init; }
}