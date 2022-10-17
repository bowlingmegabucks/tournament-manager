
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
public class ViewModel
{
    [Test]
    public void Constructor_SquadScore_BowlerIdMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            BowlerId = BowlerId.New()
        };

        var viewModel = new NortheastMegabuck.Scores.Retrieve.ViewModel(model);

        Assert.That(viewModel.BowlerId, Is.EqualTo(model.BowlerId));
    }

    [Test]
    public void Constructor_SquadScore_GameNumberMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            GameNumber = 5
        };

        var viewModel = new NortheastMegabuck.Scores.Retrieve.ViewModel(model);

        Assert.That(viewModel.GameNumber, Is.EqualTo(model.GameNumber));
    }

    [Test]
    public void Constructor_SquadScore_ScoreMapped()
    { 
        var model = new NortheastMegabuck.Models.SquadScore
        { 
            Score = 200
        };

        var viewModel = new NortheastMegabuck.Scores.Retrieve.ViewModel(model);

        Assert.That(viewModel.Score, Is.EqualTo(model.Score));
    }
}