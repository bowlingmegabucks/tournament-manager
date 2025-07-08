
namespace NortheastMegabuck.Models;

/// <summary>
/// 
/// </summary>
public class SquadScore
{
    /// <summary>
    /// 
    /// </summary>
    public SquadId SquadId { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime SquadDate { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public Bowler Bowler { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public short GameNumber { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public int Score { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public Division Division { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public int Handicap { get; init; }

    internal SquadScore(Database.Entities.SquadScore score, Squads.IHandicapCalculator handicapCalculator)
    {
        SquadId = score.SquadId;
        SquadDate = score.Squad.Date;
        Bowler = new Bowler(score.Bowler);
        GameNumber = score.Game;
        Score = score.Score;
        Division = new Division(score.Bowler.Registrations.Single().Division);

        Handicap = score.Squad is Database.Entities.SweeperSquad sweeper
            ? sweeper.Divisions.SingleOrDefault(division => division.DivisionId == Division.Id)?.BonusPinsPerGame.GetValueOrDefault(0) ?? 0
            : handicapCalculator.Calculate(score.Bowler.Registrations.Single());
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal SquadScore()
    {
        Bowler = new Bowler();
        Division = new Division();
    }

#if DEBUG
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
        => $"{Bowler}: Game {GameNumber}: {Score}";
#endif
}
