
namespace NortheastMegabuck.Tournaments.Seeding;
internal class ViewModel : IViewModel
{
    public string DivisionName { get; }

    public int Seed { get; }

    public string BowlerName { get; }

    public int Score { get; }

    public int HighGame { get; }

    public ViewModel(int seed, Models.BowlerSquadScore bowlerScore)
    {
        DivisionName = bowlerScore.Division.Name;
        Seed = seed;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        HighGame = bowlerScore.HighGame;
    }
}

internal interface IViewModel
{
    string DivisionName { get; }

    int Seed { get; }

    string BowlerName { get; }

    int Score { get; }

    int HighGame { get; }
}