using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Tournaments;

[TestFixture]
internal class Repository
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Tournaments.IRepository _tournamentsRepository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();

        _tournamentsRepository = new NewEnglandClassic.Tournaments.Repository(_dataContext.Object);
    }

    [Test]
    public void RetrieveAll_ReturnsAllTournaments()
    {
        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament3 = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.RetrieveAll();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.Id == tournament1.Id), Is.True, "tournament1 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == tournament2.Id), Is.True, "tournament2 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == tournament3.Id), Is.True, "tournament3 Id Not Found");
        });
    }

    [Test]
    public void Retrieve_ReturnsTournament()
    {
        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament3 = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.Retrieve(tournament1.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament1.Id));
    }

    [Test]
    public void Add_TournamentAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new NewEnglandClassic.Database.Entities.Tournament();

        var id = _tournamentsRepository.Add(tournament);

        Assert.That(tournament.Id, Is.EqualTo(id));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new NewEnglandClassic.Database.Entities.Tournament();

        _tournamentsRepository.Add(tournament);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once);
    }

    [Test]
    public void RetrieveByDivision_ReturnsTournamentWithDivision()
    {
        var division1 = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() };
        var division2 = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() };
        var division3 = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() };
        var division4 = new NewEnglandClassic.Database.Entities.Division { Id = DivisionId.New() };

        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Divisions = new List<NewEnglandClassic.Database.Entities.Division> { division1, division2 }
        };

        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Divisions = new List<NewEnglandClassic.Database.Entities.Division> { division3, division4 }
        };

        var tournaments = new List<NewEnglandClassic.Database.Entities.Tournament> { tournament1, tournament2 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.Retrieve(division2.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament1.Id));
    }
}