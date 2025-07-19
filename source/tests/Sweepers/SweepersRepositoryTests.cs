using BowlingMegabucks.TournamentManager.Tests.Extensions;

namespace BowlingMegabucks.TournamentManager.Tests.Sweepers;

[TestFixture]
internal sealed class Repository
{
    private Mock<BowlingMegabucks.TournamentManager.Database.IDataContext> _dataContext;

    private BowlingMegabucks.TournamentManager.Sweepers.Repository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<BowlingMegabucks.TournamentManager.Database.IDataContext>();
        _repository = new BowlingMegabucks.TournamentManager.Sweepers.Repository(_dataContext.Object);
    }

    [Test]
    public async Task AddAsync_SquadAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad>().SetUpDbContext());

        var sweeper = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad();

        var id = await _repository.AddAsync(sweeper, default).ConfigureAwait(false);

        Assert.That(sweeper.Id, Is.EqualTo(id));
    }

    [Test]
    public async Task AddAsync_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(Enumerable.Empty<BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad>().SetUpDbContext());

        var sweeper = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad();

        CancellationToken cancellationToken = default;
        await _repository.AddAsync(sweeper, cancellationToken).ConfigureAwait(false);

        _dataContext.Verify(dataContext => dataContext.SaveChangesAsync(cancellationToken), Times.Once());
    }

    [Test]
    public void Retrieve_TournamentId_ReturnsSquadsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var sweeper1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var sweeper2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var sweeper3 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            MaxPerPair = 2
        };

        var sweepers = new[] { sweeper1, sweeper2, sweeper3 };
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(sweepers.SetUpDbContext());

        var actual = _repository.Retrieve(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 1), Is.EqualTo(2));
        });
    }

    [Test]
    public async Task RetrieveAsync_SquadId_ReturnsSquad()
    {
        var tournamentId = TournamentId.New();

        var sweeper1 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1,
        };

        var sweeper2 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 2
        };

        var sweeper3 = new BowlingMegabucks.TournamentManager.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            MaxPerPair = 3
        };

        var sweepers = new[] { sweeper1, sweeper2, sweeper3 };
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(sweepers.SetUpDbContext());

        var actual = await _repository.RetrieveAsync(sweeper2.Id, default).ConfigureAwait(false);

        Assert.That(actual.MaxPerPair, Is.EqualTo(sweeper2.MaxPerPair));
    }
}
