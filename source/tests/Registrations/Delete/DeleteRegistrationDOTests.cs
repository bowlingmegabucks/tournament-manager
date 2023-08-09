
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
    public void Execute_BowlerIdSquadId_RepositoryDelete_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        _dataLayer.Execute(bowlerId, squadId);

        _repository.Verify(repository => repository.Delete(bowlerId, squadId), Times.Once);
    }

    [Test]
    public void Execute_RegistrationId_RepositoryDelete_CalledCorrectly()
    {
        var registrationId = RegistrationId.New();

        _dataLayer.Execute(registrationId);

        _repository.Verify(repository => repository.Delete(registrationId), Times.Once);
    }
}
