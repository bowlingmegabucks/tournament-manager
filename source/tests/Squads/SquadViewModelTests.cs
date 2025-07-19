
namespace BowlingMegabucks.TournamentManager.Tests.Squads;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_Model_IdMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Id = SquadId.New()
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Constructor_Model_TournamentIdMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.TournamentId, Is.EqualTo(model.TournamentId));
    }

    [Test]
    public void Constructor_Model_CashRatioMapped([Values(null, 1.1)] decimal? cashRatio)
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            CashRatio = cashRatio
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.CashRatio, Is.EqualTo(model.CashRatio));
    }

    [Test]
    public void Constructor_Model_FinalsRatioMapped([Values(null, 1.1)] decimal? finalsRatio)
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            FinalsRatio = finalsRatio
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.FinalsRatio, Is.EqualTo(model.FinalsRatio));
    }

    [Test]
    public void Constructor_Model_DateMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Date = DateTime.Now
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.Date, Is.EqualTo(model.Date));
    }

    [Test]
    public void Constructor_Model_MaxPerPairMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            MaxPerPair = 1
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.MaxPerPair, Is.EqualTo(model.MaxPerPair));
    }

    [Test]
    public void Constructor_Model_CompleteMapped([Values] bool complete)
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Squad
        {
            Complete = complete
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Squads.ViewModel(model);

        Assert.That(viewModel.Complete, Is.EqualTo(model.Complete));
    }
}
