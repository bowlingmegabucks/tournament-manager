
namespace NortheastMegabuck.Tests.Registrations.Delete;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Registrations.IRepository> _repository;

    private NortheastMegabuck.Registrations.Delete.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Registrations.IRepository>();

        _dataLayer = new NortheastMegabuck.Registrations.Delete.DataLayer(_repository.Object);
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

    [Test]
    public async Task ExecuteAsync_RegistrationId_RepositoryDelete_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.DeleteAsync(registrationId, cancellationToken), Times.Once);
    }
}
