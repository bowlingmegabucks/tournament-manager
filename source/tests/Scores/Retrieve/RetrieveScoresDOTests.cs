
using MockQueryable;

namespace BowlingMegabucks.TournamentManager.Tests.Scores.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Scores.IRepository> _repository;
    private Mock<BowlingMegabucks.TournamentManager.Squads.IHandicapCalculatorInternal> _handicapCalculator;

    private BowlingMegabucks.TournamentManager.Scores.Retrieve.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.Scores.IRepository>();
        _handicapCalculator = new Mock<BowlingMegabucks.TournamentManager.Squads.IHandicapCalculatorInternal>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Scores.Retrieve.DataLayer(_repository.Object, _handicapCalculator.Object);
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId[]>())).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.SquadScore>().BuildMock());
        var squadIds = new[] { SquadId.New(), SquadId.New() };

        await _dataLayer.ExecuteAsync(squadIds, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(squadIds), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_ReturnsRepositoryRetrieve()
    {
        var entity1 = new BowlingMegabucks.TournamentManager.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Squad = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
            {
                Date = DateTime.Now
            },
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations =
                [
                    new BowlingMegabucks.TournamentManager.Database.Entities.Registration {Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division() }
                ]
            }
        };

        var entity2 = new BowlingMegabucks.TournamentManager.Database.Entities.SquadScore
        {
            SquadId = SquadId.New(),
            Squad = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
            {
                Date = DateTime.Now
            },
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler
            {
                Id = BowlerId.New(),
                Registrations =
                [
                    new BowlingMegabucks.TournamentManager.Database.Entities.Registration {Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division() }
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
