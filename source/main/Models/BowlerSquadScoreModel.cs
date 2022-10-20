
namespace NortheastMegabuck.Models;
internal class BowlerSquadScore : IEquatable<BowlerSquadScore>, IComparable<BowlerSquadScore>
{
    public SquadId SquadId { get; set; }

    public Bowler Bowler { get; set; }

    public Division Division { get; set; }

    internal ILookup<short, int> GameScores { get; set; }

    public int ScratchScore
        => GameScores.SelectMany(gameScore=> gameScore).Sum();

    public int Score
        =>  ScratchScore + (_handicap * GameScores.Count);

    public int HighGame
        => GameScores.SelectMany(gameScore => gameScore).Max();

    private readonly int _handicap;

    public BowlerSquadScore(IGrouping<Bowler, SquadScore> bowlerScores)
    {
        Bowler = bowlerScores.Key;
        Division = bowlerScores.First().Division;
        SquadId = bowlerScores.First().SquadId;
        GameScores = bowlerScores.ToLookup(score => score.GameNumber, score => score.Score);

        _handicap = bowlerScores.First().Handicap;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal BowlerSquadScore(params int[] games)
    {
        Bowler = new Bowler();
        Division = new Division();

        short i = 1;

        var gameScores = new Dictionary<short, int>();

        foreach (var game in games)
        {
            gameScores.Add(i++, game);
        }

        GameScores = gameScores.ToLookup(gameScore => gameScore.Key, gameScore => gameScore.Value);
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

        var scores = GameScores.SelectMany(score=> score).OrderByDescending(score => score).ToList();
        var otherScores = other.GameScores.SelectMany(score=> score).OrderByDescending(score => score).ToList();

        for (var i = 1; i < GameScores.Count; i++)
        {
            if (scores[i] + _handicap != otherScores[i] + _handicap)
            {
                return otherScores[i].CompareTo(scores[i]);
            }
        }

        return 0;
    }
}
