
namespace NortheastMegabuck.Tests.Bowlers.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Bowlers.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Bowlers.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Bowlers.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Bowlers.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_BowlerId_DataLayerExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(bowlerId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(bowlerId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerId_DataLayerExecuteSuccessful_ReturnsBowler()
    {
        var bowler = new NortheastMegabuck.Models.Bowler();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        var actual = await _businessLogic.ExecuteAsync(BowlerId.New(), default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(bowler));
    }

    [Test]
    public async Task ExecuteAsync_BowlerId_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("ex");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var actual = await _businessLogic.ExecuteAsync(BowlerId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Null);

            Assert.That(_businessLogic.Error.Message, Is.EqualTo("ex"));
        });
    }
}
