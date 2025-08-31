using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

public sealed record TournamentSummaryViewModel
{
    /// <summary>
    /// The unique identifier of the tournament.
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
    /// The name of the bowling center hosting the tournament.
    /// </summary>
    public required string BowlingCenter { get; init; }

    /// <summary>
    /// The entry fee for the tournament.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// Indicates whether the tournament is completed.
    /// </summary>
    public required bool Completed { get; init; }
}
