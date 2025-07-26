
namespace BowlingMegabucks.TournamentManager.Tests.Squads.Retrieve;

[TestFixture]
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Squads.Retrieve.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Squads.Retrieve.IAdapter> _getSquadsAdapter;

    private BowlingMegabucks.TournamentManager.Squads.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Squads.Retrieve.IView>();
        _getSquadsAdapter = new Mock<BowlingMegabucks.TournamentManager.Squads.Retrieve.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Squads.Retrieve.Presenter(_view.Object, _getSquadsAdapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_GetSquadsAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _getSquadsAdapter.Verify(a => a.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetSquadsAdapterHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");

        _getSquadsAdapter.SetupGet(getSquadsAdapter => getSquadsAdapter.Error).Returns(error);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.Disable(), Times.Once);
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindSquads(It.IsAny<IEnumerable<BowlingMegabucks.TournamentManager.Squads.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetSquadsAdapterReturnsSquads_ViewBindSquads_CalledCorrectly()
    {
        var squad1 = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        squad1.SetupGet(squad => squad.Date).Returns(DateTime.Now);
        squad1.SetupGet(squad => squad.MaxPerPair).Returns(1);

        var squad2 = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        squad2.SetupGet(squad => squad.Date).Returns(DateTime.Now.AddDays(1));
        squad2.SetupGet(squad => squad.MaxPerPair).Returns(2);

        var squad3 = new Mock<BowlingMegabucks.TournamentManager.Squads.IViewModel>();
        squad3.SetupGet(squad => squad.Date).Returns(DateTime.Now.AddHours(-1));
        squad3.SetupGet(squad => squad.MaxPerPair).Returns(3);

        var squads = new[] { squad1.Object, squad2.Object, squad3.Object };
        _getSquadsAdapter.Setup(getSquadsAdapter => getSquadsAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squads);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Squads.IViewModel>>(collection => collection.ToList()[0].MaxPerPair == 3)), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Squads.IViewModel>>(collection => collection.ToList()[1].MaxPerPair == 1)), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Squads.IViewModel>>(collection => collection.ToList()[2].MaxPerPair == 2)), Times.Once);
        });
    }

    [Test]
    public async Task AddSquadAsync_ViewAddSquad_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        await _presenter.AddSquadAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.AddSquad(tournamentId), Times.Once);
    }

    [Test]
    public async Task AddSquadAsync_ViewAddSquadReturnsNull_ViewRefreshSquads_NotCalled()
    {
        _view.Setup(view => view.AddSquad(It.IsAny<TournamentId>())).Returns((SquadId?)null);

        await _presenter.AddSquadAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.RefreshSquadsAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task AddSquadAsync_ViewAddSquadReturnsId_ViewRefreshSquads_Called()
    {
        _view.Setup(view => view.AddSquad(It.IsAny<TournamentId>())).Returns(SquadId.New());
        CancellationToken cancellationToken = default;

        await _presenter.AddSquadAsync(cancellationToken).ConfigureAwait(false);

        _view.Verify(view => view.RefreshSquadsAsync(cancellationToken), Times.Once);
    }
}
