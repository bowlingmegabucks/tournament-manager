namespace NortheastMegabuck.Tests.LaneAssignments.Update;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.LaneAssignments.Update.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.LaneAssignments.Update.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.LaneAssignments.Update.IBusinessLogic>();

        _adapter = new NortheastMegabuck.LaneAssignments.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(squadId, bowlerId, position, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorSetToBusinessLogicError()
    {
        var errorDetail = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(errorDetail);

        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        await _adapter.ExecuteAsync(squadId, bowlerId, position, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(errorDetail));
    }
}
