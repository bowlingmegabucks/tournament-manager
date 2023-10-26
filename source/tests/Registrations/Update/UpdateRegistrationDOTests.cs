
namespace NortheastMegabuck.Tests.Registrations.Update;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Registrations.IRepository> _repository;

    private NortheastMegabuck.Registrations.Update.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Registrations.IRepository>();

        _dataLayer = new NortheastMegabuck.Registrations.Update.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryUpdate_CalledCorrectly([Values]bool superSweeper)
    {
        var registrationId = RegistrationId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(registrationId, superSweeper, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.UpdateAsync(registrationId, superSweeper, cancellationToken), Times.Once);
    }
}
