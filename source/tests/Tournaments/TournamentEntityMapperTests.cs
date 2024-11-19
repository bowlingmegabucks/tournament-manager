namespace NortheastMegabuck.Tests.Tournaments;

[TestFixture]
internal sealed class EntityMapperTests
{
    private NortheastMegabuck.Tournaments.IEntityMapper _mapper;

    [OneTimeSetUp]
    public void OneTimeSetUp()
        => _mapper = new NortheastMegabuck.Tournaments.EntityMapper();

    [Test]
    public void Execute_IdMapped()
    {
        var id = TournamentId.New();

        var model = new NortheastMegabuck.Models.Tournament { Id = id };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Id, Is.EqualTo(id));
    }

    [Test]
    public void Execute_NameMapped()
    {
        var name = "Test Tournament";

        var model = new NortheastMegabuck.Models.Tournament { Name = name };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Name, Is.EqualTo(name));
    }

    [Test]
    public void Execute_StartMapped()
    {
        var startDate = new DateOnly(2000, 1, 1);

        var model = new NortheastMegabuck.Models.Tournament { Start = startDate };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Start, Is.EqualTo(startDate));
    }

    [Test]
    public void Execute_EndMapped()
    {
        var endDate = new DateOnly(2000, 1, 1);

        var model = new NortheastMegabuck.Models.Tournament { End = endDate };

        var entity = _mapper.Execute(model);

        Assert.That(entity.End, Is.EqualTo(endDate));
    }

    [Test]
    public void Execute_EntryFeeMapped()
    {
        var entryFee = 100.50m;

        var model = new NortheastMegabuck.Models.Tournament { EntryFee = entryFee };

        var entity = _mapper.Execute(model);

        Assert.That(entity.EntryFee, Is.EqualTo(entryFee));
    }

    [Test]
    public void Execute_GamesMapped()
    {
        short games = 5;

        var model = new NortheastMegabuck.Models.Tournament { Games = games };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Games, Is.EqualTo(games));
    }

    [Test]
    public void Execute_FinalsRatioMapped()
    {
        var finalsRatio = 0.5m;

        var model = new NortheastMegabuck.Models.Tournament { FinalsRatio = finalsRatio };

        var entity = _mapper.Execute(model);

        Assert.That(entity.FinalsRatio, Is.EqualTo(finalsRatio));
    }

    [Test]
    public void Execute_CashRatioMapped()
    {
        var cashRatio = 0.5m;

        var model = new NortheastMegabuck.Models.Tournament { CashRatio = cashRatio };

        var entity = _mapper.Execute(model);

        Assert.That(entity.CashRatio, Is.EqualTo(cashRatio));
    }

    [Test]
    public void Execute_BowlingCenterMapped()
    {
        var bowlingCenter = "Test Bowling Center";

        var model = new NortheastMegabuck.Models.Tournament { BowlingCenter = bowlingCenter };

        var entity = _mapper.Execute(model);

        Assert.That(entity.BowlingCenter, Is.EqualTo(bowlingCenter));
    }

    [Test]
    public void Execute_CompletedMapped([Values] bool completed)
    {
        var model = new NortheastMegabuck.Models.Tournament { Completed = completed };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Completed, Is.EqualTo(completed));
    }

    [Test]
    public void Execute_SuperSweeperCashRatioMapped()
    {
        var model = new NortheastMegabuck.Models.Tournament { SuperSweeperCashRatio = 1.2m };

        var entity = _mapper.Execute(model);

        Assert.That(entity.SuperSweperCashRatio, Is.EqualTo(1.2m));
    }
}
