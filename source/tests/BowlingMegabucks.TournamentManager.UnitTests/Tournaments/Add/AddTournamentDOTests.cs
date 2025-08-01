namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Tournaments.IEntityMapper> _mapper;
    private Mock<TournamentManager.Tournaments.IRepository> _repository;

    private TournamentManager.Tournaments.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<TournamentManager.Tournaments.IEntityMapper>();
        _repository = new Mock<TournamentManager.Tournaments.IRepository>();

        _dataLayer = new TournamentManager.Tournaments.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var tournament = new TournamentManager.Models.Tournament();

        await _dataLayer.ExecuteAsync(tournament, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(tournament), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryAdd_CalledCorrectly()
    {
        var entity = new TournamentManager.Database.Entities.Tournament();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Tournament>())).Returns(entity);

        var tournament = new TournamentManager.Models.Tournament();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(tournament, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsNewGUID()
    {
        var id = TournamentId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.Tournament>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var tournament = new TournamentManager.Models.Tournament();

        var result = await _dataLayer.ExecuteAsync(tournament, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(id));
    }
}
