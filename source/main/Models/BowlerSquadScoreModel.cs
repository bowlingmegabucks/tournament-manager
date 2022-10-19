
namespace NortheastMegabuck.Models;
internal class BowlerSquadScore : IEquatable<BowlerSquadScore>, IComparable<BowlerSquadScore>
{
    public Bowler Bowler { get; set; }

    public SquadId SquadId { get; set; }

    public IDictionary<short, int> GameScores { get; set; }

    public int Score
        => GameScores.Sum(gameScore => gameScore.Value);

    public int HighGame
        => GameScores.Max(gameScore => gameScore.Value);

    public BowlerSquadScore(IGrouping<Bowler, SquadScore> bowlerScores)
    {
        Bowler = bowlerScores.Key;
        SquadId = bowlerScores.First().SquadId;
        GameScores = bowlerScores.ToDictionary(score => score.GameNumber, score => score.Score);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal BowlerSquadScore()
    {
        Bowler = new Bowler();
        GameScores = new Dictionary<short, int>();
    }


    public bool Equals(BowlerSquadScore? other)
        => other != null && Bowler.Id == other.Bowler.Id && SquadId == other.SquadId;

    public override bool Equals(object? obj)
        => Equals(obj as BowlerSquadScore);

    public override int GetHashCode()
        => Bowler.Id.GetHashCode() ^ SquadId.GetHashCode();
    public int CompareTo(BowlerSquadScore? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (GameScores.Count != other.GameScores.Count)
        {
            return other.GameScores.Count.CompareTo(GameScores.Count);
        }

        if (Score != other.Score)
        {
            return other.Score.CompareTo(Score);
        }

        if (HighGame != other.HighGame)
        {
            return other.HighGame.CompareTo(HighGame);
        }

        var scores = GameScores.Select(score=> score.Value).OrderByDescending(score => score).ToList();
        var otherScores = other.GameScores.Select(score=> score.Value).OrderByDescending(score => score).ToList();

        for (var i = 1; i < GameScores.Count; i++)
        {
            if (scores[i] != otherScores[i])
            {
                return otherScores[i].CompareTo(scores[i]);
            }
        }

        return 0;
    }
}
