
namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Portal;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Squads.Portal.IView> _view;
    private Mock<TournamentManager.Squads.Retrieve.IAdapter> _adapter;
    private Mock<TournamentManager.Squads.Complete.IAdapter> _completeAdapter;

    private TournamentManager.Squads.Portal.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Squads.Portal.IView>();
        _adapter = new Mock<TournamentManager.Squads.Retrieve.IAdapter>();
        _completeAdapter = new Mock<TournamentManager.Squads.Complete.IAdapter>();

        _presenter = new TournamentManager.Squads.Portal.Presenter(_view.Object, _adapter.Object, _completeAdapter.Object);
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadAdapterExecute_CalledCorrectly()
    {
        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad.Object);

        var squadId = new SquadId();
        _view.SetupGet(view => view.Id).Returns(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadAdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        var squadId = new SquadId();
        _view.SetupGet(view => view.Id).Returns(squadId);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);

            _view.Verify(view => view.SetPortalTitle(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadAdapterHasNoError_ViewSetPortalTitle_CalledCorrectly()
    {
        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        squad.SetupGet(s => s.Date).Returns(new DateTime(2000, 1, 2, 9, 30, 0, DateTimeKind.Unspecified));
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad.Object);

        var squadId = new SquadId();
        _view.SetupGet(view => view.Id).Returns(squadId);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.SetPortalTitle("01/02/2000 09:30AM"), Times.Once);
    }

    [Test]
    public async Task CompleteAsync_ViewConfirm_CalledCorrectly()
    {
        await _presenter.CompleteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.Confirm("Are you sure you want to complete this squad?"), Times.Once);
    }

    [Test]
    public async Task CompleteAsync_ViewConfirmFalse_CancelFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(false);

        await _presenter.CompleteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _completeAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task CompleteAsync_ViewConfirmTrue_CompleteAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var squadId = SquadId.New();
        _view.SetupGet(view => view.Id).Returns(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.CompleteAsync(cancellationToken).ConfigureAwait(false);

        _completeAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task CompleteAsync_ViewConfirmTrue_CompleteAdapterHasError_ErrorFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var error = new TournamentManager.Models.ErrorDetail("error");
        _completeAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.CompleteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task CompleteAsync_ViewConfirmTrue_ComplateAdapterSuccessful_SuccessFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        await _presenter.CompleteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Squad successfully completed"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}
