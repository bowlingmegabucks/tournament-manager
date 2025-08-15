using BowlingMegabucks.TournamentManager.Abstractions.Messaging;

namespace BowlingMegabucks.TournamentManager.Tournaments.GetTournamentById;

/// <summary>
/// Represents a query to retrieve a registration by its ID.
/// </summary>
public sealed record GetRegistrationByIdQuery
    : IQuery<Models.Registration?>
{
    /// <summary>
/// Represents a query to retrieve a registration by its ID.
/// </summary>
public sealed record GetTournamentByIdQuery
    : IQuery<Models.Tournament?>
{
    /// <summary>
    /// The ID of the registration to retrieve.
    /// </summary>
    public RegistrationId Id { get; init; }
}