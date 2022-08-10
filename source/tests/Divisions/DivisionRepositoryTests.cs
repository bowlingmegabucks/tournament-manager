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
    public void Add_DivisionAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Division>().SetUpDbContext());

        var division = new NewEnglandClassic.Database.Entities.Division();

        var id = _repository.Add(division);

        Assert.That(division.Id, Is.EqualTo(id));
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
    public void Execute_ReturnsDivisionsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var division1 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division2 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = tournamentId,
            Name = "Yes"
        };

        var division3 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var divisions = new[] { division1, division2, division3 };
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(divisions.SetUpDbContext());

        var actual = _repository.Retrieve(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.Count(division => division.Name == "Yes"), Is.EqualTo(2));
        });
    }

    [Test]
    public void Retrieve_ReturnsDivision()
    {
        var divisionId = DivisionId.New();

        var division1 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = divisionId,
            TournamentId = TournamentId.New(),
            Name = "Yes"
        };

        var division2 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var division3 = new NewEnglandClassic.Database.Entities.Division
        {
            Id = DivisionId.New(),
            TournamentId = TournamentId.New(),
            Name = "No"
        };

        var divisions = new[] { division1, division2, division3 };
        _dataContext.Setup(dataContext => dataContext.Divisions).Returns(divisions.SetUpDbContext());

        var division = _repository.Retrieve(divisionId);

        Assert.That(division.Id, Is.EqualTo(divisionId));
    }
}
