namespace NewEnglandClassic.Tests.Registrations.Add;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Registrations.IEntityMapper> _mapper;
    private Mock<NewEnglandClassic.Registrations.IRepository> _repository;

    private NewEnglandClassic.Registrations.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NewEnglandClassic.Registrations.IEntityMapper>();
        _repository = new Mock<NewEnglandClassic.Registrations.IRepository>();

        _dataLayer = new NewEnglandClassic.Registrations.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public void Execute_MapperExecute_CalledCorrectly()
    {
        var registration = new NewEnglandClassic.Models.Registration();
        _dataLayer.Execute(registration);

        _mapper.Verify(mapper => mapper.Execute(registration), Times.Once);
    }

    [Test]
    public void Execute_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NewEnglandClassic.Database.Entities.Registration();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NewEnglandClassic.Models.Registration>())).Returns(entity);

        var model = new NewEnglandClassic.Models.Registration();
        _dataLayer.Execute(model);

        _repository.Verify(repository => repository.Add(entity), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryAddResponse()
    {
        var registrationId = RegistrationId.New();
        _repository.Setup(repository => repository.Add(It.IsAny<NewEnglandClassic.Database.Entities.Registration>())).Returns(registrationId);

        var model = new NewEnglandClassic.Models.Registration();
        var actual = _dataLayer.Execute(model);

        Assert.That(actual, Is.EqualTo(registrationId));
    }
}
