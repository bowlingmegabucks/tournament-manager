namespace NortheastMegabuck.Tests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<NortheastMegabuck.LaneAssignments.Retrieve.IDataLayer> _dataLayer;

    private NortheastMegabuck.LaneAssignments.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<NortheastMegabuck.LaneAssignments.Retrieve.IDataLayer>();

        _businessLogic = new NortheastMegabuck.LaneAssignments.Retrieve.BusinessLogic(_dataLayer.Object);
    }

    [Test]
    public void Execute_DataLayerExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _dataLayer.Verify(dataLayer => dataLayer.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_ReturnsDataLayerExecute()
    {
        var laneAssignments = Enumerable.Empty<NortheastMegabuck.Models.LaneAssignment>();
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Returns(laneAssignments);

        var actual = _businessLogic.Execute(SquadId.New());

        Assert.That(actual, Is.EqualTo(laneAssignments));
    }

    [Test]
    public void Execute_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.Execute(It.IsAny<SquadId>())).Throws(ex);

        var actual = _businessLogic.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);

            Assert.That(_businessLogic.Error.Message, Is.EqualTo("exception"));
        });
    }
}
