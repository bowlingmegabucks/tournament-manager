using BowlingMegabucks.TournamentManager.Api.Divisions;
using BowlingMegabucks.TournamentManager.Api.Squads;
using BowlingMegabucks.TournamentManager.Api.Sweepers;
using BowlingMegabucks.TournamentManager.Models;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.GetRegistration;

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

internal static class RegistrationExtensions
{
    public static RegistrationDetailDto ToDto(this Registration registration)
        => new()
        {
            Id = registration.Id,
            TournamentId = registration.TournamentId,
            Bowler = registration.Bowler.ToDto(),
            Division = registration.Division.ToDto(),
            Squads = registration.Squads.Select(squad => squad.ToDto()),
            Sweepers = registration.Sweepers.Select(sweeper => sweeper.ToDto()),
            SuperSweeper = registration.SuperSweeper
        };
}