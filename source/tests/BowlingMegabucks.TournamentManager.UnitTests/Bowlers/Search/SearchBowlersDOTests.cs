using MockQueryable;

namespace BowlingMegabucks.TournamentManager.UnitTests.Bowlers.Search;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Bowlers.IRepository> _repository;

    private TournamentManager.Bowlers.Search.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.Bowlers.IRepository>();

        _dataLayer = new TournamentManager.Bowlers.Search.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositorySearch_CalledCorrectly()
    {
        _repository.Setup(repository => repository.Search(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>())).Returns(Array.Empty<TournamentManager.Database.Entities.Bowler>().BuildMock());
        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria();

        await _dataLayer.ExecuteAsync(searchCriteria, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Search(searchCriteria), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsCorrectResult()
    {
        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria();

        var bowlers = new List<TournamentManager.Database.Entities.Bowler>
        {
            new() {
                Id = BowlerId.New(),
                FirstName = "John",
                LastName = "Doe"
            },

            new() {
                Id = BowlerId.New(),
                FirstName = "Jane",
                LastName = "Doe"
            },

            new() {
                Id = BowlerId.New(),
                FirstName = "John",
                LastName = "Smith"
            }
        };

        _repository.Setup(repository => repository.Search(It.IsAny<TournamentManager.Models.BowlerSearchCriteria>())).Returns(bowlers.BuildMock());

        var actual = (await _dataLayer.ExecuteAsync(searchCriteria, default).ConfigureAwait(false)).ToList();

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
