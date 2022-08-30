namespace NortheastMegabuck.Tests.Registrations.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Registrations.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Registrations.IRepository> _repository;

    private NortheastMegabuck.Registrations.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Registrations.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Registrations.IRepository>();

        _dataLayer = new NortheastMegabuck.Registrations.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var registration = new NortheastMegabuck.Models.Registration();
        _dataLayer.Execute(registration);

        _mapper.Verify(mapper => mapper.Execute(registration), Times.Once);
    }

    [Test]
    public void Execute_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.Registration();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(entity);

        var model = new NortheastMegabuck.Models.Registration();
        _dataLayer.Execute(model);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryAddResponse()
    {
        var registrationId = RegistrationId.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NortheastMegabuck.Database.Entities.Registration>())).Returns(registrationId);

        var model = new NortheastMegabuck.Models.Registration();
        var actual = _dataLayer.Execute(model);

        Assert.That(actual, Is.EqualTo(registrationId));
    }
}
