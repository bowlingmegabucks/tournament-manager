namespace BowlingMegabucks.TournamentManager.Tests.Bowlers.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.Bowlers.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Bowlers.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_BowlerId_RepositoryRetrieve_CalledCorrectly()
    {
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(new BowlingMegabucks.TournamentManager.Database.Entities.Bowler());

        var bowlerId = BowlerId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(bowlerId, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(bowlerId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerId_ReturnsRepositoryRetrieve()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler
        {
            LastName = "test"
        };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        var actual = await _dataLayer.ExecuteAsync(BowlerId.New(), default).ConfigureAwait(false);

        Assert.That(actual.Name.Last, Is.EqualTo("test"));
    }
}
