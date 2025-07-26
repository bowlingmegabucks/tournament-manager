namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Add;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Squads.Add.IView> _view;
    private Mock<TournamentManager.Tournaments.Retrieve.IAdapter> _retrieveTournamentAdapter;
    private Mock<TournamentManager.Squads.Add.IAdapter> _addSquadAdapter;

    private TournamentManager.Squads.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Squads.Add.IView>();
        _retrieveTournamentAdapter = new Mock<TournamentManager.Tournaments.Retrieve.IAdapter>();
        _addSquadAdapter = new Mock<TournamentManager.Squads.Add.IAdapter>();

        _presenter = new TournamentManager.Squads.Add.Presenter(_view.Object, _retrieveTournamentAdapter.Object, _addSquadAdapter.Object);
    }

    [Test]
    public async Task GetTournamentDetailsAsync_RetrieveTournamentAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        squad.SetupGet(s => s.TournamentId).Returns(tournamentId);

        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var tournament = new Mock<TournamentManager.Tournaments.IViewModel>();
        _retrieveTournamentAdapter.Setup(retrieveTournamentAdapter => retrieveTournamentAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament.Object);

        CancellationToken cancellationToken = default;

        await _presenter.GetTournamentDetailsAsync(cancellationToken).ConfigureAwait(false);

        _retrieveTournamentAdapter.Verify(retrieveTournamentAdapter => retrieveTournamentAdapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task GetTournamentDetailsAsync_RetrieveTournamentAdapterHasError_ErrorFlow()
    {
        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveTournamentAdapter.SetupGet(retrieveTournamentAdapter => retrieveTournamentAdapter.Error).Returns(error);

        await _presenter.GetTournamentDetailsAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);

            _view.Verify(view => view.SetTournamentEntryFee(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.SetTournamentFinalsRatio(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.SetTournamentCashRatio(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public async Task GetTournamentDetailsAsync_RetrieveTournamentAdapterHasNoError_SuccessFlow()
    {
        var tournament = new Mock<TournamentManager.Tournaments.IViewModel>();
        tournament.SetupGet(t => t.FinalsRatio).Returns(1m);
        tournament.SetupGet(t => t.CashRatio).Returns(3m);
        tournament.SetupGet(t => t.EntryFee).Returns(50m);

        _retrieveTournamentAdapter.Setup(retrieveTournamentAdapter => retrieveTournamentAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament.Object);

        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        await _presenter.GetTournamentDetailsAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);

            _view.Verify(view => view.SetTournamentEntryFee("$50.00"), Times.Once);
            _view.Verify(view => view.SetTournamentFinalsRatio("1.0"), Times.Once);
            _view.Verify(view => view.SetTournamentCashRatio("3.0"), Times.Once);
        });
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

        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _addSquadAdapter.Verify(addSquadAdapter => addSquadAdapter.ExecuteAsync(It.IsAny<TournamentManager.Squads.IViewModel>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            squad.VerifySet(s => s.Id = It.IsAny<SquadId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AddSquadAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var squadId = SquadId.New();
        _addSquadAdapter.Setup(addSquadAdapter => addSquadAdapter.ExecuteAsync(It.IsAny<TournamentManager.Squads.IViewModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _addSquadAdapter.Verify(addSquadAdapter => addSquadAdapter.ExecuteAsync(squad.Object, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AddSquadAdapterHasErrors_ErrorFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var errors = new[]
        {
            new TournamentManager.Models.ErrorDetail("error1"),
            new TournamentManager.Models.ErrorDetail("error2"),
            new TournamentManager.Models.ErrorDetail("error3")
        };

        _addSquadAdapter.SetupGet(addSquadAdapter => addSquadAdapter.Errors).Returns(errors);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2{Environment.NewLine}error3"), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            squad.VerifySet(s => s.Id = It.IsAny<SquadId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AddSquadAdapterSuccessful_SuccessPath()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var squad = new Mock<TournamentManager.Squads.IViewModel>();
        squad.SetupGet(s => s.Date).Returns(new DateTime(2000, 1, 2, 9, 30, 00, DateTimeKind.Unspecified));
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var squadId = SquadId.New();
        _addSquadAdapter.Setup(addSquadAdapter => addSquadAdapter.ExecuteAsync(It.IsAny<TournamentManager.Squads.IViewModel>(), It.IsAny<CancellationToken>())).ReturnsAsync(squadId);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Never);
            _view.Verify(view => view.DisplayMessage("Squad added for 01/02/2000 09:30 AM"), Times.Once);
            squad.VerifySet(s => s.Id = squadId, Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}
