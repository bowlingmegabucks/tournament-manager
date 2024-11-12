
namespace NortheastMegabuck.Tournaments.Results;

internal class AtLargeViewModel : IAtLargeViewModel
{
    public short Place { get; }

    public string BowlerName { get; }

    public string DivisionName { get; }

    public string SquadDate { get; }

    public bool PreviousCasher { get; }

    public int Score { get; }

    public int ScratchScore { get; }

    public int HighGame { get; }

    public int HighGameScratch { get; }

    public AtLargeViewModel(short place, Models.BowlerSquadScore result, bool previousCasher)
    {
        Place = place;
        BowlerName = result.Bowler.ToString();
        DivisionName = result.Division.Name;
        SquadDate = result.SquadDate.ToString("MM/dd hh:mm tt");
        PreviousCasher = previousCasher;
        Score = result.Score;
        ScratchScore = result.ScratchScore;
        HighGame = result.HighGame;
        HighGameScratch = result.HighGameScratch;
    }
}

internal interface IAtLargeViewModel
{
    short Place { get; }

    string BowlerName { get; }

    string DivisionName { get; }

    string SquadDate { get; }

    bool PreviousCasher { get; }

    int Score { get; }

    int ScratchScore { get; }

    int HighGame { get; }

    int HighGameScratch { get; }
}