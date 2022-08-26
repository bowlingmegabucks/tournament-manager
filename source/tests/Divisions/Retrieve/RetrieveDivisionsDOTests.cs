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
    public void Execute_RepositoryRetrieve_Called()
    {
        var id = TournamentId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_ReturnsRepositoryRetrieveResponse()
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

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(divisions);

        var actual = _dataLayer.Execute(TournamentId.New());

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
        _repository.Setup(repository => repository.Retrieve(It.IsAny<NewEnglandClassic.Divisions.Id>())).Returns(division);

        var id = NewEnglandClassic.Divisions.Id.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionId_ReturnsRepositoryRetrieveResponse()
    {
        var division = new NewEnglandClassic.Database.Entities.Division { Name = "name"};
        _repository.Setup(repository => repository.Retrieve(It.IsAny<NewEnglandClassic.Divisions.Id>())).Returns(division);

        var id = NewEnglandClassic.Divisions.Id.New();

        var actual = _dataLayer.Execute(id);

        Assert.That(actual.Name, Is.EqualTo(division.Name));
    }
}
