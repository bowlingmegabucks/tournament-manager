
namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class SweeperResult
{
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> Scores { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public int CutScore { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public short CasherCount { get; init; }
}