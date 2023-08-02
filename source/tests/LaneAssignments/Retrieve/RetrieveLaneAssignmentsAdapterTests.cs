namespace NortheastMegabuck.Tests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.LaneAssignments.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.LaneAssignments.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.LaneAssignments.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.LaneAssignments.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public void Execute_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _adapter.Execute(squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_ErrorSetToBusinessLogicError()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        _adapter.Execute(SquadId.New());

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_ReturnsLaneAssignments()
    {
        var laneAssignments = Enumerable.Repeat(new NortheastMegabuck.Models.LaneAssignment { Average = 200}, 3);
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<SquadId>())).Returns(laneAssignments);

        var result = _adapter.Execute(SquadId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result.TrueForAll(laneAssignment => laneAssignment.Average == 200));
        });
    }
}
