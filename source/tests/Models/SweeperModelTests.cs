
using BowlingMegabucks.TournamentManager.Sweepers;

namespace BowlingMegabucks.TournamentManager.Tests.Models;

[TestFixture]
internal sealed class Sweeper
{
    [Test]
    public void Constructor_IViewModel_IdMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(SquadId.New());

        var model = viewModel.Object.ToModel();

        Assert.That(model.Id, Is.EqualTo(viewModel.Object.Id));
    }

    [Test]
    public void Constructor_IViewModel_TournamentIdMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.TournamentId).Returns(TournamentId.New());

        var model = viewModel.Object.ToModel();

        Assert.That(model.TournamentId, Is.EqualTo(viewModel.Object.TournamentId));
    }

    [Test]
    public void Constructor_IViewModel_EntryFeeMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.EntryFee).Returns(123.45m);

        var model = viewModel.Object.ToModel();

        Assert.That(model.EntryFee, Is.EqualTo(123.45m));
    }

    [Test]
    public void Constructor_IViewModel_GamesMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Games).Returns(5);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Games, Is.EqualTo(5));
    }

    [Test]
    public void Constructor_IViewModel_CashRatioMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.CashRatio).Returns(1.3m);

        var model = viewModel.Object.ToModel();

        Assert.That(model.CashRatio, Is.EqualTo(1.3m));
    }

    [Test]
    public void Constructor_IViewModel_DateMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Date).Returns(DateTime.Now);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Date, Is.EqualTo(viewModel.Object.Date));
    }

    [Test]
    public void Constructor_IViewModel_MaxPerPairMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.MaxPerPair).Returns(3);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MaxPerPair, Is.EqualTo(3));
    }

    [Test]
    public void Constructor_SquadViewModel_StartingLaneMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.StartingLane).Returns(1);

        var model = viewModel.Object.ToModel();

        Assert.That(model.StartingLane, Is.EqualTo(1));
    }

    [Test]
    public void Constructor_SquadViewModel_NumberOfLanesMapped()
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.NumberOfLanes).Returns(32);

        var model = viewModel.Object.ToModel();

        Assert.That(model.NumberOfLanes, Is.EqualTo(32));
    }

    [Test]
    public void Constructor_IViewModel_CompleteMapped([Values] bool complete)
    {
        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Complete).Returns(complete);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Complete, Is.EqualTo(complete));
    }

    [Test]
    public void Constructor_IViewModel_DivisionsMapped()
    {
        var divisions = new Mock<IDictionary<DivisionId, int?>>();

        var viewModel = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        viewModel.SetupGet(v => v.Divisions).Returns(divisions.Object);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Divisions, Is.EqualTo(divisions.Object));
    }

    [Test]
    public void Constructor_Entity_IdMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public void Constructor_Entity_TournamentIdMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.TournamentId, Is.EqualTo(entity.TournamentId));
    }

    [Test]
    public void Constructor_Entity_EntryFeeMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.EntryFee, Is.EqualTo(entity.EntryFee));
    }

    [Test]
    public void Constructor_Entity_GamesMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.Games, Is.EqualTo(entity.Games));
    }

    [Test]
    public void Constructor_Entity_CashRatioMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.CashRatio, Is.EqualTo(entity.CashRatio));
    }

    [Test]
    public void Constructor_Entity_DateMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.Date, Is.EqualTo(entity.Date));
    }

    [Test]
    public void Constructor_Entity_MaxPerPairMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.MaxPerPair, Is.EqualTo(entity.MaxPerPair));
    }

    [Test]
    public void Constructor_Entity_StartingLaneMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            StartingLane = 1,
            NumberOfLanes = 32,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.StartingLane, Is.EqualTo(entity.StartingLane));
    }

    [Test]
    public void Constructor_Entity_NumberOfLanesMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            StartingLane = 1,
            NumberOfLanes = 32,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.NumberOfLanes, Is.EqualTo(entity.NumberOfLanes));
    }

    [Test]
    public void Constructor_Entity_CompleteMapped([Values] bool complete)
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = complete,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.That(model.Complete, Is.EqualTo(entity.Complete));
    }

    [Test]
    public void Constructor_Entity_DivisionsMapped()
    {
        var sweeperDivision1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 1
        };

        var sweeperDivision2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision
        {
            SweeperId = SquadId.New(),
            DivisionId = BowlingMegabucks.TournamentManager.DivisionId.New(),
            BonusPinsPerGame = 2
        };

        var sweeperDivisions = new List<BowlingMegabucks.TournamentManager.Database.Entities.SweeperDivision>
        {
            sweeperDivision1,
            sweeperDivision2
        };

        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 123.45m,
            Games = 5,
            CashRatio = 1.3m,
            Date = DateTime.Now,
            MaxPerPair = 3,
            Complete = true,
            Divisions = sweeperDivisions
        };

        var model = new BowlingMegabucks.TournamentManager.Models.Sweeper(entity);

        Assert.Multiple(() =>
        {
            Assert.That(model.Divisions.ContainsKey(sweeperDivision1.DivisionId));
            Assert.That(model.Divisions.ContainsKey(sweeperDivision2.DivisionId));

            Assert.That(model.Divisions[sweeperDivision1.DivisionId].Value, Is.EqualTo(sweeperDivision1.BonusPinsPerGame));
            Assert.That(model.Divisions[sweeperDivision2.DivisionId].Value, Is.EqualTo(sweeperDivision2.BonusPinsPerGame));
        });
    }
}
