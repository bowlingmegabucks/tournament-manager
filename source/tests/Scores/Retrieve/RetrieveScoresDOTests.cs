
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Scores.IRepository> _repository;
    private Mock<NortheastMegabuck.Squads.IHandicapCalculator> _handicapCalculator;

    private NortheastMegabuck.Scores.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Scores.IRepository>();
        _handicapCalculator = new Mock<NortheastMegabuck.Squads.IHandicapCalculator>();

        _dataLayer = new NortheastMegabuck.Scores.Retrieve.DataLayer(_repository.Object, _handicapCalculator.Object);
    }

    [Test]
    public void Execute_SquadId_RepositoryRetrieve_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _dataLayer.Execute(squadId);

        _repository.Verify(repository => repository.Retrieve(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ReturnsRepositoryRetrieve()
    {
        var entity1 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler 
            { 
                Id = BowlerId.New() ,
                Registrations = new[]
                { 
                    new NortheastMegabuck.Database.Entities.Registration {Division = new NortheastMegabuck.Database.Entities.Division() }
                }
            }
        };

        var entity2 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations = new[]
                {
                    new NortheastMegabuck.Database.Entities.Registration {Division = new NortheastMegabuck.Database.Entities.Division() }
                }
            }
        };

        var entities = new[] { entity1, entity2 };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(entities);

        var actual = _dataLayer.Execute(SquadId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual[0].Bowler.Id, Is.EqualTo(entity1.Bowler.Id));
            Assert.That(actual[1].SquadId, Is.EqualTo(entity2.SquadId));
        });
    }

    [Test]
    public void Execute_SquadIds_RepositoryRetrieve_CalledCorrectly()
    {
        var squadIds = new[] { SquadId.New(), SquadId.New() };

        _dataLayer.Execute(squadIds);

        _repository.Verify(repository => repository.Retrieve(squadIds), Times.Once);
    }

    [Test]
    public void Execute_SquadIds_ReturnsRepositoryRetrieve()
    {
        var entity1 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations = new[]
                {
                    new NortheastMegabuck.Database.Entities.Registration {Division = new NortheastMegabuck.Database.Entities.Division() }
                }
            }
        };

        var entity2 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Bowler = new NortheastMegabuck.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations = new[]
                {
                    new NortheastMegabuck.Database.Entities.Registration {Division = new NortheastMegabuck.Database.Entities.Division() }
                }
            }
        };

        var entities = new[] { entity1, entity2 };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<IEnumerable<SquadId>>())).Returns(entities);

        var actual = _dataLayer.Execute(new[] { SquadId.New(), SquadId.New() }).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual[0].Bowler.Id, Is.EqualTo(entity1.Bowler.Id));
            Assert.That(actual[1].SquadId, Is.EqualTo(entity2.SquadId));
        });
    }
}
