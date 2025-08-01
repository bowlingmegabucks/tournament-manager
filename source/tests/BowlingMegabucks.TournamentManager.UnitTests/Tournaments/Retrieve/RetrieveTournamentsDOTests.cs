﻿using MockQueryable;
using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Retrieve;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<TournamentManager.Tournaments.IRepository> _repository;

    private TournamentManager.Tournaments.Retrieve.IDataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<TournamentManager.Tournaments.IRepository>();

        _dataLayer = new TournamentManager.Tournaments.Retrieve.DataLayer(_repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_RepositoryExecuteDivisionId_CalledCorrectly()
    {
        var tournament = new TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = DivisionId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_DivisionId_ReturnsTournament()
    {
        var tournament = new TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<DivisionId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var actual = await _dataLayer.ExecuteAsync(DivisionId.New(), default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public async Task ExecuteAsync_SquadId_RepositoryExecuteSquadId_CalledCorrectly()
    {
        var tournament = new TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var id = SquadId.New();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.RetrieveAsync(id, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ReturnsTournament()
    {
        var tournament = new TournamentManager.Database.Entities.Tournament { Id = TournamentId.New() };
        _repository.Setup(repository => repository.RetrieveAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var actual = await _dataLayer.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(actual.Id, Is.EqualTo(tournament.Id));
    }
}
