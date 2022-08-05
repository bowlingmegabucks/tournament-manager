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
        var guid1 = Guid.NewGuid();
        var guid2 = Guid.NewGuid();
        var guid3 = Guid.NewGuid();

        var tournament1 = new NewEnglandClassic.Database.Entities.Tournament { Id = guid1 };
        var tournament2 = new NewEnglandClassic.Database.Entities.Tournament { Id = guid2 };
        var tournament3 = new NewEnglandClassic.Database.Entities.Tournament { Id = guid3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _repository.Setup(repository=> repository.RetrieveAll()).Returns(tournaments);

        var actual = _dataLayer.Execute().ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.Id == guid1), "tournament1 not returned");
            Assert.That(actual.Any(tournament => tournament.Id == guid2), "tournament2 not returned");
            Assert.That(actual.Any(tournament => tournament.Id == guid3), "tournament3 not returned");
        });
    }

    [Test]
    public void Execute_Id_RepositoryExecuteId_CalledCorrectly()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<Guid>())).Returns(tournament);

        var guid = Guid.NewGuid();

        _dataLayer.Execute(guid);

        _repository.Verify(repository => repository.Retrieve(guid), Times.Once);
    }

    [Test]
    public void Execute_Id_ReturnsTournament()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<Guid>())).Returns(tournament);

        var actual = _dataLayer.Execute(tournament.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public void Execute_DivisionIdRepositoryExecuteDivisionId_CalledCorrectly()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<Guid>())).Returns(tournament);

        var guid = Guid.NewGuid();

        _dataLayer.Execute(guid);

        _repository.Verify(repository => repository.Retrieve(guid), Times.Once);
    }

    [Test]
    public void Execute_DivisionIdReturnsTournament()
    {
        var tournament = new NewEnglandClassic.Database.Entities.Tournament { Id = Guid.NewGuid() };
        _repository.Setup(repository => repository.Retrieve(It.IsAny<Guid>())).Returns(tournament);

        var actual = _dataLayer.Execute(tournament.Id);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }
}
