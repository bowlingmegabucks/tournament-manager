namespace NortheastMegabuck.Tests.Divisions.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Divisions.IRepository> _repository;

    private NortheastMegabuck.Divisions.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Divisions.IRepository>();

        _dataLayer = new NortheastMegabuck.Divisions.Retrieve.DataLayer(_repository.Object);
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
        var division1 = new NortheastMegabuck.Database.Entities.Division
        {
            Name = "Division 1"
        };

        var division2 = new NortheastMegabuck.Database.Entities.Division
        {
            Name = "Division 2"
        };

        var division3 = new NortheastMegabuck.Database.Entities.Division
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
        var division = new NortheastMegabuck.Database.Entities.Division();
        _repository.Setup(repository => repository.Retrieve(It.IsAny<NortheastMegabuck.DivisionId>())).Returns(division);

        var id = NortheastMegabuck.DivisionId.New();

        _dataLayer.Execute(id);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public void Execute_DivisionId_ReturnsRepositoryRetrieveResponse()
    {
        var division = new NortheastMegabuck.Database.Entities.Division { Name = "name"};
        _repository.Setup(repository => repository.Retrieve(It.IsAny<NortheastMegabuck.DivisionId>())).Returns(division);

        var id = NortheastMegabuck.DivisionId.New();

        var actual = _dataLayer.Execute(id);

        Assert.That(actual.Name, Is.EqualTo(division.Name));
    }
}
