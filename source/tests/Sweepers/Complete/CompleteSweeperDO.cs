
namespace NortheastMegabuck.Tests.Sweepers.Complete;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Sweepers.IRepository> _repository;

    private NortheastMegabuck.Sweepers.Complete.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Sweepers.IRepository>();

        _dataLayer = new NortheastMegabuck.Sweepers.Complete.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryComplete_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.CompleteAsync(squadId, cancellationToken), Times.Once);
    }
}
