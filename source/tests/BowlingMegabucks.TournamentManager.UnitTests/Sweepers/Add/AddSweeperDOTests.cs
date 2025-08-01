﻿using BowlingMegabucks.TournamentManager.Sweepers;

namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Add;

[TestFixture]
internal sealed class DataLayer
{
    private Mock<IEntityMapper> _mapper;
    private Mock<IRepository> _repository;

    private TournamentManager.Sweepers.Add.DataLayer _dataLayer;

    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<IEntityMapper>();
        _repository = new Mock<IRepository>();

        _dataLayer = new TournamentManager.Sweepers.Add.DataLayer(_mapper.Object, _repository.Object);
    }

    [Test]
    public async Task ExecuteAsync_MapperExecute_CalledCorrectly()
    {
        var sweeper = new TournamentManager.Models.Sweeper();
        await _dataLayer.ExecuteAsync(sweeper, default).ConfigureAwait(false);

        _mapper.Verify(mapper => mapper.Execute(sweeper), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_RepositoryExecute_CalledCorrectly()
    {
        var entity = new TournamentManager.Database.Entities.SweeperSquad();
        _mapper.Setup(mapper => mapper.Execute(It.IsAny<TournamentManager.Models.Sweeper>())).Returns(entity);

        var model = new TournamentManager.Models.Sweeper();
        CancellationToken cancellationToken = default;

        await _dataLayer.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        _repository.Verify(repository => repository.AddAsync(entity, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ReturnsRepositoryAddResponse()
    {
        var id = SquadId.New();
        _repository.Setup(repository => repository.AddAsync(It.IsAny<TournamentManager.Database.Entities.SweeperSquad>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var model = new TournamentManager.Models.Sweeper();
        var actual = await _dataLayer.ExecuteAsync(model, default).ConfigureAwait(false);

        Assert.That(actual, Is.EqualTo(id));
    }
}
