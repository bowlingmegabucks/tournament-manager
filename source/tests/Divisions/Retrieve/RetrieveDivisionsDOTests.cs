namespace NortheastMegabuck.Tests.Divisions.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Divisions.IRepository> _repository;

    private NortheastMegabuck.Divisions.Retrieve.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Divisions.IRepository>();

        _dataLayer = new NortheastMegabuck.Divisions.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieve_Called()
    {
        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Division>().BuildMock());
        var id = TournamentId.New();

        await _dataLayer.ExecuteAsync(id, default).ConfigureAwait(false);

        _repository.Verify(repository => repository.Retrieve(id), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryRetrieveResponse()
    {
        var division1 = new NortheastMegabuck.Database.Entities.Division
        {
            Name = "Division 1"
        };

        var division2 = new NortheastMegabuck.Database.Entities.Division
        {
            Name = "Division 2"
        };

        var division3 = new NortheastMegabuck.Database.Entities.Division
        {
            Name = "Division 3"
        };

        var divisions = new[] { division1, division2, division3 };

        _repository.Setup(repository => repository.Retrieve(It.IsAny<TournamentId>())).Returns(divisions.BuildMock());

        var actual = await _dataLayer.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Count(division => division.Name == "Division 1"), Is.EqualTo(1));
            Assert.That(actual.Count(division => division.Name == "Division 2"), Is.EqualTo(1));
            Assert.That(actual.Count(division => division.Name == "Division 3"), Is.EqualTo(1));
        });
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieve_CalledCorrectly()
    {
        var division = new NortheastMegabuck.Database.Entities.Division();
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var id = DivisionId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_ReturnsRepositoryRetrieveResponse()
    {
        var division = new NortheastMegabuck.Database.Entities.Division { Name = "name" };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(division);

        var id = DivisionId.New();

        var actual = await _dataLayer.ExecuteAsync(id, default).ConfigureAwait(false);

        Assert.That(actual.Name, Is.EqualTo(division.Name));
    }
}
