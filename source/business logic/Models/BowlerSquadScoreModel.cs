
using System.Diagnostics;

namespace BowlingMegabucks.TournamentManager.Models;

/// <summary>
/// 
/// </summary>
[DebuggerDisplay("{Bowler}: {GameScoreTotal}")]
public sealed class BowlerSquadScore : IEquatable<BowlerSquadScore>, IComparable<BowlerSquadScore>
{
    /// <summary>
    /// 
    /// </summary>
    public SquadId SquadId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime SquadDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Bowler Bowler { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Division Division { get; set; }

    internal ILookup<short, int> GameScores { get; set; }

    private int GameScoreTotal
        => GameScores.SelectMany(score => score).Sum();

    /// <summary>
    /// 
    /// </summary>
    public int ScratchScore
        => GameScores.SelectMany(gameScore => gameScore).Sum();

    /// <summary>
    /// 
    /// </summary>
    public int Score
        => ScratchScore + (Handicap * GameScores.SelectMany(gameScore => gameScore).Count());

    /// <summary>
    /// 
    /// </summary>
    public int HighGame
        => HighGameScratch + Handicap;

    /// <summary>
    /// 
    /// </summary>
    public int HighGameScratch
        => GameScores.SelectMany(gameScore => gameScore).Max();

    /// <summary>
    /// 
    /// </summary>
    public int Handicap { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bowlerScores"></param>
    public BowlerSquadScore(IGrouping<Bowler, SquadScore> bowlerScores)
    {
        ArgumentNullException.ThrowIfNull(bowlerScores);

        Bowler = bowlerScores.Key;
        Division = bowlerScores.First().Division;
        SquadId = bowlerScores.First().SquadId;
        SquadDate = bowlerScores.First().SquadDate;
        GameScores = bowlerScores.ToLookup(score => score.GameNumber, score => score.Score);

        Handicap = bowlerScores.First().Handicap;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="bowlerId"></param>
    /// <param name="games"></param>
    internal BowlerSquadScore(BowlerId bowlerId, params int[] games)
    {
        Bowler = new Bowler { Id = bowlerId };
        Division = new Division();

        short i = 1;

        var gameScores = new Dictionary<short, int>();

        foreach (var game in games)
        {
            gameScores.Add(i++, game);
        }

        GameScores = gameScores.ToLookup(gameScore => gameScore.Key, gameScore => gameScore.Value);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal BowlerSquadScore(params int[] games) : this(BowlerId.New(), games)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
        => Equals(obj as BowlerSquadScore);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(BowlerSquadScore? other)
        => other != null && Bowler.Id == other.Bowler.Id && SquadId == other.SquadId;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator ==(BowlerSquadScore? left, BowlerSquadScore? right)
        => left is null ? right is null : left.Equals(right);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator !=(BowlerSquadScore? left, BowlerSquadScore? right)
        => !(left == right);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >(BowlerSquadScore? left, BowlerSquadScore? right)
        => left is null ? right is not null : left.CompareTo(right) > 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <(BowlerSquadScore? left, BowlerSquadScore? right)
        => left is null ? right is null : left.CompareTo(right) < 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator >=(BowlerSquadScore? left, BowlerSquadScore? right)
        => left is null || left.CompareTo(right) >= 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static bool operator <=(BowlerSquadScore? left, BowlerSquadScore? right)
        => left is null ? right is null : left.CompareTo(right) <= 0;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
        => Bowler.Id.GetHashCode() ^ SquadId.GetHashCode();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
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

        var scores = GameScores.SelectMany(score => score).OrderByDescending(score => score).ToList();
        var otherScores = other.GameScores.SelectMany(score => score).OrderByDescending(score => score).ToList();

        for (var i = 1; i < GameScores.Count; i++)
        {
            if (scores[i] + Handicap != otherScores[i] + Handicap)
            {
                return otherScores[i].CompareTo(scores[i]);
            }
        }

        return 0;
    }
}
