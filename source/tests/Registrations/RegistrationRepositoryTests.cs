using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Registrations;

[TestFixture]
internal class Repository
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Registrations.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();
        _repository = new NewEnglandClassic.Registrations.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_RegistrationAddedWithGuid()
    {
        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Registration>().SetUpDbContext());

        var registration = new NewEnglandClassic.Database.Entities.Registration();

        var guid = _repository.Add(registration);

        Assert.That(registration.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Registrations).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Registration>().SetUpDbContext());

        var registration = new NewEnglandClassic.Database.Entities.Registration();

        _repository.Add(registration);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }
}
