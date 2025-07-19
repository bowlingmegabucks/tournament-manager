
using System.Globalization;

namespace BowlingMegabucks.TournamentManager.Tournaments.Results;

internal class AtLargeViewModel(short place, Models.BowlerSquadScore result, bool previousCasher) : IAtLargeViewModel
{
    public short Place { get; } = place;

    public string BowlerName { get; } = result.Bowler.ToString();

    public string DivisionName { get; } = result.Division.Name;

    public string SquadDate { get; } = result.SquadDate.ToString("MM/dd hh:mm tt", CultureInfo.InvariantCulture);

    public bool PreviousCasher { get; } = previousCasher;

    public int Score { get; } = result.Score;

    public int ScratchScore { get; } = result.ScratchScore;

    public int HighGame { get; } = result.HighGame;

    public int HighGameScratch { get; } = result.HighGameScratch;
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