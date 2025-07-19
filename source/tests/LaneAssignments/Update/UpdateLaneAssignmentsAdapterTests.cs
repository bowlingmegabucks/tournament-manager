namespace BowlingMegabucks.TournamentManager.Tests.LaneAssignments.Update;

[TestFixture]
internal sealed class Adapter
{
    private Mock<BowlingMegabucks.TournamentManager.LaneAssignments.Update.IBusinessLogic> _businessLogic;

#pragma warning disable CA1859 // Use concrete types when possible for improved performance
    private BowlingMegabucks.TournamentManager.LaneAssignments.Update.IAdapter _adapter;
#pragma warning restore CA1859 // Use concrete types when possible for improved performance

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<BowlingMegabucks.TournamentManager.LaneAssignments.Update.IBusinessLogic>();

        _adapter = new BowlingMegabucks.TournamentManager.LaneAssignments.Update.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var originalPosition = "1A";
        var position = "21A";
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, bowlerId, originalPosition, position, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(squadId, bowlerId, originalPosition, position, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorSetToBusinessLogicError()
    {
        var errorDetail = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(errorDetail);

        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var originalPosition = "1A";
        var position = "21A";

        await _adapter.ExecuteAsync(squadId, bowlerId, originalPosition, position, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(errorDetail));
    }
}
