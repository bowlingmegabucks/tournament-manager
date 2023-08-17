namespace NortheastMegabuck.Tests.Sweepers.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Sweepers.Retrieve.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Sweepers.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Sweepers.Retrieve.BusinessLogic(_dataLayer.Object);
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
        var sweepers = Enumerable.Repeat(new NortheastMegabuck.Models.Sweeper { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(sweepers));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_DataLayerExecuteNoException_ErrorNull()
    {
        var sweepers = Enumerable.Repeat(new NortheastMegabuck.Models.Sweeper { Id = SquadId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var tournamentId = TournamentId.New();

         await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error, Is.Null);
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
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void Execute_SquadId_DataLayerExecute_CalledCorrectly()
    {
        var id = SquadId.New();

        _businessLogic.Execute(id);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(id), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ReturnsDataLayerExecuteResults()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper { Id = SquadId.New() };
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(sweeper);

        var id = SquadId.New();

        var actual = _businessLogic.Execute(id);

        Assert.That(actual, Is.EqualTo(sweeper));
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteNoException_ErrorNull()
    {
        var sweeper = new NortheastMegabuck.Models.Sweeper { Id = SquadId.New() };
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(sweeper);

        var id = SquadId.New();

        _businessLogic.Execute(id);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_SquadId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        var id = SquadId.New();

        var actual = _businessLogic.Execute(id);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}