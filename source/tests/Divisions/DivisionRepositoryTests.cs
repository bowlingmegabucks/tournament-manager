using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Divisions;

[TestFixture]
internal class Repository
{

    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Divisions.IRepository _divisionsRepository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();

        _divisionsRepository = new NewEnglandClassic.Divisions.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_DivisionAddedWithGuid()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Division>().SetUpDbContext());

        var division = new NewEnglandClassic.Database.Entities.Division();

        var guid = _divisionsRepository.Add(division);

        Assert.That(division.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Division>().SetUpDbContext());

        var division = new NewEnglandClassic.Database.Entities.Division();

        _divisionsRepository.Add(division);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once);
    }
}
