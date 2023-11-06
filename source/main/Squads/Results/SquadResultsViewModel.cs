
namespace NortheastMegabuck.Squads.Results;

internal class ViewModel : IViewModel
{
    public short Place { get; }

    public SquadId SquadId { get; }

    public DateTime SquadDate { get; }

    public string DivisionName { get; init; }

    public string BowlerName { get; init; }

    public int Score { get; }

    public int ScratchScore { get; }

    public int HighGame { get; }

    public int HighGameScratch { get; }

    public bool Advancer { get; }

    public bool Casher { get; }

    public int Handicap { get; }

    public ViewModel(Models.BowlerSquadScore bowlerScore, DateTime squadDate, short place, bool advancer, bool casher)
    {
        Place = place;
        SquadId = bowlerScore.SquadId;
        SquadDate = squadDate;
        DivisionName = bowlerScore.Division.Name;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        ScratchScore = bowlerScore.ScratchScore;
        HighGame = bowlerScore.HighGame;
        HighGameScratch = bowlerScore.HighGameScratch;
        Advancer = advancer;
        Casher = casher;
        Handicap = bowlerScore.Handicap;
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    internal ViewModel()
    {
        DivisionName = string.Empty;
        BowlerName = string.Empty;
    }
}

internal interface IViewModel
{
    bool Advancer { get; }

    string BowlerName { get; }

    bool Casher { get; }

    string DivisionName { get; }

    int HighGame { get; }

    int HighGameScratch { get; }

    short Place { get; }

    int Score { get; }

    int ScratchScore { get; }

    SquadId SquadId { get; }

    DateTime SquadDate { get; }

    int Handicap { get; }
}
