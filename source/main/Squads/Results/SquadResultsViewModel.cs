
namespace NortheastMegabuck.Squads.Results;

internal class ViewModel : IViewModel
{
    public short Place { get; }

    public SquadId SquadId { get; }

    public DateTime SquadDate { get; }

    public DivisionId DivisionId { get; }

    public string DivisionName { get; }

    public string BowlerName { get; }

    public int Score { get; }

    public int ScratchScore { get; }

    public int HighGame { get; }

    public int HighGameScratch { get; }

    public bool Advancer { get; }

    public bool Casher { get; }

    internal ViewModel(Models.BowlerSquadScore bowlerScore, DateTime squadDate, short place, short advancerPositions, short cashingPositions)
    {
        Place = place;
        SquadId = bowlerScore.SquadId;
        SquadDate = squadDate;
        DivisionId = bowlerScore.Division.Id;
        DivisionName = bowlerScore.Division.Name;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        ScratchScore = bowlerScore.ScratchScore;
        HighGame = bowlerScore.HighGame;
        HighGameScratch = bowlerScore.HighGameScratch;
        Advancer = place <= advancerPositions;
        Casher = !Advancer && place <= cashingPositions;
    }
}

internal interface IViewModel
{
    bool Advancer { get; }

    string BowlerName { get; }

    bool Casher { get; }

    DivisionId DivisionId { get; }

    string DivisionName { get; }

    int HighGame { get; }

    int HighGameScratch { get; }

    short Place { get; }

    int Score { get; }

    int ScratchScore { get; }

    SquadId SquadId { get; }

    DateTime SquadDate { get; }
}
