
namespace NortheastMegabuck.Tests.LaneAssignments.Update;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.LaneAssignments.Update.IDataLayer> _dataLayer;

    private NortheastMegabuck.LaneAssignments.Update.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.LaneAssignments.Update.IDataLayer>();

        _businessLogic = new NortheastMegabuck.LaneAssignments.Update.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";
        CancellationToken cancellationToken = default;

        await _businessLogic.ExecuteAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);

        _dataLayer.Verify(dataLayer => dataLayer.ExecuteAsync(squadId, bowlerId, position, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteDoesNotThrowException_ErrorIsNull()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        await _businessLogic.ExecuteAsync(squadId, bowlerId, position, default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ErrorSet()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<BowlerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        await _businessLogic.ExecuteAsync(squadId, bowlerId, position, default).ConfigureAwait(false);

        Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
    }
}
