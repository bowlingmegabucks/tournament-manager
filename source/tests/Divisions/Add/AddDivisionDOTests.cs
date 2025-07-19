namespace BowlingMegabucks.TournamentManager.Tests.Divisions.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<BowlingMegabucks.TournamentManager.Divisions.IEntityMapper> _mapper;
    private Mock<BowlingMegabucks.TournamentManager.Divisions.IRepository> _repository;

    private BowlingMegabucks.TournamentManager.Divisions.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<BowlingMegabucks.TournamentManager.Divisions.IEntityMapper>();
        _repository = new Mock<BowlingMegabucks.TournamentManager.Divisions.IRepository>();

        _dataLayer = new BowlingMegabucks.TournamentManager.Divisions.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var division = new BowlingMegabucks.TournamentManager.Models.Division();

        await _dataLayer.ExecuteAsync(division, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(division), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryAdd_CalledCorrectly()
    {
        var entity = new BowlingMegabucks.TournamentManager.Database.Entities.Division();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>())).Returns(entity);

        var division = new BowlingMegabucks.TournamentManager.Models.Division();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(division, cancellationToken).ConfigureAwait(true);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsNewGUID()
    {
        var divisionId = BowlingMegabucks.TournamentManager.DivisionId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<BowlingMegabucks.TournamentManager.Database.Entities.Division>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisionId);

        var division = new BowlingMegabucks.TournamentManager.Models.Division();

        var result = await _dataLayer.ExecuteAsync(division, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
