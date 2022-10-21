
namespace NortheastMegabuck.Models;
internal class SweeperResults
{
    public IEnumerable<BowlerSquadScore> Scores { get; set; } = Enumerable.Empty<BowlerSquadScore>();

    public int CutScore { get; set; }

    public short CasherCount { get; set; }
}
