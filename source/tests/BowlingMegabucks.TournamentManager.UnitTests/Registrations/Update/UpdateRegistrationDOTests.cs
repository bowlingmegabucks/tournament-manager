
namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Registrations.IRepository> _repository;

    private TournamentManager.Registrations.Update.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.Registrations.IRepository>();

        _dataLayer = new TournamentManager.Registrations.Update.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryUpdate_CalledCorrectly([Values] bool superSweeper)
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(registrationId, superSweeper, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.UpdateAsync(registrationId, superSweeper, cancellationToken), Times.Once);
    }
}
