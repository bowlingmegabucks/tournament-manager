using System.Collections.ObjectModel;
using NortheastMegabuck.Api.Divisions;
using NortheastMegabuck.Api.Squads;
using NortheastMegabuck.Api.Sweepers;

namespace NortheastMegabuck.Api.Tournaments.GetTournament;

/// <summary>
/// Data Transfer Object for retrieving detailed tournament information.
/// </summary>
public sealed record TournamentDetailDto
{
    /// <summary>
    /// The unique identifier for the tournament.
    /// </summary>
    public required TournamentId Id { get; init; }

    /// <summary>
    /// The name of the tournament.
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// The start date of the tournament.
    /// </summary>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// The end date of the tournament.
    /// </summary>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// The entry fee for the tournament.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// The bowling center hosting the tournament.
    /// </summary>
    public required string BowlingCenter { get; init; }

    /// <summary>
    /// The divisions available in the tournament.
    /// </summary>
    public required ReadOnlyCollection<DivisionDetailDto> Divisions { get; init; }

    /// <summary>
    /// The squads available in the tournament.
    /// </summary>
    public required ReadOnlyCollection<SquadDetailDto> Squads { get; init; }

    /// <summary>
    /// The sweepers available in the tournament.
    /// </summary>
    public required ReadOnlyCollection<SweeperDetailDto> Sweepers { get; init; }
}