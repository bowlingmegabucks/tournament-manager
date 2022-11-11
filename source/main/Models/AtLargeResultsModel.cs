
namespace NortheastMegabuck.Models;

internal class AtLargeResults
{
    internal DivisionId DivisionId { get; init; }

    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = Enumerable.Empty<BowlerSquadScore>();

    public IEnumerable<BowlerId> AdvancersWhoPreviouslyCashed { get; init; } = Enumerable.Empty<BowlerId>();
}
