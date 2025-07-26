
namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Results;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_PlaceMapped()
    {
        var viewModel = new TournamentManager.Sweepers.Results.ViewModel(new TournamentManager.Models.BowlerSquadScore(200), 1, 5);

        Assert.That(viewModel.Place, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_BowlerNameMapped()
    {
        var viewModel = new TournamentManager.Sweepers.Results.ViewModel(new TournamentManager.Models.BowlerSquadScore(200)
        {
            Bowler = new TournamentManager.Models.Bowler { Name = new TournamentManager.Models.PersonName { First = "first", Last = "last" } }
        }, 1, 5);

        Assert.That(viewModel.BowlerName, Is.EqualTo("first last"));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_ScoreMapped()
    {
        var viewModel = new TournamentManager.Sweepers.Results.ViewModel(new TournamentManager.Models.BowlerSquadScore(200, 201), 1, 5);

        Assert.That(viewModel.Score, Is.EqualTo(401));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_HighGameMapped()
    {
        var viewModel = new TournamentManager.Sweepers.Results.ViewModel(new TournamentManager.Models.BowlerSquadScore(200, 201), 1, 5);

        Assert.That(viewModel.HighGame, Is.EqualTo(201));
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_PlaceLessThanOrEqualToCashingPosition_CasherMapped([Range(1, 5)] short place)
    {
        var viewModel = new TournamentManager.Sweepers.Results.ViewModel(new TournamentManager.Models.BowlerSquadScore(200), place, 5);

        Assert.That(viewModel.Casher, Is.True);
    }

    [Test]
    public void Constructor_ModelsBowlerSquadScorePlaceCashingPositions_PlaceGreaterThanCashingPosition_CasherMapped()
    {
        var viewModel = new TournamentManager.Sweepers.Results.ViewModel(new TournamentManager.Models.BowlerSquadScore(200), 6, 5);

        Assert.That(viewModel.Casher, Is.False);
    }
}
