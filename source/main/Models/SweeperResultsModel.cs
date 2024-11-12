
namespace NortheastMegabuck.Models;
internal class SweeperResult
{
    public IEnumerable<BowlerSquadScore> Scores { get; init; } = [];

    public int CutScore { get; init; }

    public short CasherCount { get; init; }
}
