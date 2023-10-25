namespace NortheastMegabuck.Tests.Squads.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Squads.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Squads.IRepository> _repository;

    private NortheastMegabuck.Squads.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Squads.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Squads.IRepository>();

        _dataLayer = new NortheastMegabuck.Squads.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var squad = new NortheastMegabuck.Models.Squad();
        
        await _dataLayer.ExecuteAsync(squad, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(squad), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryExecute_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.TournamentSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Squad>())).Returns(entity);

        var model = new NortheastMegabuck.Models.Squad();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryAddResponse()
    {
        var id = SquadId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<NortheastMegabuck.Database.Entities.TournamentSquad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var model = new NortheastMegabuck.Models.Squad();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
