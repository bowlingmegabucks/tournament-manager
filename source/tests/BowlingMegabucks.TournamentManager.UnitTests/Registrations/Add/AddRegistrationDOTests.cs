namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Registrations.IRepository> _repository;

    private TournamentManager.Registrations.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.Registrations.IRepository>();

        _dataLayer = new TournamentManager.Registrations.Add.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_RepositoryAddSquad_CalledCorrectly()
    {
        _repository.Setup(repository => repository.AddSquadAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Database.Entities.Registration
            {
                Squads = [],
                Payments = [],
                Bowler = new TournamentManager.Database.Entities.Bowler(),
                Division = new TournamentManager.Database.Entities.Division()
            });

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddSquadAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_ReturnsRegistration()
    {
        var registrationId = RegistrationId.New();

        _repository.Setup(repository => repository.AddSquadAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Database.Entities.Registration
            {
                Id = registrationId,
                Squads = [],
                Payments = [],
                Bowler = new TournamentManager.Database.Entities.Bowler(),
                Division = new TournamentManager.Database.Entities.Division()
            });

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _dataLayer.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(registrationId));
    }
}
