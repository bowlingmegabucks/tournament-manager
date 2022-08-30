namespace NortheastMegabuck.Tests.Models;

[TestFixture]
internal class Tournament
{
    [Test]
    public void Constructor_TournamentEntity_IdMapped()
    {
        var id = TournamentId.New();
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Id = id };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentEntity_NameMapped()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Name = "name" };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Name, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentEntity_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Start = start };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Start, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentEntity_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var entity = new NortheastMegabuck.Database.Entities.Tournament { End = end };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.End, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentEntity_EntryFeeMapped()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { EntryFee = 123.45m };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentEntity_GamesMapped()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Games = 5 };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentEntity_FinalsRatioMapped()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { FinalsRatio = 12.3m };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentEntity_CashRatioMapped()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { CashRatio = 45.6m };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentEntity_BowlingCenterMapped()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { BowlingCenter = "bowlingCenter" };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentEntity_CompletedMapped([Values]bool completed)
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Completed = completed };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Completed, Is.EqualTo(completed));
    }

    [Test]
    public void Constructor_TournamentEntity_SquadsNull_EmptySquadsCollection()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Squads = null };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Squads, Is.Empty);
    }
    
    [Test]
    public void Constructor_TournamentEntity_SquadsNotNull_SquadsCollectionMapped()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New(), Tournament = new NortheastMegabuck.Database.Entities.Tournament()
        }, 3).ToList();
        
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Squads = squads };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Squads, Is.Not.Empty);
    }

    [Test]
    public void Constructor_TournamentEntity_SweepersNull_EmptySweepersCollection()
    {
        var entity = new NortheastMegabuck.Database.Entities.Tournament { Sweepers = null };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Sweepers, Is.Empty);
    }

    [Test]
    public void Constructor_TournamentEntity_SweepersNotNull_SweepersCollectionMapped()
    {
        var sweepers = Enumerable.Repeat(new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            Tournament = new NortheastMegabuck.Database.Entities.Tournament(),
            CashRatio = 5,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList() 
        }, 3).ToList();

        var entity = new NortheastMegabuck.Database.Entities.Tournament { Sweepers = sweepers };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Sweepers, Is.Not.Empty);
    }

    [Test]
    public void Constructor_TournamentViewModel_IdMapped()
    {
        var id = TournamentId.New();
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { Id = id };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_TournamentViewModel_NameMapped()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { TournamentName = "name" };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.Name, Is.EqualTo("name"));
    }

    [Test]
    public void Constructor_TournamentViewModel_StartMapped()
    {
        var start = new DateOnly(2000, 1, 2);
        var entity = new NortheastMegabuck.Tournaments.ViewModel { Start = start };

        var model = new NortheastMegabuck.Models.Tournament(entity);

        Assert.That(model.Start, Is.EqualTo(start));
    }

    [Test]
    public void Constructor_TournamentViewModel_EndMapped()
    {
        var end = new DateOnly(2000, 1, 2);
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { End = end };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.End, Is.EqualTo(end));
    }

    [Test]
    public void Constructor_TournamentViewModel_EntryFeeMapped()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { EntryFee = 123.45m };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_TournamentViewModel_GamesMapped()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { Games = 5 };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_TournamentViewModel_FinalsRatioMapped()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { FinalsRatio = 12.3m };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.FinalsRatio, Is.EqualTo(12.3m));
    }

    [Test]
    public void Constructor_TournamentViewModel_CashRatioMapped()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { CashRatio = 45.6m };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.CashRatio, Is.EqualTo(45.6m));
    }

    [Test]
    public void Constructor_TournamentViewModel_BowlingCenterMapped()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { BowlingCenter = "bowlingCenter" };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.BowlingCenter, Is.EqualTo("bowlingCenter"));
    }

    [Test]
    public void Constructor_TournamentViewModel_CompletedMapped([Values] bool completed)
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel { Completed = completed };

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.Completed, Is.EqualTo(completed));
    }

    [Test]
    public void Constructor_TournamentViewModel_EmptySquadsCollection()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel();

        var model = new NortheastMegabuck.Models.Tournament(viewModel);

        Assert.That(model.Squads, Is.Empty);
    }
}
