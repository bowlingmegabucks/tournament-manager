
namespace NortheastMegabuck.Models;

internal class AtLargeResults
{
    internal DivisionId DivisionId { get; init; }

    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = [];

    public IEnumerable<BowlerId> AdvancersWhoPreviouslyCashed { get; init; } = [];
}
