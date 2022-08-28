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
    public void Add_SquadAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.TournamentSquad>().SetUpDbContext());

        var squad = new NewEnglandClassic.Database.Entities.TournamentSquad();

        var id = _repository.Add(squad);

        Assert.That(squad.Id, Is.EqualTo(id));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(Enumerable.Empty<NewEnglandClassic.Database.Entities.TournamentSquad>().SetUpDbContext());

        var squad = new NewEnglandClassic.Database.Entities.TournamentSquad();

        _repository.Add(squad);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }

    [Test]
    public void Execute_ReturnsSquadsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var squad1 = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var squad2 = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var squad3 = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            MaxPerPair = 2
        };

        var squads = new[] { squad1, squad2, squad3 };
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(squads.SetUpDbContext());

        var actual = _repository.Retrieve(tournamentId);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 1), Is.EqualTo(2));
        });
    }
}
