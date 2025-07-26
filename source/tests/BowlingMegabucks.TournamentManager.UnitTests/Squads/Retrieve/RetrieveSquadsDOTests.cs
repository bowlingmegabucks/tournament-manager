using MockQueryable;

namespace BowlingMegabucks.TournamentManager.Tests.Squads.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Squads.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Squads.Retrieve.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.Squads.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Squads.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RepositoryRetrieve_Called()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad>().BuildMock());

        var id = TournamentId.New();

        await _dataLayer.ExecuteAsync(id, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsRepositoryRetrieveResponse()
    {
        var squad1 = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            MaxPerPair = 1
        };

        var squad2 = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            MaxPerPair = 2
        };

        var squad3 = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            MaxPerPair = 3
        };

        var squads = new[] { squad1, squad2, squad3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(squads.BuildMock());

        var actual = await _dataLayer.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(squad => squad.MaxPerPair == 1), Is.EqualTo(1));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 2), Is.EqualTo(1));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 3), Is.EqualTo(1));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RepositoryRetrieve_CalledCorrectly()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            MaxPerPair = 1
        };

        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var squadId = new SquadId();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ReturnsModel()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad
        {
            MaxPerPair = 1
        };

        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var squadId = new SquadId();

        var actual = await _dataLayer.ExecuteAsync(squadId, default).ConfigureAwait(false);

        Assert.That(actual.MaxPerPair, Is.EqualTo(entity.MaxPerPair));
    }
}
