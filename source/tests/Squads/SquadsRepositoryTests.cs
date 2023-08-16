using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Squads;

[TestFixture]
internal sealed class Repository
{
    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.Squads.Repository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();
        _repository = new NortheastMegabuck.Squads.Repository(_dataContext.Object);
    }

    [Test]
    public void Add_SquadAddedWithId()
    {
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.TournamentSquad>().SetUpDbContext());

        var squad = new NortheastMegabuck.Database.Entities.TournamentSquad();

        var id = _repository.Add(squad);

        Assert.That(squad.Id, Is.EqualTo(id));
    }

    [Test]
    public void Add_DataContextSaveChanges_Called()
    {
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.TournamentSquad>().SetUpDbContext());

        var squad = new NortheastMegabuck.Database.Entities.TournamentSquad();

        _repository.Add(squad);

        _dataContext.Verify(dataContext => dataContext.SaveChanges(), Times.Once());
    }

    [Test]
    public void Retrieve_TournamentId_ReturnsSquadsForSelectedTournament()
    {
        var tournamentId = TournamentId.New();

        var squad1 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var squad2 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1
        };

        var squad3 = new NortheastMegabuck.Database.Entities.TournamentSquad
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

    [Test]
    public async Task RetrieveAsync_SquadId_ReturnsSquad()
    {
        var tournamentId = TournamentId.New();

        var squad1 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 1,
        };

        var squad2 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = tournamentId,
            MaxPerPair = 2
        };

        var squad3 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            MaxPerPair = 3
        };

        var squads = new[] { squad1, squad2, squad3 };
        _dataContext.Setup(dataContext => dataContext.Squads).Returns(squads.SetUpDbContext());

        var actual = await _repository.RetrieveAsync(squad2.Id, default).ConfigureAwait(false);

        Assert.That(actual.MaxPerPair, Is.EqualTo(squad2.MaxPerPair));
    }
}
