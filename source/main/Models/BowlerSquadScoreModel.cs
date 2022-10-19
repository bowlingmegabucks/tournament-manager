
namespace NortheastMegabuck.Models;
internal class BowlerSquadScore
{
    public Bowler Bowler { get; set; }

    public SquadId SquadId { get; set; }

    public IDictionary<short, int> Scores { get; set; }

    public BowlerSquadScore(IGrouping<Bowler, SquadScore> bowlerScores)
    {
        Bowler = bowlerScores.Key;
        SquadId = bowlerScores.First().SquadId;
        Scores = bowlerScores.ToDictionary(score => score.GameNumber, score => score.Score);
    }
}
