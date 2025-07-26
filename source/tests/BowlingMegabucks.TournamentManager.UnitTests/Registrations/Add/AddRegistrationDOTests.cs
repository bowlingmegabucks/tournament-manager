namespace BowlingMegabucks.TournamentManager.Tests.Registrations.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Registrations.IEntityMapper> _mapper;
    private Mock<BowlingMegabucks.TournamentManager.Registrations.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Registrations.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<BowlingMegabucks.TournamentManager.Registrations.IEntityMapper>();
        _repository = new Mock<BowlingMegabucks.TournamentManager.Registrations.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Registrations.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_Model_MapperExecute_Model_CalledCorrectly()
    {
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Registration>())).Returns(new BowlingMegabucks.TournamentManager.Database.Entities.Registration());

        var registration = new BowlingMegabucks.TournamentManager.Models.Registration();

        await _dataLayer.ExecuteAsync(registration, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(registration), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_RepositoryExecute_Model_CalledCorrectly()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.Registration();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Registration>())).Returns(entity);

        var model = new BowlingMegabucks.TournamentManager.Models.Registration();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_ReturnsRepositoryAddResponse()
    {
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Registration>())).Returns(new BowlingMegabucks.TournamentManager.Database.Entities.Registration());

        var registrationId = RegistrationId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<BowlingMegabucks.TournamentManager.Database.Entities.Registration>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrationId);

        var model = new BowlingMegabucks.TournamentManager.Models.Registration();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(registrationId));
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_RepositoryAddSquad_CalledCorrectly()
    {
        _repository.Setup(repository => repository.AddSquadAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BowlingMegabucks.TournamentManager.Database.Entities.Registration
            {
                Squads = [],
                Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
                Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division()
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
            .ReturnsAsync(new BowlingMegabucks.TournamentManager.Database.Entities.Registration
            {
                Id = registrationId,
                Squads = [],
                Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
                Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division()
            });

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _dataLayer.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(registrationId));
    }
}
