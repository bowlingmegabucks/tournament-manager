namespace BowlingMegabucks.TournamentManager.Tests.Sweepers.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<BowlingMegabucks.TournamentManager.Sweepers.Retrieve.IDataLayer> _dataLayer;

    private BowlingMegabucks.TournamentManager.Sweepers.Retrieve.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<BowlingMegabucks.TournamentManager.Sweepers.Retrieve.IDataLayer>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Sweepers.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecute_TournamentId_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsDataLayerExecuteResults()
    {
        var sweepers = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(sweepers));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecuteNoException_ErrorNull()
    {
        var sweepers = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var tournamentId = TournamentId.New();

        await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecute_CalledCorrectly()
    {
        var id = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ReturnsDataLayerExecuteResults()
    {
        var sweeper = new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() };
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeper);

        var id = SquadId.New();

        var actual = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(sweeper));
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecuteNoException_ErrorNull()
    {
        var sweeper = new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New() };
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeper);

        var id = SquadId.New();

        await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var id = SquadId.New();

        var actual = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("exception"));
        });
    }
}