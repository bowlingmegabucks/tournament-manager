
namespace NortheastMegabuck.Tests.Scores.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Scores.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.Scores.Retrieve.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Scores.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Scores.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_DataLayerExecute_SquadIds_CalledCorrectly()
    {
        var squadIds = new[] { SquadId.New(), SquadId.New() };
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadIds, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(squadIds, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var result = await _businessLogic.ExecuteAsync(new[] { SquadId.New(), SquadId.New() }, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadIds_ReturnsDataLayerExecute()
    {
        var scores = new Mock<IEnumerable<NortheastMegabuck.Models.SquadScore>>();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<IEnumerable<SquadId>>(), It.IsAny<CancellationToken>())).ReturnsAsync(scores.Object);

        var actual = await _businessLogic.ExecuteAsync(new[] { SquadId.New(), SquadId.New() }, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(scores.Object));
    }
}
