
namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_TournamentModel_IdMapped()
    {
        var id = TournamentId.New();
        var model = new TournamentManager.Models.Tournament
        {
            Id = id
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentModel_NameMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            Name = "name"
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.TournamentName, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentModel_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var model = new TournamentManager.Models.Tournament
        {
            Start = start
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.StartDate, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentModel_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var model = new TournamentManager.Models.Tournament
        {
            End = end
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.EndDate, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentModel_EntryFeeMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            EntryFee = 123.45m
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentModel_GamesMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            Games = 5
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentModel_FinalsRatioMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            FinalsRatio = 12.3m
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentModel_CashRatioMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            CashRatio = 45.6m
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentModel_SuperSweeperCashRatioMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            SuperSweeperCashRatio = 45.6m
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.SuperSweeperCashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentModel_BowlingCenterMapped()
    {
        var model = new TournamentManager.Models.Tournament
        {
            BowlingCenter = "bowlingCenter"
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentModel_CompletedMapped([Values] bool completed)
    {
        var model = new TournamentManager.Models.Tournament
        {
            Completed = completed
        };

        var viewModel = new TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Completed, Is.EqualTo(completed));
    }
}
