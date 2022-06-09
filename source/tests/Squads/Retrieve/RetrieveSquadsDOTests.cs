namespace NewEnglandClassic.Tests.Squads.Retrieve;

[TestFixture]
internal class DataLayer
{
    private Mock<NewEnglandClassic.Squads.IRepository> _repository;

    private NewEnglandClassic.Squads.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NewEnglandClassic.Squads.IRepository>();

        _dataLayer = new NewEnglandClassic.Squads.Retrieve.DataLayer(_repository.Object);
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
        var squad1 = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            MaxPerPair = 1
        };

        var squad2 = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            MaxPerPair = 2
        };

        var squad3 = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            MaxPerPair = 3
        };

        var squads = new[] { squad1, squad2, squad3 };

        _repository.Setup(repository => repository.ForTournament(It.IsAny<Guid>())).Returns(squads);

        var actual = _dataLayer.ForTournament(Guid.NewGuid());

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(squad => squad.MaxPerPair == 1), Is.EqualTo(1));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 2), Is.EqualTo(1));
            Assert.That(actual.Count(squad => squad.MaxPerPair == 3), Is.EqualTo(1));
        });
    }
}
