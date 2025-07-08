namespace NortheastMegabuck.Tests.Squads.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Squads.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Squads.Retrieve.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Squads.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Squads.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsDataLayerExecuteResults()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Models.Squad(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squads);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(squads));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecuteNoException_ErrorNull()
    {
        var squads = Enumerable.Repeat(new NortheastMegabuck.Models.Squad(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squads);

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
        var squad = new NortheastMegabuck.Models.Squad();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad);

        var id = SquadId.New();

        var actual = await _businessLogic.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(squad));
    }

    [Test]
    public async Task ExecuteAsync_SquadId_DataLayerExecuteNoException_ErrorNull()
    {
        var squad = new NortheastMegabuck.Models.Squad();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad);

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