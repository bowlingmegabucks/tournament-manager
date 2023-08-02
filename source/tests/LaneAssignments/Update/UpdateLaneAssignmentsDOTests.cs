
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
    public void Execute_RepositoryUpdate_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var bowlerId = BowlerId.New();
        var position = "21A";

        _dataLayer.Execute(squadId, bowlerId, position);

        _repository.Verify(repository => repository.Update(squadId, bowlerId, position), Times.Once);
    }
}
