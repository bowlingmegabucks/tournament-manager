namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.LaneAssignments.Retrieve.IBusinessLogic> _businessLogic;

    private TournamentManager.LaneAssignments.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.LaneAssignments.Retrieve.IBusinessLogic>();

        _adapter = new TournamentManager.LaneAssignments.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorSetToBusinessLogicError()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsLaneAssignments()
    {
        var laneAssignments = Enumerable.Repeat(new TournamentManager.Models.LaneAssignment { Average = 200 }, 3);
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(laneAssignments);

        var result = (await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(3));
            Assert.That(result.TrueForAll(laneAssignment => laneAssignment.Average == 200));
        });
    }
}
