
namespace NortheastMegabuck.Models;
internal class SquadResult
{
    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> CashingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> NonQualifyingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public int CutScore
        => AdvancingScores.Last().Score;

    public int CashScore
        => CashingScores.Last().Score;
}
