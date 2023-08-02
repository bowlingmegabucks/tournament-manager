namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Bowlers.IRepository> _repository;

    private NortheastMegabuck.Bowlers.Search.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Bowlers.IRepository>();

        _dataLayer = new NortheastMegabuck.Bowlers.Search.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositorySearch_CalledCorrectly()
    {
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        _dataLayer.Execute(searchCriteria);

        _repository.Verify(repository => repository.Search(searchCriteria), Times.Once);
    }

    [Test]
    public void Execute_ReturnsCorrectResult()
    {
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria();

        var bowlers = new List<NortheastMegabuck.Database.Entities.Bowler>
        {
            new NortheastMegabuck.Database.Entities.Bowler
            {
                FirstName = "John",
                LastName = "Doe"
            },
            
            new NortheastMegabuck.Database.Entities.Bowler
            {
                FirstName = "Jane",
                LastName = "Doe"
            },
            
            new NortheastMegabuck.Database.Entities.Bowler
            {
                FirstName = "John",
                LastName = "Smith"
            }
        };

        _repository.Setup(repository => repository.Search(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>())).Returns(bowlers);

        var actual = _dataLayer.Execute(searchCriteria).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Count(bowler => bowler.Name.Last == "Smith"), Is.EqualTo(1));
            Assert.That(actual.Count(bowler => bowler.Name.First == "John"), Is.EqualTo(2));
            Assert.That(actual.Count(bowler => bowler.Name.Last == "Doe"), Is.EqualTo(2));
            Assert.That(actual.Count(bowler => bowler.Name.First == "Jane"), Is.EqualTo(1));
        });
    }
}
