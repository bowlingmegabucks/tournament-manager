
namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Retrieve;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Sweepers.Retrieve.IView> _view;
    private Mock<TournamentManager.Sweepers.Retrieve.IAdapter> _getSweepersAdapter;

    private TournamentManager.Sweepers.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Sweepers.Retrieve.IView>();
        _getSweepersAdapter = new Mock<TournamentManager.Sweepers.Retrieve.IAdapter>();

        _presenter = new TournamentManager.Sweepers.Retrieve.Presenter(_view.Object, _getSweepersAdapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_GetSweepersAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _getSweepersAdapter.Verify(a => a.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_GetSweepersAdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");

        _getSweepersAdapter.SetupGet(getSweepersAdapter => getSweepersAdapter.Error).Returns(error);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.Disable(), Times.Once);
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindSweepers(It.IsAny<IEnumerable<TournamentManager.Sweepers.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_GetSweepersAdapterReturnsSweepers_ViewBindSweepers_CalledCorrectly()
    {
        var sweeper1 = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper1.SetupGet(sweeper => sweeper.Date).Returns(DateTime.Now);
        sweeper1.SetupGet(sweeper => sweeper.MaxPerPair).Returns(1);

        var sweeper2 = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper2.SetupGet(sweeper => sweeper.Date).Returns(DateTime.Now.AddDays(1));
        sweeper2.SetupGet(sweeper => sweeper.MaxPerPair).Returns(2);

        var sweeper3 = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper3.SetupGet(sweeper => sweeper.Date).Returns(DateTime.Now.AddHours(-1));
        sweeper3.SetupGet(sweeper => sweeper.MaxPerPair).Returns(3);

        var sweepers = new[] { sweeper1.Object, sweeper2.Object, sweeper3.Object };
        _getSweepersAdapter.Setup(getSweepersAdapter => getSweepersAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(collection => collection.ToList()[0].MaxPerPair == 3)), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(collection => collection.ToList()[1].MaxPerPair == 1)), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(collection => collection.ToList()[2].MaxPerPair == 2)), Times.Once);
        });
    }

    [Test]
    public async Task AddSweeperAsync_ViewAddSweeper_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        await _presenter.AddSweeperAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.AddSweeper(tournamentId), Times.Once);
    }

    [Test]
    public async Task AddSweeperAsync_ViewAddSweeperReturnsNull_ViewRefreshSweepers_NotCalled()
    {
        _view.Setup(view => view.AddSweeper(It.IsAny<TournamentId>())).Returns((SquadId?)null);

        await _presenter.AddSweeperAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.RefreshSweepersAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task AddSweeperAsync_ViewAddSweeperReturnsId_ViewRefreshSweepers_Called()
    {
        _view.Setup(view => view.AddSweeper(It.IsAny<TournamentId>())).Returns(SquadId.New());

        CancellationToken cancellationToken = default;
        await _presenter.AddSweeperAsync(cancellationToken).ConfigureAwait(false);

        _view.Verify(view => view.RefreshSweepersAsync(cancellationToken), Times.Once);
    }
}
