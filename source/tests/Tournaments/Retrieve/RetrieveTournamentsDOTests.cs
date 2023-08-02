namespace NortheastMegabuck.Tests.Tournaments.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Tournaments.IRepository> _repository;

    private NortheastMegabuck.Tournaments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Tournaments.IRepository>();

        _dataLayer = new NortheastMegabuck.Tournaments.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieveAll_Called()
    {
        await _dataLayer.ExecuteAsync(default);

        _repository.Verify(repository => repository.RetrieveAll(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsTournamentModels()
    {
        var id1 = TournamentId.New();
        var id2 = TournamentId.New();
        var id3 = TournamentId.New();

        var tournament1 = new NortheastMegabuck.Database.Entities.Tournament { Id = id1 };
        var tournament2 = new NortheastMegabuck.Database.Entities.Tournament { Id = id2 };
        var tournament3 = new NortheastMegabuck.Database.Entities.Tournament { Id = id3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _repository.Setup(repository=> repository.RetrieveAll()).Returns(tournaments.BuildMock());

        var actual = (await _dataLayer.ExecuteAsync(default)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Exists(tournament => tournament.Id == id1), "tournament1 not returned");
            Assert.That(actual.Exists(tournament => tournament.Id == id2), "tournament2 not returned");
            Assert.That(actual.Exists(tournament => tournament.Id == id3), "tournament3 not returned");
        });
    }

    [Test]
    public void Execute_Id_RepositoryExecuteId_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(tournament);

        var id = TournamentId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_Id_ReturnsTournament()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(tournament);

        var actual = _dataLayer.Execute(tournament.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public void Execute_DivisionId_RepositoryExecuteDivisionId_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<DivisionId>())).Returns(tournament);

        var id = DivisionId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionId_ReturnsTournament()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<DivisionId>())).Returns(tournament);

        var actual = _dataLayer.Execute(DivisionId.New());

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public void Execute_SquadId_RepositoryExecuteSquadId_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(tournament);

        var id = SquadId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ReturnsTournament()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<SquadId>())).Returns(tournament);

        var actual = _dataLayer.Execute(SquadId.New());

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }
}
