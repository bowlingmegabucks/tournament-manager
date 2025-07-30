namespace BowlingMegabucks.TournamentManager.Api.Sweepers;

/// <summary>
/// Data Transfer Object for retrieving detailed information about a sweeper in a tournament.
/// </summary>
public sealed record SweeperDetailDto
{
    /// <summary>
    /// The unique identifier for the sweeper.
    /// </summary>
    public required SquadId Id { get; init; }

    /// <summary>
    /// Date and time when the sweeper is scheduled to take place (Eastern Time).
    /// </summary>
    public required DateTime Date { get; init; }

    /// <summary>
    /// Entry fee for the sweeper.
    /// </summary>
    public required decimal EntryFee { get; init; }

    /// <summary>
    /// Number of games bowled during the sweeper.
    /// </summary>
    public required short Games { get; init; }
}

internal static class SweeperDetailDtoExtensions
{
    /// <summary>
    /// Converts a sweeper model to a SweeperDetailDto.
    /// </summary>
    /// <param name="sweeper">The sweeper model.</param>
    /// <returns>A SweeperDetailDto containing the sweeper details.</returns>
    public static SweeperDetailDto ToDto(this Models.Sweeper sweeper)
    {
        return new SweeperDetailDto
        {
            Id = sweeper.Id,
            Date = sweeper.Date,
            EntryFee = sweeper.EntryFee,
            Games = sweeper.Games
        };
    }
}