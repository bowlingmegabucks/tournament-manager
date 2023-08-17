namespace NortheastMegabuck.Tests.Sweepers.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Sweepers.IRepository> _repository;

    private NortheastMegabuck.Sweepers.Retrieve.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Sweepers.IRepository>();

        _dataLayer = new NortheastMegabuck.Sweepers.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_RepositoryRetrieve_Called()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperSquad>().BuildMock());

        var id = TournamentId.New();

        await _dataLayer.ExecuteAsync(id, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsRepositoryRetrieveResponse()
    {
        var sweeper1 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 1,
            CashRatio = 2,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
        };

        var sweeper2 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 2,
            CashRatio = 3,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
        };

        var sweeper3 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 3,
            CashRatio = 4,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
        };

        var sweepers = new[] { sweeper1, sweeper2, sweeper3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(sweepers.BuildMock());

        var actual = await _dataLayer.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 1), Is.EqualTo(1));
            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 2), Is.EqualTo(1));
            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 3), Is.EqualTo(1));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RepositoryRetrieve_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 1,
            CashRatio = 1.5m
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
        var entity = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 1,
            CashRatio = 1.5m
        };

        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var squadId = new SquadId();

        var actual = await _dataLayer.ExecuteAsync(squadId, default).ConfigureAwait(false);

        Assert.That(actual.MaxPerPair, Is.EqualTo(entity.MaxPerPair));
    }

    [Test]
    public void SuperSweeperBowlers_RepositorySuperSweeperBowlers_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _dataLayer.SuperSweeperBowlers(tournamentId);

        _repository.Verify(repository => repository.SuperSweeperBowlers(tournamentId), Times.Once);
    }

    [Test]
    public void SuperSweeperBowlers_ReturnsRepositorySuperSweeperBowlerIds()
    {
        var bowlerId1 = BowlerId.New();
        var bowlerId2 = BowlerId.New();
        var bowlerId3 = BowlerId.New();

        var bowlerIds = new[] { bowlerId1, bowlerId2, bowlerId3 };

        _repository.Setup(repository => repository.SuperSweeperBowlers(It.IsAny<TournamentId>())).Returns(bowlerIds.AsQueryable());

        var actual = _dataLayer.SuperSweeperBowlers(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(bowlerId => bowlerId == bowlerId1), Is.EqualTo(1));
            Assert.That(actual.Count(bowlerId => bowlerId == bowlerId2), Is.EqualTo(1));
            Assert.That(actual.Count(bowlerId => bowlerId == bowlerId3), Is.EqualTo(1));
        });
    }
}
