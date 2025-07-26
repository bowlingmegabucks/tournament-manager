
namespace BowlingMegabucks.TournamentManager.Tests.Tournaments;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_TournamentModel_IdMapped()
    {
        var id = TournamentId.New();
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            Id = id
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentModel_NameMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            Name = "name"
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.TournamentName, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentModel_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            Start = start
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Start, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentModel_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            End = end
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.End, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentModel_EntryFeeMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            EntryFee = 123.45m
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentModel_GamesMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            Games = 5
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentModel_FinalsRatioMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            FinalsRatio = 12.3m
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentModel_CashRatioMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            CashRatio = 45.6m
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentModel_SuperSweeperCashRatioMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            SuperSweeperCashRatio = 45.6m
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.SuperSweeperCashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentModel_BowlingCenterMapped()
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            BowlingCenter = "bowlingCenter"
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentModel_CompletedMapped([Values] bool completed)
    {
        var model = new BowlingMegabucks.TournamentManager.Models.Tournament
        {
            Completed = completed
        };

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel(model);

        Assert.That(viewModel.Completed, Is.EqualTo(completed));
    }
}
