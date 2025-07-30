using System.Collections.ObjectModel;
using BowlingMegabucks.TournamentManager.Api.Divisions;
using BowlingMegabucks.TournamentManager.Api.Squads;
using BowlingMegabucks.TournamentManager.Api.Sweepers;

namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournament;

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

internal static class TournamentDetailDtoExtensions
{
    /// <summary>
    /// Converts a tournament model to a TournamentDetailDto.
    /// </summary>
    /// <param name="tournament">The tournament model.</param>
    /// <returns>A TournamentDetailDto containing the tournament details.</returns>
    public static TournamentDetailDto ToDto(this Models.Tournament tournament)
    {
        return new TournamentDetailDto
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.Start,
            EndDate = tournament.End,
            EntryFee = tournament.EntryFee,
            BowlingCenter = tournament.BowlingCenter,
            Divisions = tournament.Divisions.Select(d => d.ToDto()).ToList().AsReadOnly(),
            Squads = tournament.Squads.Select(s => s.ToDto()).ToList().AsReadOnly(),
            Sweepers = tournament.Sweepers.Select(s => s.ToDto()).ToList().AsReadOnly()
        };
    }
}