using NortheastMegabuck.Sweepers;

namespace NortheastMegabuck.Tests.Sweepers.Add;

[TestFixture]
internal class EntityMapper
{
    private IEntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new NortheastMegabuck.Sweepers.EntityMapper();

    [Test]
    public void Execute_IdMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            Id = SquadId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Execute_TournamentIdMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            TournamentId = TournamentId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.TournamentId, Is.EqualTo(model.TournamentId));
    }

    [Test]
    public void Execute_CashRatioMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            CashRatio = 5.5m
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.CashRatio, Is.EqualTo(model.CashRatio));
    }

    [Test]
    public void Execute_DateMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            Date = new DateTime(2018, 1, 1)
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Date, Is.EqualTo(model.Date));
    }

    [Test]
    public void Execute_MaxPerPairMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            MaxPerPair = 1
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MaxPerPair, Is.EqualTo(model.MaxPerPair));
    }

    [Test]
    public void Execute_CompleteMapped([Values] bool complete)
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            Complete = complete
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Complete, Is.EqualTo(model.Complete));
    }

    [Test]
    public void Execute_EntryFeeMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            EntryFee = 123.45m
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.EntryFee, Is.EqualTo(model.EntryFee));
    }

    [Test]
    public void Execute_GamesMapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            Games = 5
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Games, Is.EqualTo(model.Games));
    }

    [Test]
    public void Execute_DivisionsMapped()
    {
        var division0 = DivisionId.New();
        var division1 = DivisionId.New();
        var division2 = DivisionId.New();
        var division3 = DivisionId.New();

        var model = new NortheastMegabuck.Models.Sweeper
        {
            Id = SquadId.New(),
            Divisions = new Dictionary<NortheastMegabuck.DivisionId, int?>
            {
                { division0, null},
                { division1, 1},
                { division2, 2},
                { division3, 3}
            }
        };

        var entity = _mapper.Execute(model);

        Assert.Multiple(() =>
        {
            Assert.That(entity.Divisions.Count, Is.EqualTo(4));
            Assert.That(entity.Divisions.All(division => division.SweeperId == model.Id));

            Assert.That(entity.Divisions.Count(division => division.DivisionId == division0 && division.BonusPinsPerGame == null), Is.EqualTo(1));
            Assert.That(entity.Divisions.Count(division => division.DivisionId == division1 && division.BonusPinsPerGame == 1), Is.EqualTo(1));
            Assert.That(entity.Divisions.Count(division => division.DivisionId == division2 && division.BonusPinsPerGame == 2), Is.EqualTo(1));
            Assert.That(entity.Divisions.Count(division => division.DivisionId == division3 && division.BonusPinsPerGame == 3), Is.EqualTo(1));
        });
    }

    [Test]
    public void Execute_StartingLane_Mapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            StartingLane = 1
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.StartingLane, Is.EqualTo(model.StartingLane));
    }

    [Test]
    public void Execute_NumberOfLanes_Mapped()
    {
        var model = new NortheastMegabuck.Models.Sweeper
        {
            NumberOfLanes = 10
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.NumberOfLanes, Is.EqualTo(model.NumberOfLanes));
    }
}
