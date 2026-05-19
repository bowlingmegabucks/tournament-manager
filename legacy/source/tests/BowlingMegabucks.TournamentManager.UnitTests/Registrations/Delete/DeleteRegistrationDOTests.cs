
namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Delete;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Registrations.IRepository> _repository;

    private TournamentManager.Registrations.Delete.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.Registrations.IRepository>();

        _dataLayer = new TournamentManager.Registrations.Delete.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_RepositoryDelete_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.DeleteAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }
}
