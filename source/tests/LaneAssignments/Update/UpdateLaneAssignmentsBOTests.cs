
namespace NortheastMegabuck.Tests.LaneAssignments.Update;

[TestFixture]
internal class BusinessLogic
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
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        _businessLogic.Execute(squadId, bowlerId, position);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squadId, bowlerId, position), Times.Once);
    }

    [Test]
    public void Execute_DataLayerExecuteDoesNotThrowException_ErrorIsNull()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        _businessLogic.Execute(squadId, bowlerId, position);

        Assert.That(_businessLogic.Error, Is.Null);
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ErrorSet()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>(), It.IsAny<BowlerId>(), It.IsAny<string>())).Throws(ex);

        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        _businessLogic.Execute(squadId, bowlerId, position);

        Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
    }
}
