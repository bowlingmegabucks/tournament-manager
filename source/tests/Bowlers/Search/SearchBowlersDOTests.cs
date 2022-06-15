namespace NewEnglandClassic.Tests.Bowlers.Search;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Bowlers.IRepository> _repository;

    private NewEnglandClassic.Bowlers.Search.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NewEnglandClassic.Bowlers.IRepository>();

        _dataLayer = new NewEnglandClassic.Bowlers.Search.DataLayer(_repository.Object);
    }

    [Test]
    public void Execute_RepositorySearch_CalledCorrectly()
    {
        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria();

        _dataLayer.Execute(searchCriteria);

        _repository.Verify(repository => repository.Search(searchCriteria), Times.Once);
    }

    [Test]
    public void Execute_ReturnsCorrectResult()
    {
        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria();

        var bowlers = new List<NewEnglandClassic.Database.Entities.Bowler>
        {
            new NewEnglandClassic.Database.Entities.Bowler
            {
                FirstName = "John",
                LastName = "Doe"
            },
            
            new NewEnglandClassic.Database.Entities.Bowler
            {
                FirstName = "Jane",
                LastName = "Doe"
            },
            
            new NewEnglandClassic.Database.Entities.Bowler
            {
                FirstName = "John",
                LastName = "Smith"
            }
        };

        _repository.Setup(repository => repository.Search(It.IsAny<NewEnglandClassic.Models.BowlerSearchCriteria>())).Returns(bowlers);

        var actual = _dataLayer.Execute(searchCriteria).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Count(bowler => bowler.LastName == "Smith"), Is.EqualTo(1));
            Assert.That(actual.Count(bowler => bowler.FirstName == "John"), Is.EqualTo(2));
            Assert.That(actual.Count(bowler => bowler.LastName == "Doe"), Is.EqualTo(2));
            Assert.That(actual.Count(bowler => bowler.FirstName == "Jane"), Is.EqualTo(1));
        });
    }
}
