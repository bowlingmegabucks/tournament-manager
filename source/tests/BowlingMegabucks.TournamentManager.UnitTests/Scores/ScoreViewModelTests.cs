namespace BowlingMegabucks.TournamentManager.UnitTests.Scores;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_Model_BowlerIdMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() }
        };

        var viewModel = new TournamentManager.Scores.ViewModel(model);

        Assert.That(viewModel.BowlerId, Is.EqualTo(model.Bowler.Id));
    }

    [Test]
    public void Constructor_Model_GameNumberMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            GameNumber = 5
        };

        var viewModel = new TournamentManager.Scores.ViewModel(model);

        Assert.That(viewModel.GameNumber, Is.EqualTo(model.GameNumber));
    }

    [Test]
    public void Constructor_Model_ScoreMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            Score = 200
        };

        var viewModel = new TournamentManager.Scores.ViewModel(model);

        Assert.That(viewModel.Score, Is.EqualTo(model.Score));
    }

    [Test]
    public void Constructor_Model_SquadIdMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            SquadId = SquadId.New()
        };

        var viewModel = new TournamentManager.Scores.ViewModel(model);

        Assert.That(viewModel.SquadId, Is.EqualTo(model.SquadId));
    }
}
