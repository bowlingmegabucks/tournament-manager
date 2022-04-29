
namespace NewEnglandClassic.Tests.Tournaments;

[TestFixture]
internal class ViewModel
{
    [Test]
    public void Constructor_TournamentModel_IdMapped()
    {
        var id = Guid.NewGuid();
        var model = new NewEnglandClassic.Models.Tournament
        {
            Id = id
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentModel_NameMapped()
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            Name = "name"
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.TournamentName, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentModel_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var model = new NewEnglandClassic.Models.Tournament
        {
            Start = start
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.Start, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentModel_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var model = new NewEnglandClassic.Models.Tournament
        {
            End = end
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.End, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentModel_EntryFeeMapped()
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            EntryFee = 123.45m
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentModel_GamesMapped()
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            Games = 5
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentModel_FinalsRatioMapped()
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            FinalsRatio = 12.3m
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentModel_CashRatioMapped()
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            CashRatio = 45.6m
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentModel_BowlingCenterMapped()
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            BowlingCenter = "bowlingCenter"
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentModel_CompletedMapped([Values] bool completed)
    {
        var model = new NewEnglandClassic.Models.Tournament
        {
            Completed = completed
        };

        var viewModel = new NewEnglandClassic.Tournaments.ViewModel(model);

        Assert.That(viewModel.Completed, Is.EqualTo(completed));
    }
}
