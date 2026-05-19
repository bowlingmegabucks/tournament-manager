namespace BowlingMegabucks.TournamentManager.UnitTests.Sweepers.Results;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Sweepers.Results.IView> _view;
    private Mock<TournamentManager.Sweepers.Results.IAdapter> _adapter;

    private TournamentManager.Sweepers.Results.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Sweepers.Results.IView>();
        _adapter = new Mock<TournamentManager.Sweepers.Results.IAdapter>();

        _presenter = new TournamentManager.Sweepers.Results.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_AdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_AdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<ICollection<TournamentManager.Sweepers.Results.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_AdapterExecuteNoError_ViewBindResults_CalledCorrectly()
    {
        var results = new List<TournamentManager.Sweepers.Results.IViewModel>();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(results);

        await _presenter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        _view.Verify(view => view.BindResults(results), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_AdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_AdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<ICollection<TournamentManager.Sweepers.Results.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_AdapterExecuteNoError_ViewBindResults_CalledCorrectly()
    {
        var results = new List<TournamentManager.Sweepers.Results.IViewModel>();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(results);

        await _presenter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        _view.Verify(view => view.BindResults(results), Times.Once);
    }
}
