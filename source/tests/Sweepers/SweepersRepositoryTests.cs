using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Sweepers;

[TestFixture]
internal class Repository
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Sweepers.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();
        _repository = new NewEnglandClassic.Sweepers.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_SquadAddedWithGuid()
    {
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.SweeperSquad>().SetUpDbContext());

        var sweeper = new NewEnglandClassic.Database.Entities.SweeperSquad();

        var guid = _repository.Add(sweeper);

        Assert.That(sweeper.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.SweeperSquad>().SetUpDbContext());

        var sweeper = new NewEnglandClassic.Database.Entities.SweeperSquad();

        _repository.Add(sweeper);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }

    [Test]
    public void ForTournament_ReturnsSquadsForSelectedTournament()
    {
        var tournamentId = Guid.NewGuid();

        var sweeper1 = new NewEnglandClassic.Database.Entities.SweeperSquad
        {
            Id = Guid.NewGuid(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var sweeper2 = new NewEnglandClassic.Database.Entities.SweeperSquad
        {
            Id = Guid.NewGuid(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var sweeper3 = new NewEnglandClassic.Database.Entities.SweeperSquad
        {
            Id = Guid.NewGuid(),
            TournamentId = Guid.NewGuid(),
            MaxPerPair = 2
        };

        var sweepers = new[] { sweeper1, sweeper2, sweeper3 };
        _dataContext.Setup(dataContext => dataContext.Sweepers).Returns(sweepers.SetUpDbContext());

        var actual = _repository.ForTournament(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 1), Is.EqualTo(2));
        });
    }
}
