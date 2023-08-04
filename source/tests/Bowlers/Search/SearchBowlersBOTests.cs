namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Bowlers.Search.IDataLayer> _dataLayer;

    private NortheastMegabuck.Bowlers.Search.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Bowlers.Search.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Bowlers.Search.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_CalledCorrectly()
    {
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(searchCriteria, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(searchCriteria, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsDataLayerExecuteResults()
    {
        var divisions = Enumerable.Repeat(new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        var actual = await _businessLogic.ExecuteAsync(searchCriteria, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(divisions));
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteNoException_ErrorNull()
    {
        var divisions = Enumerable.Repeat(new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }, 2);
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        await _businessLogic.ExecuteAsync(searchCriteria, default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        var actual = await _businessLogic.ExecuteAsync(searchCriteria, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}