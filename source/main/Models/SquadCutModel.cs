
namespace NortheastMegabuck.Models;
internal class SweeperCut
{
    public IEnumerable<BowlerSquadScore> Scores { get; set; } = Enumerable.Empty<BowlerSquadScore>();

    public int CutScore { get; set; }

    public short CasherCount { get; set; }
}
