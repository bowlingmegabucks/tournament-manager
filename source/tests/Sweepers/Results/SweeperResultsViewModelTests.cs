
namespace BowlingMegabucks.TournamentManager.Tests.Sweepers.Results;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_PlaceMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Sweepers.Results.ViewModel(new BowlingMegabucks.TournamentManager.Models.BowlerSquadScore(200), 1, 5);

        Assert.That(viewModel.Place, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_BowlerNameMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Sweepers.Results.ViewModel(new BowlingMegabucks.TournamentManager.Models.BowlerSquadScore(200)
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler { Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "first", Last = "last" } }
        }, 1, 5);

        Assert.That(viewModel.BowlerName, Is.EqualTo("first last"));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_ScoreMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Sweepers.Results.ViewModel(new BowlingMegabucks.TournamentManager.Models.BowlerSquadScore(200, 201), 1, 5);

        Assert.That(viewModel.Score, Is.EqualTo(401));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_HighGameMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Sweepers.Results.ViewModel(new BowlingMegabucks.TournamentManager.Models.BowlerSquadScore(200, 201), 1, 5);

        Assert.That(viewModel.HighGame, Is.EqualTo(201));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_PlaceLessThanOrEqualToCashingPosition_CasherMapped([Range(1, 5)] short place)
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Sweepers.Results.ViewModel(new BowlingMegabucks.TournamentManager.Models.BowlerSquadScore(200), place, 5);

        Assert.That(viewModel.Casher, Is.True);
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_PlaceGreaterThanCashingPosition_CasherMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Sweepers.Results.ViewModel(new BowlingMegabucks.TournamentManager.Models.BowlerSquadScore(200), 6, 5);

        Assert.That(viewModel.Casher, Is.False);
    }
}
