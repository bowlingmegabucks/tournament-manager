namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments.Retrieve;

[TestFixture]
internal sealed class BusinessLogic
{
    private Mock<TournamentManager.LaneAssignments.Retrieve.IDataLayer> _dataLayer;

    private TournamentManager.LaneAssignments.Retrieve.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _dataLayer = new Mock<TournamentManager.LaneAssignments.Retrieve.IDataLayer>();

        _businessLogic = new TournamentManager.LaneAssignments.Retrieve.BusinessLogic(_dataLayer.Object);
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
    public async Task ExecuteAsync_ReturnsDataLayerExecute()
    {
        var laneAssignments = Enumerable.Empty<TournamentManager.Models.LaneAssignment>();
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(laneAssignments);

        var actual = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(laneAssignments));
    }

    [Test]
    public async Task ExecuteAsync_DataLayerExecuteThrowsException_ErrorFlow()
    {
        var ex = new Exception("exception");
        _dataLayer.Setup(dataLayer => dataLayer.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ThrowsAsync(ex);

        var actual = await _businessLogic.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Empty);

            Assert.That(_businessLogic.ErrorDetail.Message, Is.EqualTo("exception"));
        });
    }
}
