namespace NortheastMegabuck.Tests.Sweepers.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Sweepers.IRepository> _repository;

    private NortheastMegabuck.Sweepers.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Sweepers.IRepository>();

        _dataLayer = new NortheastMegabuck.Sweepers.Retrieve.DataLayer(_repository.Object);
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
        var sweeper1 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 1,
            CashRatio = 2,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
        };

        var sweeper2 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 2,
            CashRatio = 3,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
        };

        var sweeper3 = new NortheastMegabuck.Database.Entities.SweeperSquad
        {
            MaxPerPair = 3,
            CashRatio = 4,
            Divisions = Enumerable.Empty<NortheastMegabuck.Database.Entities.SweeperDivision>().ToList()
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
