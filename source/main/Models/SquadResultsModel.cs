
namespace NortheastMegabuck.Models;
internal class SquadResult
{
    public Squad Squad { get; init; } = null!;

    public Division Division { get; init; } = null!;

    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> CashingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerSquadScore> NonQualifyingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public int Entries
        => AdvancingScores.Count() + CashingScores.Count() + NonQualifyingScores.Count();

    public int CutScore
        => AdvancingScores.Last().Score;

    public int CashScore
        => CashingScores.Last().Score;
}
