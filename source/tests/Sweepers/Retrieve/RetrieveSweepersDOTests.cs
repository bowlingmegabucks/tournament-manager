namespace NewEnglandClassic.Tests.Sweepers.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Sweepers.IRepository> _repository;

    private NewEnglandClassic.Sweepers.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NewEnglandClassic.Sweepers.IRepository>();

        _dataLayer = new NewEnglandClassic.Sweepers.Retrieve.DataLayer(_repository.Object);
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
        var sweeper1 = new NewEnglandClassic.Database.Entities.SweeperSquad
        {
            MaxPerPair = 1,
            CashRatio = 2,
            Divisions = Enumerable.Empty<NewEnglandClassic.Database.Entities.SweeperDivision>().ToList()
        };

        var sweeper2 = new NewEnglandClassic.Database.Entities.SweeperSquad
        {
            MaxPerPair = 2,
            CashRatio = 3,
            Divisions = Enumerable.Empty<NewEnglandClassic.Database.Entities.SweeperDivision>().ToList()
        };

        var sweeper3 = new NewEnglandClassic.Database.Entities.SweeperSquad
        {
            MaxPerPair = 3,
            CashRatio = 4,
            Divisions = Enumerable.Empty<NewEnglandClassic.Database.Entities.SweeperDivision>().ToList()
        };

        var sweepers = new[] { sweeper1, sweeper2, sweeper3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(sweepers);

        var actual = _dataLayer.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 1), Is.EqualTo(1));
            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 2), Is.EqualTo(1));
            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 3), Is.EqualTo(1));
        });
    }
}
