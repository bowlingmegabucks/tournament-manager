namespace NewEnglandClassic.Tests.Tournaments.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Tournaments.IRepository> _repository;

    private NewEnglandClassic.Tournaments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NewEnglandClassic.Tournaments.IRepository>();

        _dataLayer = new NewEnglandClassic.Tournaments.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositoryRetrieveAll_Called()
    {
        _dataLayer.Execute();

        _repository.Verify(repository => repository.RetrieveAll(), Times.Once);
    }

    [Test]
    public void Execute_ReturnsTournamentModels()
    {
        var id1 = TournamentId.New();
        var id2 = TournamentId.New();
        var id3 = TournamentId.New();

        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament { Id = id1 };
        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament { Id = id2 };
        var tournament3 = new NewEnglandClassic.Database.Entities.Tournament { Id = id3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _repository.Setup(repository=> repository.RetrieveAll()).Returns(tournaments);

        var actual = _dataLayer.Execute().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.Id == id1), "tournament1 not returned");
            Assert.That(actual.Any(tournament => tournament.Id == id2), "tournament2 not returned");
            Assert.That(actual.Any(tournament => tournament.Id == id3), "tournament3 not returned");
        });
    }

    [Test]
    public void Execute_Id_RepositoryExecuteId_CalledCorrectly()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(tournament);

        var id = TournamentId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_Id_ReturnsTournament()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(tournament);

        var actual = _dataLayer.Execute(tournament.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public void Execute_DivisionIdRepositoryExecuteDivisionId_CalledCorrectly()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(tournament);

        var id = TournamentId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionIdReturnsTournament()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(tournament);

        var actual = _dataLayer.Execute(tournament.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }
}
