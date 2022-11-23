
namespace NortheastMegabuck.Tournaments.Seeding;
internal class ViewModel : IViewModel
{
    public string DivisionName { get; }

    public int Seed { get; }

    public string BowlerName { get; }

    public int Score { get; }

    public int HighGame { get; }

    public bool Qualified { get; }

    public bool AtLargeCasher { get; }

    public ViewModel(int seed, bool qualified, bool atLargeCasher, Models.BowlerSquadScore bowlerScore)
    {
        DivisionName = bowlerScore.Division.Name;
        Seed = seed;
        BowlerName = bowlerScore.Bowler.ToString();
        Score = bowlerScore.Score;
        HighGame = bowlerScore.HighGame;
        Qualified = qualified;
        AtLargeCasher = atLargeCasher;
    }
}

internal interface IViewModel
{
    string DivisionName { get; }

    int Seed { get; }

    string BowlerName { get; }

    int Score { get; }

    int HighGame { get; }

    bool Qualified { get; }

    bool AtLargeCasher { get; }
}