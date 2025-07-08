namespace NortheastMegabuck.Models;

/// <summary>
/// Represents the results of a tournament division, including squad and at-large results.
/// </summary>
public class TournamentResults
{
    /// <summary>
    /// Gets the division for which the results are reported.
    /// </summary>
    public Division Division { get; init; }

    /// <summary>
    /// Gets the total number of entries across all squads in the division.
    /// </summary>
    public int Entries
        => SquadResults.Sum(squadResult => squadResult.Entries);

    /// <summary>
    /// Gets the results for each squad in the division.
    /// </summary>
    public IEnumerable<SquadResult> SquadResults { get; init; } = [];

    /// <summary>
    /// Gets the at-large results for the division.
    /// </summary>
    public AtLargeResults AtLarge { get; init; } = null!;

    /// <summary>
    /// Initializes a new instance of the <see cref="TournamentResults"/> class with the specified division, squad results, and at-large results.
    /// </summary>
    /// <param name="division">The division for which results are reported.</param>
    /// <param name="squadResults">The results for each squad in the division.</param>
    /// <param name="atLarge">The at-large results for the division.</param>
    public TournamentResults(Division division, IEnumerable<SquadResult> squadResults, AtLargeResults atLarge)
    {
        Division = division;
        SquadResults = squadResults;
        AtLarge = atLarge;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TournamentResults"/> class for unit testing.
    /// </summary>
    internal TournamentResults()
    {
        Division = new Division();
    }
}
