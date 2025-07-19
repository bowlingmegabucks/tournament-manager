
namespace BowlingMegabucks.TournamentManager.Sweepers.Results;
internal class ViewModel : IViewModel
{
    public short Place { get; }

    public string BowlerName { get; }

    public int Score { get; }

    public int ScratchScore { get; }

    public int HighGame { get; }

    public int HighGameScratch { get; }

    public bool Casher { get; }

    internal ViewModel(Models.BowlerSquadScore bowlerScore, short place, int cashingPositions)
    {
        Place = place;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        ScratchScore = bowlerScore.ScratchScore;
        HighGame = bowlerScore.HighGame;
        HighGameScratch = bowlerScore.HighGameScratch;
        Casher = place <= cashingPositions;
    }
}

internal interface IViewModel
{
    short Place { get; }

    string BowlerName { get; }

    int Score { get; }

    int ScratchScore { get; }

    int HighGame { get; }

    int HighGameScratch { get; }

    bool Casher { get; }
}
