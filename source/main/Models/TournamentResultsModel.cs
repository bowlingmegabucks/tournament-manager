
namespace NortheastMegabuck.Models;

internal class TournamentResults
{
    public Division Division { get; init; }

    public int Entries
        => SquadResults.Sum(squadResult => squadResult.Entries);

    public IEnumerable<SquadResult> SquadResults { get; init; } = Enumerable.Empty<SquadResult>();

    public AtLargeResults AtLarge { get; init; } = null!;

    public TournamentResults(Division division, IEnumerable<SquadResult> squadResults, AtLargeResults atLarge)
    {
        Division = division;
        SquadResults = squadResults;
        AtLarge = atLarge;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal TournamentResults()
    {
        Division = new Division();
    }
}
