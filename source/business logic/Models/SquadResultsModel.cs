
namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
public class SquadResult
{
    /// <summary>
    /// 
    /// </summary>
    public Squad Squad { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public Division Division { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> AdvancingScores { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> CashingScores { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> NonQualifyingScores { get; init; } = [];

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<BowlerSquadScore> AtLargeEligibleScores
        => CashingScores.Union(NonQualifyingScores);

    internal IEnumerable<BowlerSquadScore> Scores
        => AdvancingScores.Union(CashingScores).Union(NonQualifyingScores);

    /// <summary>
    /// 
    /// </summary>
    public int Entries
        => AdvancingScores.Count() + CashingScores.Count() + NonQualifyingScores.Count();

    /// <summary>
    /// 
    /// </summary>
    public int CutScore
        => AdvancingScores.Last().Score;

    /// <summary>
    /// 
    /// </summary>
    public int CashScore
        => CashingScores.Last().Score;

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal SquadResult()
    {
        Squad = new Squad();
        Division = new Division();
    }

#if DEBUG
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => $"{Division.Name} - {Squad.Date}";
#endif
}
