using BowlingMegabucks.TournamentManager.Tests.Extensions;

namespace BowlingMegabucks.TournamentManager.Tests.Bowlers.Update;

[TestFixture]
internal sealed class NamePresenter
{
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.IBowlerNameView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IAdapter> _retrieveBowlerAdapter;
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.IAdapter> _updateBowlerAdapter;

    private BowlingMegabucks.TournamentManager.Bowlers.Update.NamePresenter _namePresenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.IBowlerNameView>();
        _retrieveBowlerAdapter = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IAdapter>();
        _updateBowlerAdapter = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.IAdapter>();

        _namePresenter = new BowlingMegabucks.TournamentManager.Bowlers.Update.NamePresenter(_view.Object, _retrieveBowlerAdapter.Object, _updateBowlerAdapter.Object);
    }

    [Test]
    public async Task LoadAsync_RetrieveBowlerAdapterExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        _view.SetupGet(view => view.Id).Returns(bowlerId);

        CancellationToken cancellationToken = default;

        await _namePresenter.LoadAsync(cancellationToken).ConfigureAwait(false);

        _retrieveBowlerAdapter.Verify(adapter => adapter.ExecuteAsync(bowlerId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task LoadAsync_RetrieveBowlerAdapterErrorNotNull_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _retrieveBowlerAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _namePresenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.Bind(It.IsAny<BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_RetrievebowlerAdapterSuccessful_ViewBind_CalledCorrectly()
    {
        var bowler = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Retrieve.IViewModel>().Object;
        _retrieveBowlerAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler);

        await _namePresenter.LoadAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.Bind(bowler), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValid_Called()
    {
        await _namePresenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidFalse_NothingElseCalled()
    {
        _view.IsValid_False();

        await _namePresenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _updateBowlerAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>(), It.IsAny<CancellationToken>()), Times.Never);
            _view.Verify(view => view.DisplayErrors(It.IsAny<IEnumerable<string>>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.OkToClose(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_UpdateBowlerNameAdapterExecute_CalledCorrectly()
    {
        _view.IsValid_True();

        var bowlerId = BowlerId.New();
        _view.SetupGet(view => view.Id).Returns(bowlerId);

        var bowlerName = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Update.INameViewModel>().Object;
        _view.SetupGet(view => view.BowlerName).Returns(bowlerName);

        CancellationToken cancellationToken = default;

        await _namePresenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _updateBowlerAdapter.Verify(adapter => adapter.ExecuteAsync(bowlerId, bowlerName, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_UpdateBowlerNameAdapterExecuteHasErrors_ErrorFlow()
    {
        _view.IsValid_True();

        var errors = new[] { new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error1"), new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error2") };
        _updateBowlerAdapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        await _namePresenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrors(new[] { "error1", "error2" }), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.OkToClose(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_UpdateBowlerNameAdapterExecuteSuccessful_ViewDisplayMessage_CalledCorrectly()
    {
        _view.IsValid_True();

        var fullName = "fullName";
        _view.SetupGet(view => view.FullName).Returns(fullName);

        await _namePresenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.DisplayMessage("fullName's name updated"), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_UpdateBowlerNameAdapterExecuteSuccessful_ViewOkToClose_Called()
    {
        _view.IsValid_True();

        var fullName = "fullName";
        _view.SetupGet(view => view.FullName).Returns(fullName);

        await _namePresenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.OkToClose(), Times.Once);
    }
}
