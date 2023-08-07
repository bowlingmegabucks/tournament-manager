namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Divisions.IEntityMapper> _mapper;
    private Mock<NortheastMegabuck.Divisions.IRepository> _repository;

    private NortheastMegabuck.Divisions.Add.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<NortheastMegabuck.Divisions.IEntityMapper>();
        _repository = new Mock<NortheastMegabuck.Divisions.IRepository>();

        _dataLayer = new NortheastMegabuck.Divisions.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var division = new NortheastMegabuck.Models.Division();

        await _dataLayer.ExecuteAsync(division, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(division), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryAdd_CalledCorrectly()
    {
        var entity = new NortheastMegabuck.Database.Entities.Division();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<NortheastMegabuck.Models.Division>())).Returns(entity);

        var division = new NortheastMegabuck.Models.Division();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(division, cancellationToken).ConfigureAwait(true);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsNewGUID()
    {
        var divisionId = NortheastMegabuck.DivisionId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<NortheastMegabuck.Database.Entities.Division>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisionId);

        var division = new NortheastMegabuck.Models.Division();

        var result = await _dataLayer.ExecuteAsync(division, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
