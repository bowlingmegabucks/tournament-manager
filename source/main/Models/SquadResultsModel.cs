
namespace NortheastMegabuck.Models;
internal class SquadResult
{
    public Squad Squad { get; init; }

    public Division Division { get; init; }

    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> CashingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> NonQualifyingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> AtLargeEligibleScores
        => CashingScores.Union(NonQualifyingScores);

    public int Entries
        => AdvancingScores.Count() + CashingScores.Count() + NonQualifyingScores.Count();

    public int CutScore
        => AdvancingScores.Last().Score;

    public int CashScore
        => CashingScores.Last().Score;

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal SquadResult()
    {
        Squad = new Squad();
        Division = new Division();
    }
}
