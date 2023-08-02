namespace NortheastMegabuck.Tests.Scores;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_Model_BowlerIdMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var viewModel = new NortheastMegabuck.Scores.ViewModel(model);

        Assert.That(viewModel.BowlerId, Is.EqualTo(model.Bowler.Id));
    }

    [Test]
    public void Constructor_Model_GameNumberMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            GameNumber = 5
        };

        var viewModel = new NortheastMegabuck.Scores.ViewModel(model);

        Assert.That(viewModel.GameNumber, Is.EqualTo(model.GameNumber));
    }

    [Test]
    public void Constructor_Model_ScoreMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            Score = 200
        };

        var viewModel = new NortheastMegabuck.Scores.ViewModel(model);

        Assert.That(viewModel.Score, Is.EqualTo(model.Score));
    }

    [Test]
    public void Constructor_Model_SquadIdMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = SquadId.New()
        };

        var viewModel = new NortheastMegabuck.Scores.ViewModel(model);

        Assert.That(viewModel.SquadId, Is.EqualTo(model.SquadId));
    }
}
