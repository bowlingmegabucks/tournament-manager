namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Add;

[TestFixture]
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Add.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Add.IAdapter> _adapter;

    private BowlingMegabucks.TournamentManager.Tournaments.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Add.IView>();
        _adapter = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Add.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Tournaments.Add.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValid_Called()
    {
        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidFalse_InvalidViewFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(false);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _adapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Tournaments.IViewModel>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.DisplayErrors(It.IsAny<IEnumerable<string>>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.VerifySet(view => view.Tournament.Id = It.IsAny<TournamentId>(), Times.Never);
            _view.Verify(view => view.OkToClose(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterExecute_CalledCorrectly()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel();
        _view.SetupGet(view => view.Tournament).Returns(viewModel);

        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Tournaments.IViewModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(TournamentId.Empty);

        _view.Setup(view => view.IsValid()).Returns(true);
        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(viewModel, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterHasErrors_AdapterErrorFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var errors = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.ErrorDetail("message"), 3);
        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayErrors(new[] { "message", "message", "message" }), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.VerifySet(view => view.Tournament.Id = It.IsAny<TournamentId>(), Times.Never);
            _view.Verify(view => view.OkToClose(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterHasNoErrors_SuccessFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var id = TournamentId.New();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Tournaments.IViewModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel
        {
            TournamentName = "name"
        };

        _view.SetupGet(view => view.Tournament).Returns(viewModel);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("name successfully added"), Times.Once);
            Assert.That(viewModel.Id, Is.EqualTo(id));
            _view.Verify(view => view.OkToClose(), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);

            _view.Verify(view => view.KeepOpen(), Times.Never);
            _view.Verify(view => view.DisplayErrors(It.IsAny<IEnumerable<string>>()), Times.Never);
        });
    }
}
