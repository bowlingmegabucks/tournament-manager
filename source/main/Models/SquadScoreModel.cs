
using NortheastMegabuck.Scores;

namespace NortheastMegabuck.Models;
internal class SquadScore
{
    public SquadId SquadId { get; init; }

    public Bowler Bowler { get; init; }

    public short GameNumber { get; init; }

    public int Score { get; init; }

    public Division Division { get; init; }

    public int Handicap { get; init; }

    public SquadScore(IViewModel viewModel)
    {
        SquadId = viewModel.SquadId;
        Bowler = new Bowler { Id = viewModel.BowlerId };
        GameNumber = viewModel.GameNumber;
        Score = viewModel.Score;
        Division = new Division();
    }
    
    /// <summary>
    /// Sweeper Score
    /// </summary>
    /// <param name="score"></param>
    public SquadScore(Database.Entities.SquadScore score, Squads.IHandicapCalculator handicapCalculator)
    {
        SquadId = score.SquadId;
        Bowler = new Bowler(score.Bowler);
        GameNumber = score.Game;
        Score = score.Score;
        Division = new Division(score.Bowler.Registrations.Single().Division);
        
        Handicap = score.Squad is Database.Entities.SweeperSquad sweeper
            ? sweeper.Divisions.SingleOrDefault(division => division.DivisionId == Division.Id)?.BonusPinsPerGame.GetValueOrDefault(0) ?? 0
            : handicapCalculator.Calculate(score.Bowler.Registrations.Single());
    }

    /// <summary>
    /// Unit Test Constuctor
    /// </summary>
    internal SquadScore()
    {
        Bowler = new Bowler();
        Division = new Division();
    }
}
