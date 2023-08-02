
namespace NortheastMegabuck.Tests.Squads.Complete;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Squads.IRepository> _repository;

    private NortheastMegabuck.Squads.Complete.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Squads.IRepository>();

        _dataLayer = new NortheastMegabuck.Squads.Complete.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositoryComplete_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _dataLayer.Execute(squadId);

        _repository.Verify(repository => repository.Complete(squadId), Times.Once);
    }
}
