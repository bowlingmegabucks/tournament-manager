using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Registrations.GetRegistrationById;

/// <summary>
/// Represents a query to retrieve a registration by its ID.
/// </summary>
public sealed record GetRegistrationByIdQuery
    : IQuery<Models.Registration?>
{
    /// <summary>
    /// Represents a query to retrieve a registration by its ID.
    /// </summary>
    public RegistrationId Id { get; init; }
}