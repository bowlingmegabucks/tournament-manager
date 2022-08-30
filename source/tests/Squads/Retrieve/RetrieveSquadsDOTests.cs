namespace NortheastMegabuck.Tests.Squads.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NortheastMegabuck.Squads.IRepository> _repository;

    private NortheastMegabuck.Squads.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Squads.IRepository>();

        _dataLayer = new NortheastMegabuck.Squads.Retrieve.DataLayer(_repository.Object);
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
        var squad1 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            MaxPerPair = 1
        };

        var squad2 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            MaxPerPair = 2
        };

        var squad3 = new NortheastMegabuck.Database.Entities.TournamentSquad
        {
            MaxPerPair = 3
        };

        var squads = new[] { squad1, squad2, squad3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(squads);

        var actual = _dataLayer.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(squad => squad.MaxPerPair == 1), Is.EqualTo(1));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 2), Is.EqualTo(1));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 3), Is.EqualTo(1));
        });
    }
}
