
namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public class TournamentFinalsSeeding
{
    /// <summary>
    /// 
    /// </summary>
    public Division Division { get; init; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> Qualifiers { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> NonQualifiers { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerId> AtLargeCashers { get; init; } = [];
}
