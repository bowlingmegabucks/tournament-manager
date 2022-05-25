using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Divisions;

[TestFixture]
internal class Repository
{

    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Divisions.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();

        _repository = new NewEnglandClassic.Divisions.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_DivisionAddedWithGuid()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Division>().SetUpDbContext());

        var division = new NewEnglandClassic.Database.Entities.Division();

        var guid = _repository.Add(division);

        Assert.That(division.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Division>().SetUpDbContext());

        var division = new NewEnglandClassic.Database.Entities.Division();

        _repository.Add(division);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once);
    }

    [Test]
    public void ForTournament_ReturnsDivisionsForSelectedTournament()
    {
        var tournamentId = Guid.NewGuid();

        var division1 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = Guid.NewGuid(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division2 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = Guid.NewGuid(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division3 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = Guid.NewGuid(),
            TournamentId = Guid.NewGuid(),
            Name = "No"
        };

        var divisions = new[] { division1, division2, division3 };
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(divisions.SetUpDbContext());

        var actual = _repository.ForTournament(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.Where(division => division.Name == "Yes").Count(), Is.EqualTo(2));
        });
    }
}
