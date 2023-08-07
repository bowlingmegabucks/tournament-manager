namespace NortheastMegabuck.Tests.Divisions.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Divisions.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Divisions.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Divisions.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Divisions.Retrieve.BusinessLogic(_dataLayer.Object);
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
        var divisions = Enumerable.Repeat(new NortheastMegabuck.Models.Division(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var tournamentId = TournamentId.New();

        var actual = await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteNoException_ErrorNull()
    {
        var divisions = Enumerable.Repeat(new NortheastMegabuck.Models.Division(), 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var tournamentId = TournamentId.New();

        await _businessLogic.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error, Is.Null);
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
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public void Execute_DivisionId_DataLayerExecute_CalledCorrectly()
    {
        var divisionId = DivisionId.New();

        _businessLogic.Execute(divisionId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(divisionId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecuteResult()
    {
        var division = new NortheastMegabuck.Models.Division();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Returns(division);

        var divisionId = DivisionId.New();

        var actual = _businessLogic.Execute(divisionId);

        Assert.That(actual, Is.EqualTo(division));
    }

    [Test]
    public void Execute_DataLayerExecutetNoException_ErrorNull()
    {
        var division = new NortheastMegabuck.Models.Division();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Returns(division);

        var divisionId = DivisionId.New();

        _businessLogic.Execute(divisionId);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DivisionId_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<DivisionId>())).Throws(ex);

        var divisionId = DivisionId.New();

        var actual = _businessLogic.Execute(divisionId);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}