namespace NewEnglandClassic.Tests.Divisions.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Divisions.IRepository> _repository;

    private NewEnglandClassic.Divisions.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NewEnglandClassic.Divisions.IRepository>();

        _dataLayer = new NewEnglandClassic.Divisions.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public void ForTournament_RepositoryForTournament_Called()
    {
        var guid = Guid.NewGuid();

        _dataLayer.ForTournament(guid);

        _repository.Verify(repository => repository.ForTournament(guid), Times.Once);
    }

    [Test]
    public void ForTournament_ReturnsRepositoryForTournamentResponse()
    {
        var division1 = new NewEnglandClassic.Database.Entities.Division
        {
            Name = "Division 1"
        };

        var division2 = new NewEnglandClassic.Database.Entities.Division
        {
            Name = "Division 2"
        };

        var division3 = new NewEnglandClassic.Database.Entities.Division
        {
            Name = "Division 3"
        };

        var divisions = new[] { division1, division2, division3 };

        _repository.Setup(repository => repository.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var actual = _dataLayer.ForTournament(Guid.NewGuid());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(division => division.Name == "Division 1"), Is.EqualTo(1));
            Assert.That(actual.Count(division => division.Name == "Division 2"), Is.EqualTo(1));
            Assert.That(actual.Count(division => division.Name == "Division 3"), Is.EqualTo(1));
        });
    }

    [Test]
    public void Execute_RepositoryRetrieve_CalledCorrectly()
    {
        var division = new NewEnglandClassic.Database.Entities.Division();
        _repository.Setup(repository => repository.Retrieve(It.IsAny<Guid>())).Returns(division);

        var id = Guid.NewGuid();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryRetrieveResponse()
    {
        var division = new NewEnglandClassic.Database.Entities.Division { Name = "name"};
        _repository.Setup(repository => repository.Retrieve(It.IsAny<Guid>())).Returns(division);

        var id = Guid.NewGuid();

        var actual = _dataLayer.Execute(id);

        Assert.That(actual.Name, Is.EqualTo(division.Name));
    }
}
