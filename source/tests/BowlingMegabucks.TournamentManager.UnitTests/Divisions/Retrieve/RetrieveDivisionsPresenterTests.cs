namespace BowlingMegabucks.TournamentManager.Tests.Divisions.Retrieve;
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IAdapter> _adapter;

    private BowlingMegabucks.TournamentManager.Divisions.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IView>();
        _adapter = new Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Divisions.Retrieve.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_AdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorNotNull_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once());

            _view.Verify(view => view.BindDivisions(It.IsAny<IEnumerable<BowlingMegabucks.TournamentManager.Divisions.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorNull_ViewBindDivisions_CalledCorrectly()
    {
        var divisions = new List<BowlingMegabucks.TournamentManager.Divisions.IViewModel>();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.BindDivisions(divisions), Times.Once);
    }

    [Test]
    public async Task AddDivisionAsync_ViewAddDivision_Called()
    {
        var id = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(id);

        await _presenter.AddDivisionAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.AddDivision(id), Times.Once);
    }

    [Test]
    public async Task AddDivisionAsync_ViewAddDivisionReturnsId_ViewRefreshDivisions_Called()
    {
        var id = BowlingMegabucks.TournamentManager.DivisionId.New();
        _view.Setup(view => view.AddDivision(It.IsAny<TournamentId>())).Returns(id);

        CancellationToken cancellationToken = default;

        await _presenter.AddDivisionAsync(cancellationToken).ConfigureAwait(false);

        _view.Verify(view => view.RefreshDivisionsAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task AddDivisionAsync_ViewAddDivisionReturnsNull_ViewRefreshDivisions_NotCalled()
    {
        _view.Setup(view => view.AddDivision(It.IsAny<TournamentId>())).Returns((DivisionId?)null);

        CancellationToken cancellationToken = default;

        await _presenter.AddDivisionAsync(cancellationToken).ConfigureAwait(false);

        _view.Verify(view => view.RefreshDivisionsAsync(cancellationToken), Times.Never);
    }
}
