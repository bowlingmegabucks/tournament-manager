
namespace NortheastMegabuck.Models;
internal class SweeperCut
{
    public SquadId SquadId { get; set; }

    public IEnumerable<SquadScore> Scores { get; set; } = Enumerable.Empty<SquadScore>();

    public int CutScore { get; set; }

    public short CasherCount { get; set; }
}
