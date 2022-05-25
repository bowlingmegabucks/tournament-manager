using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Squads;

[TestFixture]
internal class Repository
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Squads.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();
        _repository = new NewEnglandClassic.Squads.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_SquadAddedWithGuid()
    {
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.TournamentSquad>().SetUpDbContext());

        var squad = new NewEnglandClassic.Database.Entities.TournamentSquad();

        var guid = _repository.Add(squad);

        Assert.That(squad.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.TournamentSquad>().SetUpDbContext());

        var squad = new NewEnglandClassic.Database.Entities.TournamentSquad();

        _repository.Add(squad);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }
}
