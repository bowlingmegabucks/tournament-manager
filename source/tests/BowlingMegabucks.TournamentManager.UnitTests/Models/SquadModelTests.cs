using BowlingMegabucks.TournamentManager.Squads;

namespace BowlingMegabucks.TournamentManager.Tests.Models;

[TestFixture]
internal sealed class Squad
{
    [Test]
    public void Constructor_SquadEntity_IdMapped()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public void Constructor_SquadEntity_TournamentIdMapped()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            TournamentId = TournamentId.New(),
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.TournamentId, Is.EqualTo(entity.TournamentId));
    }

    [Test]
    public void Constructor_SquadEntity_EntryFeeMapped([Values(null, 100)] decimal? entryFee)
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            EntryFee = entryFee,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.EntryFee, Is.EqualTo(entity.EntryFee));
    }

    [Test]
    public void Constructor_SquadEntity_CashRatioMapped([Values(null, 5.5)] decimal? cashRatio)
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            CashRatio = cashRatio,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.CashRatio, Is.EqualTo(entity.CashRatio));
    }

    [Test]
    public void Constructor_SquadEntity_FinalsRatioMapped([Values(null, 4.5)] decimal? finalsRatio)
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            FinalsRatio = finalsRatio,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.FinalsRatio, Is.EqualTo(entity.FinalsRatio));
    }

    [Test]
    public void Constructor_SquadEntity_DateMapped()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            Date = DateTime.Now,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.Date, Is.EqualTo(entity.Date));
    }

    [Test]
    public void Constructor_SquadEntity_MaxPerPairMapped()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            MaxPerPair = 5,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.MaxPerPair, Is.EqualTo(entity.MaxPerPair));
    }

    [Test]
    public void Constructor_SquadEntity_StartingLaneMapped()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            StartingLane = 1,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.StartingLane, Is.EqualTo(entity.StartingLane));
    }

    [Test]
    public void Constructor_SquadEntity_NumberOfLanesMapped()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            NumberOfLanes = 32,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.NumberOfLanes, Is.EqualTo(entity.NumberOfLanes));
    }

    [Test]
    public void Constructor_SquadEntity_CompleteMapped([Values] bool complete)
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            Complete = complete,
            Tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament()
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Squad(entity);

        Assert.That(model.Complete, Is.EqualTo(entity.Complete));
    }

    [Test]
    public void Constructor_SquadViewModel_IdMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(SquadId.New());

        var model = viewModel.Object.ToModel();

        Assert.That(model.Id, Is.EqualTo(viewModel.Object.Id));
    }

    [Test]
    public void Constructor_SquadViewModel_TournamentIdMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.TournamentId).Returns(TournamentId.New());

        var model = viewModel.Object.ToModel();

        Assert.That(model.TournamentId, Is.EqualTo(viewModel.Object.TournamentId));
    }

    [Test]
    public void Constructor_SquadViewModel_EntryFeeMapped([Values(null, 100)] decimal? entryFee)
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.EntryFee).Returns(entryFee);

        var model = viewModel.Object.ToModel();

        Assert.That(model.EntryFee, Is.EqualTo(entryFee));
    }

    [Test]
    public void Constructor_SquadViewModel_CashRatioMapped([Values(null, 1.2)] decimal? cashRatio)
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.CashRatio).Returns(cashRatio);

        var model = viewModel.Object.ToModel();

        Assert.That(model.CashRatio, Is.EqualTo(cashRatio));
    }

    [Test]
    public void Constructor_SquadViewModel_FinalsRatioMapped([Values(null, 2.2)] decimal? finalsRatio)
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.FinalsRatio).Returns(finalsRatio);

        var model = viewModel.Object.ToModel();

        Assert.That(model.FinalsRatio, Is.EqualTo(finalsRatio));
    }

    [Test]
    public void Constructor_SquadViewModel_DateMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.Date).Returns(DateTime.Now);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Date, Is.EqualTo(viewModel.Object.Date));
    }

    [Test]
    public void Constructor_SquadViewModel_MaxPerPairMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.MaxPerPair).Returns(5);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MaxPerPair, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_SquadViewModel_StartingLaneMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.StartingLane).Returns(1);

        var model = viewModel.Object.ToModel();

        Assert.That(model.StartingLane, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_SquadViewModel_NumberOfLanesMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.NumberOfLanes).Returns(32);

        var model = viewModel.Object.ToModel();

        Assert.That(model.NumberOfLanes, Is.EqualTo(32));
    }

    [Test]
    public void Constructor_SquadViewModel_CompleteMapped([Values] bool complete)
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        viewModel.SetupGet(v => v.Complete).Returns(complete);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Complete, Is.EqualTo(complete));
    }
}
