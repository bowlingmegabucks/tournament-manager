
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
            Id = Guid.NewGuid()
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
            Division = new NewEnglandClassic.Models.Division { Id = Guid.NewGuid() }
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
        var squad1 = Guid.NewGuid();
        var squad2 = Guid.NewGuid();
        var squad3 = Guid.NewGuid();

        var sweeper1 = Guid.NewGuid();
        var sweeper2 = Guid.NewGuid();
        var sweeper3 = Guid.NewGuid();

        var id = Guid.NewGuid();

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

            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(squad1));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(squad2));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(squad3));

            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(sweeper1));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(sweeper2));
            Assert.That(entity.Squads.Select(squad => squad.SquadId), Has.Member(sweeper3));
        });
    }
}
