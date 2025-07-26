namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Squads.IEntityMapper> _mapper;
    private Mock<TournamentManager.Squads.IRepository> _repository;

    private TournamentManager.Squads.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<TournamentManager.Squads.IEntityMapper>();
        _repository = new Mock<TournamentManager.Squads.IRepository>();

        _dataLayer = new TournamentManager.Squads.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var squad = new TournamentManager.Models.Squad();

        await _dataLayer.ExecuteAsync(squad, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(squad), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryExecute_CalledCorrectly()
    {
        var entity = new TournamentManager.Database.Entities.TournamentSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Squad>())).Returns(entity);

        var model = new TournamentManager.Models.Squad();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryAddResponse()
    {
        var id = SquadId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.TournamentSquad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var model = new TournamentManager.Models.Squad();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
