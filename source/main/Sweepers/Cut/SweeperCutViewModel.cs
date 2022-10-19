
namespace NortheastMegabuck.Sweepers.Cut;
internal class ViewModel : IViewModel
{
    public short Place { get; }

    public string BowlerName { get; }

    public int Score { get; }

    public int HighGame { get; }

    public bool Casher { get; }

    public ViewModel(Models.BowlerSquadScore bowlerScore, short place, int cashingPositions)
    {
        Place = place;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        HighGame = bowlerScore.HighGame;
        Casher = place <= cashingPositions;
    }
}

internal interface IViewModel
{
    short Place { get; }

    string BowlerName { get; }

    int Score { get; }

    int HighGame { get; }

    bool Casher { get; }
}
