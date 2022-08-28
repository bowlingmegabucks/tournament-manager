
namespace NewEnglandClassic.Tests.Registrations;

[TestFixture]
internal class EntityMapper
{
    private Mock<NewEnglandClassic.Bowlers.IEntityMapper> _bowlerEntityMapper;

    private NewEnglandClassic.Registrations.IEntityMapper _mapper;

    [SetUp]
    public void SetUp()
    { 
        _bowlerEntityMapper = new Mock<NewEnglandClassic.Bowlers.IEntityMapper>();

        _mapper = new NewEnglandClassic.Registrations.EntityMapper(_bowlerEntityMapper.Object);
    }

    [Test]
    public void Execute_RegistrationIdMapped()
    {
        var model = new NewEnglandClassic.Models.Registration
        {
            Id = RegistrationId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Execute_BowlerIdMapped()
    {
        var model = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = BowlerId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.BowlerId, Is.EqualTo(model.Bowler.Id));
    }

    [Test]
    public void Execute_BowlerEntityMapperExecute_CalledCorrectly()
    {
        var model = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = BowlerId.New() }
        };

        _mapper.Execute(model);

        _bowlerEntityMapper.Verify(mapper => mapper.Execute(model.Bowler), Times.Once);
    }

    [Test]
    public void Execute_BowlerMapped()
    {
        var bowler = new NewEnglandClassic.Database.Entities.Bowler { Id = BowlerId.New() };
        _bowlerEntityMapper.Setup(mapper => mapper.Execute(It.IsAny<NewEnglandClassic.Models.Bowler>())).Returns(bowler);

        var model = new NewEnglandClassic.Models.Registration
        {
            Bowler = new NewEnglandClassic.Models.Bowler { Id = BowlerId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Bowler, Is.EqualTo(bowler));
    }

    [Test]
    public void Execute_DivisionIdMapped()
    {
        var model = new NewEnglandClassic.Models.Registration
        {
            Division = new NewEnglandClassic.Models.Division { Id = NewEnglandClassic.Divisions.Id.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.DivisionId, Is.EqualTo(model.Division.Id));
    }

    [Test]
    public void Execute_AverageMapped([Values(null, 200)] int? average)
    {
        var model = new NewEnglandClassic.Models.Registration
        {
            Average = average
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Average, Is.EqualTo(model.Average));
    }

    [Test]
    public void Execute_SquadsMapped()
    {
        var squad1 = new NewEnglandClassic.Models.Squad { Id = SquadId.New() };
        var squad2 = new NewEnglandClassic.Models.Squad { Id = SquadId.New() };
        var squad3 = new NewEnglandClassic.Models.Squad { Id = SquadId.New() };

        var sweeper1 = new NewEnglandClassic.Models.Sweeper { Id = SquadId.New() };
        var sweeper2 = new NewEnglandClassic.Models.Sweeper { Id = SquadId.New() };
        var sweeper3 = new NewEnglandClassic.Models.Sweeper { Id = SquadId.New() };

        var id = RegistrationId.New();

        var model = new NewEnglandClassic.Models.Registration
        {
            Squads = new[] { squad1, squad2, squad3 },
            Sweepers = new[] { sweeper1, sweeper2, sweeper3 },
            Id = id
        };

        var entity = _mapper.Execute(model);

        Assert.Multiple(() =>
        {
            Assert.That(entity.Squads.All(squad => squad.RegistrationId == id));
            Assert.That(entity.Squads, Has.Count.EqualTo(6));

            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(squad1.Id));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(squad2.Id));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(squad3.Id));

            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(sweeper1.Id));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(sweeper2.Id));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(sweeper3.Id));
        });
    }

    [Test]
    public void Execute_SuperSweeperMapped([Values] bool superSweeper)
    {
        var model = new NewEnglandClassic.Models.Registration
        {
            SuperSweeper = superSweeper
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.SuperSweeper, Is.EqualTo(superSweeper));
    }
}
