
namespace NortheastMegabuck.Tests.LaneAssignments.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.LaneAssignments.IRepository> _repository;

    private NortheastMegabuck.LaneAssignments.Update.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.LaneAssignments.IRepository>();

        _dataLayer = new NortheastMegabuck.LaneAssignments.Update.DataLayer(_repository.Object);
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
