
namespace NortheastMegabuck.Tournaments.Seeding;
internal class ViewModel(int seed, bool qualified, bool atLargeCasher, Models.BowlerSquadScore bowlerScore) : IViewModel
{
    public string DivisionName { get; } = bowlerScore.Division.Name;

    public int Seed { get; } = seed;

    public string BowlerName { get; } = bowlerScore.Bowler.ToString();

    public int Score { get; } = bowlerScore.Score;

    public int HighGame { get; } = bowlerScore.HighGame;

    public bool Qualified { get; } = qualified;

    public bool AtLargeCasher { get; } = atLargeCasher;
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