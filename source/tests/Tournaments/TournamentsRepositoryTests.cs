using BowlingMegabucks.TournamentManager.Tests.Extensions;

namespace BowlingMegabucks.TournamentManager.Tests.Tournaments;

[TestFixture]
internal sealed class Repository
{
    private Mock<BowlingMegabucks.TournamentManager.Database.IDataContext> _dataContext;

    private BowlingMegabucks.TournamentManager.Tournaments.IRepository _tournamentsRepository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<BowlingMegabucks.TournamentManager.Database.IDataContext>();

        _tournamentsRepository = new BowlingMegabucks.TournamentManager.Tournaments.Repository(_dataContext.Object);
    }

    [Test]
    public void RetrieveAll_ReturnsAllTournaments()
    {
        var tournament1 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament2 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament3 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };

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
    public async Task RetrieveAsync_ReturnsTournament()
    {
        var tournament1 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament2 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        var tournament3 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = await _tournamentsRepository.RetrieveAsync(tournament1.Id, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament1.Id));
    }

    [Test]
    public async Task AddAsync_TournamentAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament();

        var id = await _tournamentsRepository.AddAsync(tournament, default).ConfigureAwait(false);

        Assert.That(tournament.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task AddAsync_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.Tournament>().SetUpDbContext());

        var tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament();
        CancellationToken cancellationToken = default;

        await _tournamentsRepository.AddAsync(tournament, cancellationToken).ConfigureAwait(false);

        _dataContext.Verify(dataContext => dataContext.SaveChangesAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task RetrieveByDivisionAsync_ReturnsTournamentWithDivision()
    {
        var division1 = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() };
        var division2 = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() };
        var division3 = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() };
        var division4 = new BowlingMegabucks.TournamentManager.Database.Entities.Division { Id = BowlingMegabucks.TournamentManager.DivisionId.New() };

        var tournament1 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Divisions = [division1, division2]
        };

        var tournament2 = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament
        {
            Id = TournamentId.New(),
            Divisions = [division3, division4]
        };

        var tournaments = new List<BowlingMegabucks.TournamentManager.Database.Entities.Tournament> { tournament1, tournament2 };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(tournaments.SetUpDbContext());

        var actual = await _tournamentsRepository.RetrieveAsync(division2.Id, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament1.Id));
    }

    [Test]
    public async Task RetrieveBySquadIdAsync_SquadIdIsASquad_ReturnsTournament()
    {
        var squadId = SquadId.New();

        var squads = new[]
        {
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = squadId },
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() }
        };

        var sweepers = new[]
        {
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() }
        };

        var tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament
        {
            Sweepers = sweepers,
            Squads = squads
        };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(new[] { tournament }.SetUpDbContext());

        var result = await _tournamentsRepository.RetrieveAsync(squadId, default).ConfigureAwait(false);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task RetrieveBySquadIdAsync_SquadIdIsASweeper_ReturnsTournament()
    {
        var squadId = SquadId.New();

        var squads = new[]
        {
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad { Id = SquadId.New() }
        };

        var sweepers = new[]
        {
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = SquadId.New() },
            new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad { Id = squadId }
        };

        var tournament = new BowlingMegabucks.TournamentManager.Database.Entities.Tournament
        {
            Sweepers = sweepers,
            Squads = squads
        };

        _dataContext.Setup(dataContext => dataContext.Tournaments).Returns(new[] { tournament }.SetUpDbContext());

        var result = await _tournamentsRepository.RetrieveAsync(squadId, default).ConfigureAwait(false);

        Assert.That(result, Is.Not.Null);
    }
}