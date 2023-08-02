using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Tournaments;

[TestFixture]
internal sealed class Repository
{
    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.Tournaments.IRepository _tournamentsRepository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();

        _tournamentsRepository = new NortheastMegabuck.Tournaments.Repository(_dataContext.Object);
    }

    [Test]
    public void RetrieveAll_ReturnsAllTournaments()
    {
        var tournament1 = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament2 = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament3 = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };

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
        var tournament1 = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament2 = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament3 = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.Retrieve(tournament1.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament1.Id));
    }

    [Test]
    public void Add_TournamentAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new NortheastMegabuck.Database.Entities.Tournament();

        var id = _tournamentsRepository.Add(tournament);

        Assert.That(tournament.Id, Is.EqualTo(id));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new NortheastMegabuck.Database.Entities.Tournament();

        _tournamentsRepository.Add(tournament);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once);
    }

    [Test]
    public void RetrieveByDivision_ReturnsTournamentWithDivision()
    {
        var division1 = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() };
        var division2 = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() };
        var division3 = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() };
        var division4 = new NortheastMegabuck.Database.Entities.Division { Id = NortheastMegabuck.DivisionId.New() };

        var tournament1 = new NortheastMegabuck.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Divisions = new List<NortheastMegabuck.Database.Entities.Division> { division1, division2 }
        };

        var tournament2 = new NortheastMegabuck.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Divisions = new List<NortheastMegabuck.Database.Entities.Division> { division3, division4 }
        };

        var tournaments = new List<NortheastMegabuck.Database.Entities.Tournament> { tournament1, tournament2 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = _tournamentsRepository.Retrieve(division2.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament1.Id));
    }

    [Test]
    public void RetrieveBySquadId_SquadIdIsASquad_ReturnsTournament()
    {
        var squadId = SquadId.New();

        var squads = new[]
        {
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = squadId },
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() }
        };

        var sweepers = new[]
        {
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() }
        };

        var tournament = new NortheastMegabuck.Database.Entities.Tournament
        {
            Sweepers = sweepers,
            Squads = squads
        };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(new[] { tournament }.SetUpDbContext());

        var result = _tournamentsRepository.Retrieve(squadId);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void RetrieveBySquadId_SquadIdIsASweeper_ReturnsTournament()
    {
        var squadId = SquadId.New();

        var squads = new[]
        {
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.TournamentSquad { Id = SquadId.New() }
        };

        var sweepers = new[]
        {
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new NortheastMegabuck.Database.Entities.SweeperSquad { Id = squadId }
        };

        var tournament = new NortheastMegabuck.Database.Entities.Tournament
        {
            Sweepers = sweepers,
            Squads = squads
        };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(new[] { tournament }.SetUpDbContext());

        var result = _tournamentsRepository.Retrieve(squadId);

        Assert.That(result, Is.Not.Null);
    }
}