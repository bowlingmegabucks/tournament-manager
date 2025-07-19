
namespace BowlingMegabucks.TournamentManager.Tests.LaneAssignments.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.LaneAssignments.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.LaneAssignments.Update.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.LaneAssignments.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.LaneAssignments.Update.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryUpdate_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.UpdateAsync(squadId, bowlerId, position, cancellationToken), Times.Once);
    }
}
