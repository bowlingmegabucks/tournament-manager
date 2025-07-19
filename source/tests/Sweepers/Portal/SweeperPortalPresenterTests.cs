
namespace BowlingMegabucks.TournamentManager.Tests.Sweepers.Portal;

[TestFixture]
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Sweepers.Portal.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Sweepers.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<BowlingMegabucks.TournamentManager.Sweepers.Complete.IAdapter> _completeAdapter;

    private BowlingMegabucks.TournamentManager.Sweepers.Portal.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Sweepers.Portal.IView>();
        _retrieveAdapter = new Mock<BowlingMegabucks.TournamentManager.Sweepers.Retrieve.IAdapter>();
        _completeAdapter = new Mock<BowlingMegabucks.TournamentManager.Sweepers.Complete.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Sweepers.Portal.Presenter(_view.Object, _retrieveAdapter.Object, _completeAdapter.Object);
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadAdapterExecute_CalledCorrectly()
    {
        var squad = new BowlingMegabucks.TournamentManager.Sweepers.ViewModel();
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad);

        var squadId = SquadId.New();
        _view.SetupGet(view => view.Id).Returns(squadId);

        CancellationToken cancellationToken = default;
        await _presenter.LoadAsync(cancellationToken).ConfigureAwait(false);

        _retrieveAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadAdapterHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);

            _view.Verify(view => view.SetPortalTitle(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.SetStartingLane(It.IsAny<int>()), Times.Never);
            _view.Verify(view => view.SetNumberOfLanes(It.IsAny<int>()), Times.Never);
            _view.Verify(view => view.SetMaxPerPair(It.IsAny<int>()), Times.Never);
            _view.Verify(view => view.SetComplete(It.IsAny<bool>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadAdapterSuccessful_ViewFieldsSetCorrectly([Values] bool complete)
    {
        var squad = new BowlingMegabucks.TournamentManager.Sweepers.ViewModel
        {
            Date = new DateTime(2000, 1, 2, 9, 30, 30, DateTimeKind.Unspecified),
            StartingLane = 1,
            NumberOfLanes = 2,
            MaxPerPair = 3,
            Complete = complete
        };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {

            _view.Verify(view => view.SetPortalTitle("01/02/2000 09:30AM"), Times.Once);
            _view.Verify(view => view.SetStartingLane(1), Times.Once);
            _view.Verify(view => view.SetNumberOfLanes(2), Times.Once);
            _view.Verify(view => view.SetMaxPerPair(3), Times.Once);
            _view.Verify(view => view.SetComplete(complete), Times.Once);
        });
    }

    [Test]
    public async Task CompleteAsync_ViewConfirm_CalledCorrectly()
    {
        await _presenter.CompleteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.Confirm("Are you sure you want to complete this sweeper?"), Times.Once);
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

        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
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
            _view.Verify(view => view.DisplayMessage("Sweeper successfully completed"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}
