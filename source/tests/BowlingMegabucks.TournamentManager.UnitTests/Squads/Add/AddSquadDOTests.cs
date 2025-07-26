namespace BowlingMegabucks.TournamentManager.Tests.Squads.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Squads.IEntityMapper> _mapper;
    private Mock<BowlingMegabucks.TournamentManager.Squads.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Squads.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<BowlingMegabucks.TournamentManager.Squads.IEntityMapper>();
        _repository = new Mock<BowlingMegabucks.TournamentManager.Squads.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Squads.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var squad = new BowlingMegabucks.TournamentManager.Models.Squad();

        await _dataLayer.ExecuteAsync(squad, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(squad), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryExecute_CalledCorrectly()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Squad>())).Returns(entity);

        var model = new BowlingMegabucks.TournamentManager.Models.Squad();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryAddResponse()
    {
        var id = SquadId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<BowlingMegabucks.TournamentManager.Database.Entities.TournamentSquad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var model = new BowlingMegabucks.TournamentManager.Models.Squad();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
