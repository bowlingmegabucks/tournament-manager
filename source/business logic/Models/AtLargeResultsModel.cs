
namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class AtLargeResults
{
    internal DivisionId DivisionId { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerId> AdvancersWhoPreviouslyCashed { get; init; } = [];
}
