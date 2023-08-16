
namespace NortheastMegabuck.Tests.Squads.Complete;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.Squads.Complete.IDataLayer> _dataLayer;

    private NortheastMegabuck.Squads.Complete.BusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.Squads.Complete.IDataLayer>();

        _businessLogic = new NortheastMegabuck.Squads.Complete.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ExceptionFlow()
    {
        var ex = new Exception("ex");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error.Message, Is.EqualTo("ex"));
    }
}
