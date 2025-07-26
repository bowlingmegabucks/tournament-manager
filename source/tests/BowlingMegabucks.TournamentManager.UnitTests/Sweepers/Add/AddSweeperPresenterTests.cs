
namespace BowlingMegabucks.TournamentManager.Tests.Sweepers.Add;

[TestFixture]
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Sweepers.Add.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IAdapter> _retrieveDivisionsAdapter;
    private Mock<BowlingMegabucks.TournamentManager.Sweepers.Add.IAdapter> _adapter;

    private BowlingMegabucks.TournamentManager.Sweepers.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Sweepers.Add.IView>();
        _retrieveDivisionsAdapter = new Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IAdapter>();
        _adapter = new Mock<BowlingMegabucks.TournamentManager.Sweepers.Add.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Sweepers.Add.Presenter(_view.Object, _retrieveDivisionsAdapter.Object, _adapter.Object);
    }

    [Test]
    public async Task GetDivisionsAsync_RetrieveDivisionsAdapter_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.GetDivisionsAsync(cancellationToken).ConfigureAwait(false);

        _retrieveDivisionsAdapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task GetDivisionsAsync_RetrieveDivisionsAdapterHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _retrieveDivisionsAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        await _presenter.GetDivisionsAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindDivisions(It.IsAny<IEnumerable<BowlingMegabucks.TournamentManager.Divisions.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task GetDivisionsAsync_RetrieveDivisionsAdapterHasNoError_ViewBindDivisions_CalledCorrectly()
    {
        var divisions = new Mock<IEnumerable<BowlingMegabucks.TournamentManager.Divisions.IViewModel>>();
        _retrieveDivisionsAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions.Object);

        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        await _presenter.GetDivisionsAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.BindDivisions(divisions.Object), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValid_Called()
    {
        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidFalse_NothingElseCalled()
    {
        _view.Setup(view => view.IsValid()).Returns(false);

        var sweeper = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _adapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            sweeper.VerifySet(s => s.Id = It.IsAny<SquadId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var sweeper = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        var sweeperId = SquadId.New();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeperId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(sweeper.Object, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterHasErrors_ErrorFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var sweeper = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        var errors = new[]
        {
            new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error1"),
            new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error2"),
            new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error3")
        };

        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2{Environment.NewLine}error3"), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            sweeper.VerifySet(s => s.Id = It.IsAny<SquadId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterSuccessful_SuccessPath()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var sweeper = new Mock<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>();
        sweeper.SetupGet(s => s.Date).Returns(new DateTime(2000, 1, 2, 9, 30, 00, DateTimeKind.Unspecified));
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        var sweeperId = SquadId.New();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Sweepers.IViewModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweeperId);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Never);
            _view.Verify(view => view.DisplayMessage("Sweeper added for 01/02/2000 09:30 AM"), Times.Once);
            sweeper.VerifySet(s => s.Id = sweeperId, Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}