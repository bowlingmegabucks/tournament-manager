namespace BowlingMegabucks.TournamentManager.Tests.Divisions.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IDataLayer> _dataLayer;

    private BowlingMegabucks.TournamentManager.Divisions.Retrieve.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IDataLayer>();

        _businessLogic = new BowlingMegabucks.TournamentManager.Divisions.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsDataLayerExecuteResults()
    {
        var divisions = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Division(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteNoException_ErrorNull()
    {
        var divisions = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.Division(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var tournamentId = TournamentId.New();

        await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_businessLogic.ErrorDetail, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ErrorFlow()
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
    public async Task ExecuteAsync_DivisionId_DataLayerExecute_CalledCorrectly()
    {
        var divisionId = DivisionId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(divisionId, default).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(divisionId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsDataLayerExecuteResult()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var divisionId = DivisionId.New();

        var actual = await _businessLogic.ExecuteAsync(divisionId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(division));
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var divisionId = DivisionId.New();

        var actual = await _businessLogic.ExecuteAsync(divisionId, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("exception"));
        });
    }
}