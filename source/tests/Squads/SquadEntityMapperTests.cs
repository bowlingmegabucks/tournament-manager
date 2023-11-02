namespace NortheastMegabuck.Tests.Squads;

[TestFixture]
internal sealed class EntityMapper
{
    private NortheastMegabuck.Squads.EntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new NortheastMegabuck.Squads.EntityMapper();

    [Test]
    public void Execute_IdMapped()
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Execute_TournamentIdMapped()
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            TournamentId = TournamentId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.TournamentId, Is.EqualTo(model.TournamentId));
    }

    [Test]
    public void Execute_EntryFeeMapped([Values(null, 100)] decimal? entryFee)
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            EntryFee = entryFee
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.EntryFee, Is.EqualTo(model.EntryFee));
    }

    [Test]
    public void Execute_CashRatioMapped([Values(null, 4.5)] decimal? cashRatio)
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            CashRatio = cashRatio
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.CashRatio, Is.EqualTo(model.CashRatio));
    }

    [Test]
    public void Execute_FinalsRatioMapped([Values(null, 5.5)] decimal? finalsRatio)
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            FinalsRatio = finalsRatio
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.FinalsRatio, Is.EqualTo(model.FinalsRatio));
    }

    [Test]
    public void Execute_DateMapped()
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            Date = DateTime.Now
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Date, Is.EqualTo(model.Date));
    }

    [Test]
    public void Execute_MaxPerPairMapped()
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            MaxPerPair = 5
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MaxPerPair, Is.EqualTo(model.MaxPerPair));
    }

    [Test]
    public void Execute_CompleteMapped([Values] bool complete)
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            Complete = complete
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Complete, Is.EqualTo(model.Complete));
    }

    [Test]
    public void Execute_StartingLane_Mapped()
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            StartingLane = 1
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.StartingLane, Is.EqualTo(model.StartingLane));
    }

    [Test]
    public void Execute_NumberOfLanes_Mapped()
    {
        var model = new NortheastMegabuck.Models.Squad
        {
            NumberOfLanes = 10
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.NumberOfLanes, Is.EqualTo(model.NumberOfLanes));
    }
}
