
namespace NortheastMegabuck.Tests.Sweepers.Complete;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Sweepers.IRepository> _repository;

    private NortheastMegabuck.Sweepers.Complete.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Sweepers.IRepository>();

        _dataLayer = new NortheastMegabuck.Sweepers.Complete.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositoryComplete_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _dataLayer.Execute(squadId);

        _repository.Verify(repository => repository.Complete(squadId), Times.Once);
    }
}
