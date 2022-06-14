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
    public void ForTournament_RepositoryForTournament_Called()
    {
        var guid = Guid.NewGuid();

        _dataLayer.ForTournament(guid);

        _repository.Verify(repository => repository.ForTournament(guid), Times.Once);
    }

    [Test]
    public void ForTournament_ReturnsRepositoryForTournamentResponse()
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

        _repository.Setup(repository => repository.ForTournament(It.IsAny<Guid>())).Returns(sweepers);

        var actual = _dataLayer.ForTournament(Guid.NewGuid());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 1), Is.EqualTo(1));
            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 2), Is.EqualTo(1));
            Assert.That(actual.Count(sweeper => sweeper.MaxPerPair == 3), Is.EqualTo(1));
        });
    }
}
