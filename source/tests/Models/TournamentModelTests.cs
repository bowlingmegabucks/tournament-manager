namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Tournament
{
    [Test]
    public void Constructor_TournamentEntity_IdMapped()
    {
        var id = Guid.NewGuid();
        var entity = new NewEnglandClassic.Database.Entities.Tournament { Id = id };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentEntity_NameMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { Name = "name" };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Name, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentEntity_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var entity = new NewEnglandClassic.Database.Entities.Tournament { Start = start };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Start, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentEntity_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var entity = new NewEnglandClassic.Database.Entities.Tournament { End = end };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.End, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentEntity_EntryFeeMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { EntryFee = 123.45m };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentEntity_GamesMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { Games = 5 };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentEntity_FinalsRatioMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { FinalsRatio = 12.3m };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentEntity_CashRatioMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { CashRatio = 45.6m };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentEntity_BowlingCenterMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { BowlingCenter = "bowlingCenter" };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentEntity_CompletedMapped([Values]bool completed)
    {
        var entity = new NewEnglandClassic.Database.Entities.Tournament { Completed = completed };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Completed, Is.EqualTo(completed));
    }

    [Test]
    public void Constructor_TournamentViewModel_IdMapped()
    {
        var id = Guid.NewGuid();
        var entity = new NewEnglandClassic.Tournaments.ViewModel { Id = id };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentViewModel_NameMapped()
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { TournamentName = "name" };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Name, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentViewModel_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var entity = new NewEnglandClassic.Tournaments.ViewModel { Start = start };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Start, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentViewModel_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var entity = new NewEnglandClassic.Tournaments.ViewModel { End = end };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.End, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentViewModel_EntryFeeMapped()
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { EntryFee = 123.45m };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentViewModel_GamesMapped()
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { Games = 5 };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentViewModel_FinalsRatioMapped()
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { FinalsRatio = 12.3m };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentViewModel_CashRatioMapped()
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { CashRatio = 45.6m };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentViewModel_BowlingCenterMapped()
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { BowlingCenter = "bowlingCenter" };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentViewModel_CompletedMapped([Values] bool completed)
    {
        var entity = new NewEnglandClassic.Tournaments.ViewModel { Completed = completed };

        var model = new NewEnglandClassic.Models.Tournament(entity);

        Assert.That(model.Completed, Is.EqualTo(completed));
    }
}
