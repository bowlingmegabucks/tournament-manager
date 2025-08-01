
namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations;

[TestFixture]
internal sealed class EntityMapper
{
    private Mock<TournamentManager.Bowlers.IEntityMapper> _bowlerEntityMapper;

    private TournamentManager.Registrations.EntityMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        _bowlerEntityMapper = new Mock<TournamentManager.Bowlers.IEntityMapper>();

        _mapper = new TournamentManager.Registrations.EntityMapper(_bowlerEntityMapper.Object);
    }

    [Test]
    public void Execute_RegistrationIdMapped()
    {
        var model = new TournamentManager.Models.Registration
        {
            Id = RegistrationId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Execute_BowlerIdMapped()
    {
        var model = new TournamentManager.Models.Registration
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.BowlerId, Is.EqualTo(model.Bowler.Id));
    }

    [Test]
    public void Execute_BowlerEntityMapperExecute_CalledCorrectly()
    {
        var model = new TournamentManager.Models.Registration
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() }
        };

        _mapper.Execute(model);

        _bowlerEntityMapper.Verify(mapper => mapper.Execute(model.Bowler), Times.Once);
    }

    [Test]
    public void Execute_BowlerMapped()
    {
        var bowler = new TournamentManager.Database.Entities.Bowler { Id = BowlerId.New() };
        _bowlerEntityMapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Bowler>())).Returns(bowler);

        var model = new TournamentManager.Models.Registration
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Bowler, Is.EqualTo(bowler));
    }

    [Test]
    public void Execute_DivisionIdMapped()
    {
        var model = new TournamentManager.Models.Registration
        {
            Division = new TournamentManager.Models.Division { Id = DivisionId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.DivisionId, Is.EqualTo(model.Division.Id));
    }

    [Test]
    public void Execute_AverageMapped([Values(null, 200)] int? average)
    {
        var model = new TournamentManager.Models.Registration
        {
            Average = average
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Average, Is.EqualTo(model.Average));
    }

    [Test]
    public void Execute_SquadsMapped()
    {
        var squad1 = new TournamentManager.Models.Squad();
        var squad2 = new TournamentManager.Models.Squad();
        var squad3 = new TournamentManager.Models.Squad();

        var sweeper1 = new TournamentManager.Models.Sweeper { Id = SquadId.New() };
        var sweeper2 = new TournamentManager.Models.Sweeper { Id = SquadId.New() };
        var sweeper3 = new TournamentManager.Models.Sweeper { Id = SquadId.New() };

        var id = RegistrationId.New();

        var model = new TournamentManager.Models.Registration
        {
            Squads = [squad1, squad2, squad3],
            Sweepers = [sweeper1, sweeper2, sweeper3],
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
        var model = new TournamentManager.Models.Registration
        {
            SuperSweeper = superSweeper
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.SuperSweeper, Is.EqualTo(superSweeper));
    }
}
