using NortheastMegabuck.Api.Divisions;
using NortheastMegabuck.Api.Squads;
using NortheastMegabuck.Api.Sweepers;

namespace NortheastMegabuck.Api.Registrations.GetRegistration;

/// <summary>
/// 
/// </summary>
public sealed record RegistrationDetailDto
{
    /// <summary>
    /// The unique identifier for the registration.
    /// </summary>
    public required RegistrationId Id { get; init; }

    /// <summary>
    /// The unique identifier for the tournament associated with the registration.
    /// </summary>
    public required TournamentId TournamentId { get; init; }

    /// <summary>
    /// The bowler's information associated with the registration.
    /// </summary>
    public required BowlerDetailDto Bowler { get; init; }

    /// <summary>
    /// The division in which the bowler is registered.
    /// </summary>
    public required DivisionDetailDto Division { get; init; }

    /// <summary>
    /// A list of squads in which the bowler is registered.
    /// </summary>
    public required IEnumerable<SquadDetailDto> Squads { get; init; }

    /// <summary>
    /// A list of sweepers associated with the registration.
    /// </summary>
    public required IEnumerable<SweeperDetailDto> Sweepers { get; init; }

    /// <summary>
    /// Indicates whether the bowler is registered for the Super Sweeper event.
    /// </summary>
    public required bool SuperSweeper { get; init; }
}