namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Registrations.IEntityMapper> _mapper;
    private Mock<TournamentManager.Registrations.IRepository> _repository;

    private TournamentManager.Registrations.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<TournamentManager.Registrations.IEntityMapper>();
        _repository = new Mock<TournamentManager.Registrations.IRepository>();

        _dataLayer = new TournamentManager.Registrations.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_Model_MapperExecute_Model_CalledCorrectly()
    {
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Registration>())).Returns(new TournamentManager.Database.Entities.Registration());

        var registration = new TournamentManager.Models.Registration();

        await _dataLayer.ExecuteAsync(registration, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(registration), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_RepositoryExecute_Model_CalledCorrectly()
    {
        var entity = new TournamentManager.Database.Entities.Registration();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Registration>())).Returns(entity);

        var model = new TournamentManager.Models.Registration();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_ReturnsRepositoryAddResponse()
    {
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Registration>())).Returns(new TournamentManager.Database.Entities.Registration());

        var registrationId = RegistrationId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrationId);

        var model = new TournamentManager.Models.Registration();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(registrationId));
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_RepositoryAddSquad_CalledCorrectly()
    {
        _repository.Setup(repository => repository.AddSquadAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new TournamentManager.Database.Entities.Registration
            {
                Squads = [],
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
                Bowler = new TournamentManager.Database.Entities.Bowler(),
                Division = new TournamentManager.Database.Entities.Division()
            });

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _dataLayer.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(registrationId));
    }
}
