using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Sweepers;

[TestFixture]
internal sealed class Repository
{
    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.Sweepers.Repository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();
        _repository = new NortheastMegabuck.Sweepers.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_SquadAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperSquad>().SetUpDbContext());

        var sweeper = new NortheastMegabuck.Database.Entities.SweeperSquad();

        var id = _repository.Add(sweeper);

        Assert.That(sweeper.Id, Is.EqualTo(id));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperSquad>().SetUpDbContext());

        var sweeper = new NortheastMegabuck.Database.Entities.SweeperSquad();

        _repository.Add(sweeper);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }

    [Test]
    public void Retrieve_TournamentId_ReturnsSquadsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var sweeper1 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var sweeper2 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var sweeper3 = new NortheastMegabuck.Database.Entities.SweeperSquad
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
    public void Retrieve_SquadId_ReturnsSquad()
    {
        var tournamentId = TournamentId.New();

        var sweeper1 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1,
        };

        var sweeper2 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 2
        };

        var sweeper3 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            MaxPerPair = 3
        };

        var sweepers = new[] { sweeper1, sweeper2, sweeper3 };
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(sweepers.SetUpDbContext());

        var actual = _repository.Retrieve(sweeper2.Id);

        Assert.That(actual.MaxPerPair, Is.EqualTo(sweeper2.MaxPerPair));
    }
}
