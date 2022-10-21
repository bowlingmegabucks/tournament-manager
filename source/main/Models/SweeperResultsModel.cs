
namespace NortheastMegabuck.Models;
internal class SweeperResult
{
    public IEnumerable<BowlerSquadScore> Scores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public int CutScore { get; init; }

    public short CasherCount { get; init; }
}
