using MockQueryable;
using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Tournaments.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<NortheastMegabuck.Tournaments.IRepository> _repository;

    private NortheastMegabuck.Tournaments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<NortheastMegabuck.Tournaments.IRepository>();

        _dataLayer = new NortheastMegabuck.Tournaments.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryRetrieveAll_Called()
    {
        _repository.Setup(repository => repository.RetrieveAll()).Returns(Enumerable.Empty<NortheastMegabuck.Database.Entities.Tournament>().SetUpDbContext());
        await _dataLayer.ExecuteAsync(default).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAll(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsTournamentModels()
    {
        var id1 = TournamentId.New();
        var id2 = TournamentId.New();
        var id3 = TournamentId.New();

        var tournament1 = new NortheastMegabuck.Database.Entities.Tournament { Id = id1 };
        var tournament2 = new NortheastMegabuck.Database.Entities.Tournament { Id = id2 };
        var tournament3 = new NortheastMegabuck.Database.Entities.Tournament { Id = id3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _repository.Setup(repository => repository.RetrieveAll()).Returns(tournaments.BuildMock());

        var actual = (await _dataLayer.ExecuteAsync(default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual, Has.Count.EqualTo(3));

            Assert.That(actual.Exists(tournament => tournament.Id == id1), "tournament1 not returned");
            Assert.That(actual.Exists(tournament => tournament.Id == id2), "tournament2 not returned");
            Assert.That(actual.Exists(tournament => tournament.Id == id3), "tournament3 not returned");
        });
    }

    [Test]
    public async Task ExecuteAsync_Id_RepositoryExecuteId_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Id_ReturnsTournament()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var actual = await _dataLayer.ExecuteAsync(tournament.Id, default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_RepositoryExecuteDivisionId_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = DivisionId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_ReturnsTournament()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var actual = await _dataLayer.ExecuteAsync(DivisionId.New(), default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RepositoryExecuteSquadId_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = SquadId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ReturnsTournament()
    {
        var tournament = new NortheastMegabuck.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var actual = await _dataLayer.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }
}
