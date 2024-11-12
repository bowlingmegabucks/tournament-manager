
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Scores.IRepository> _repository;
    private Mock<NortheastMegabuck.Squads.IHandicapCalculator> _handicapCalculator;

    private NortheastMegabuck.Scores.Retrieve.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Scores.IRepository>();
        _handicapCalculator = new Mock<NortheastMegabuck.Squads.IHandicapCalculator>();

        _dataLayer = new NortheastMegabuck.Scores.Retrieve.DataLayer(_repository.Object, _handicapCalculator.Object);
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId[]>())).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.SquadScore>().BuildMock());
        var squadIds = new[] { SquadId.New(), SquadId.New() };

        await _dataLayer.ExecuteAsync(squadIds, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(squadIds), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_ReturnsRepositoryRetrieve()
    {
        var entity1 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad
            {
                Date = DateTime.Now
            },
            Bowler = new NortheastMegabuck.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations =
                [
                    new NortheastMegabuck.Database.Entities.Registration {Division = new NortheastMegabuck.Database.Entities.Division() }
                ]
            }
        };

        var entity2 = new NortheastMegabuck.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Squad = new NortheastMegabuck.Database.Entities.TournamentSquad
            {
                Date = DateTime.Now
            },
            Bowler = new NortheastMegabuck.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations =
                [
                    new NortheastMegabuck.Database.Entities.Registration {Division = new NortheastMegabuck.Database.Entities.Division() }
                ]
            }
        };

        var entities = new[] { entity1, entity2 };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId[]>())).Returns(entities.BuildMock());

        var actual = (await _dataLayer.ExecuteAsync([SquadId.New(), SquadId.New()], default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(2));

            Assert.That(actual[0].Bowler.Id, Is.EqualTo(entity1.Bowler.Id));
            Assert.That(actual[1].SquadId, Is.EqualTo(entity2.SquadId));
        });
    }
}
