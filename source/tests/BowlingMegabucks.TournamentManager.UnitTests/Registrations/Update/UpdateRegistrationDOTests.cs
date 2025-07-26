
namespace BowlingMegabucks.TournamentManager.Tests.Registrations.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Registrations.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Registrations.Update.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<BowlingMegabucks.TournamentManager.Registrations.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Registrations.Update.DataLayer(_repository.Object);
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
