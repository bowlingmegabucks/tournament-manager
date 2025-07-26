namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Retrieve;

[TestFixture]
internal sealed class Presenters
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IAdapter> _adapter;

    private BowlingMegabucks.TournamentManager.Tournaments.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IView>();
        _adapter = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Tournaments.Retrieve.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_AdapterExecute_Called()
    {
        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorNotNull_ErrorFlow()
    {
        var errorDetail = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("message");
        _adapter.SetupGet(adapter => adapter.Error).Returns(errorDetail);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrorMessage("message"), Times.Once);
            _view.Verify(view => view.DisableOpenTournament(), Times.Once);

            _view.Verify(view => view.BindTournaments(It.IsAny<ICollection<BowlingMegabucks.TournamentManager.Tournaments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorDetailNull_NoTournamentsReturned_ViewDisableOpenTournamentCalled()
    {
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync([]);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.DisableOpenTournament(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorDetailNull_TournamentsReturned_ViewBindTournamentsCalled()
    {
        var tournaments = Enumerable.Repeat(new Mock<BowlingMegabucks.TournamentManager.Tournaments.IViewModel>().Object, 3).ToList();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tournaments);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.BindTournaments(tournaments), Times.Once);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournament_Called()
    {
        _presenter.NewTournament();

        _view.Verify(view => view.CreateNewTournament(), Times.Once);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournamentReturnsNull_ViewOpenTournamentNotCalled()
    {
        _view.Setup(view => view.CreateNewTournament()).Returns((null, string.Empty, 0));

        _presenter.NewTournament();

        _view.Verify(view => view.OpenTournament(It.IsAny<TournamentId>(), It.IsAny<string>(), It.IsAny<short>()), Times.Never);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournamentReturnsId_ViewOpenTournamentCalledCorrectly()
    {
        var id = TournamentId.New();

        _view.Setup(view => view.CreateNewTournament()).Returns((id, "tournamentName", 5));

        _presenter.NewTournament();

        _view.Verify(view => view.OpenTournament(id, "tournamentName", 5), Times.Once);
    }
}
