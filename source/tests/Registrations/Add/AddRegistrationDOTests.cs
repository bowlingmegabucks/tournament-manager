namespace NortheastMegabuck.Tests.Registrations.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Registrations.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Registrations.IRepository> _repository;

    private NortheastMegabuck.Registrations.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Registrations.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Registrations.IRepository>();

        _dataLayer = new NortheastMegabuck.Registrations.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_Model_MapperExecute_Model_CalledCorrectly()
    {
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(new NortheastMegabuck.Database.Entities.Registration());

        var registration = new NortheastMegabuck.Models.Registration();

        await _dataLayer.ExecuteAsync(registration, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(registration), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_RepositoryExecute_Model_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.Registration();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(entity);

        var model = new NortheastMegabuck.Models.Registration();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Model_ReturnsRepositoryAddResponse()
    {
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Registration>())).Returns(new NortheastMegabuck.Database.Entities.Registration());

        var registrationId = RegistrationId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<NortheastMegabuck.Database.Entities.Registration>(), It.IsAny<CancellationToken>())).ReturnsAsync(registrationId);

        var model = new NortheastMegabuck.Models.Registration();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(registrationId));
    }

    [Test]
    public async Task ExecuteAsync_BowlerIdSquadId_RepositoryAddSquad_CalledCorrectly()
    {
        _repository.Setup(repository => repository.AddSquadAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new NortheastMegabuck.Database.Entities.Registration
            {
                Squads = new List<NortheastMegabuck.Database.Entities.SquadRegistration>(),
                Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
                Division = new NortheastMegabuck.Database.Entities.Division()
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
            .ReturnsAsync(new NortheastMegabuck.Database.Entities.Registration
            {
                Id = registrationId,
                Squads = new List<NortheastMegabuck.Database.Entities.SquadRegistration>(),
                Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
                Division = new NortheastMegabuck.Database.Entities.Division()
            });

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();

        var actual = await _dataLayer.ExecuteAsync(bowlerId, squadId, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(registrationId));
    }
}
