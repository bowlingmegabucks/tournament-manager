namespace BowlingMegabucks.TournamentManager.Api.Tournaments.GetTournaments;

/// <summary>
/// Data Transfer Object for retrieving tournament details.
/// </summary>
public sealed record TournamentSummaryDto
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
    /// The date when the tournament starts.
    /// </summary>
    public required DateOnly StartDate { get; init; }

    /// <summary>
    /// The date when the tournament ends.
    /// </summary>
    public required DateOnly EndDate { get; init; }

    /// <summary>
    /// The entry fee for the tournament.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// The bowling center where the tournament is held.
    /// </summary>
    public required string BowlingCenter { get; init; }
}

internal static class TournamentSummaryDtoExtensions
{
    /// <summary>
    /// Converts a tournament model to a TournamentSummaryDto.
    /// </summary>
    /// <param name="tournament">The tournament model.</param>
    /// <returns>A TournamentSummaryDto containing the tournament details.</returns>
    public static TournamentSummaryDto ToDto(this Models.Tournament tournament)
    {
        return new TournamentSummaryDto
        {
            Id = tournament.Id,
            Name = tournament.Name,
            StartDate = tournament.Start,
            EndDate = tournament.End,
            EntryFee = tournament.EntryFee,
            BowlingCenter = tournament.BowlingCenter
        };
    }
}