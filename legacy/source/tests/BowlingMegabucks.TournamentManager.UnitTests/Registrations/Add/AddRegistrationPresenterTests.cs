
namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Add;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Registrations.Add.IView> _view;

    private Mock<TournamentManager.Divisions.Retrieve.IAdapter> _divisionsAdapter;
    private Mock<TournamentManager.Squads.Retrieve.IAdapter> _squadsAdapter;
    private Mock<TournamentManager.Sweepers.Retrieve.IAdapter> _sweepersAdapter;
    private Mock<TournamentManager.Bowlers.Retrieve.IAdapter> _bowlersAdapter;
    private Mock<TournamentManager.Registrations.Add.IAdapter> _adapter;

    private TournamentManager.Registrations.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Registrations.Add.IView>();

        _divisionsAdapter = new Mock<TournamentManager.Divisions.Retrieve.IAdapter>();
        _squadsAdapter = new Mock<TournamentManager.Squads.Retrieve.IAdapter>();
        _sweepersAdapter = new Mock<TournamentManager.Sweepers.Retrieve.IAdapter>();
        _bowlersAdapter = new Mock<TournamentManager.Bowlers.Retrieve.IAdapter>();
        _adapter = new Mock<TournamentManager.Registrations.Add.IAdapter>();

        _presenter = new TournamentManager.Registrations.Add.Presenter(_view.Object, _divisionsAdapter.Object, _squadsAdapter.Object, _sweepersAdapter.Object, _bowlersAdapter.Object, _adapter.Object);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_Called()
    {
        await _presenter.LoadAsync(new TournamentId(), default);

        _view.Verify(view => view.SelectBowler(), Times.Once);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectedBowlerReturnsNull_ExitFlow()
    {
        _view.Setup(view => view.SelectBowler()).Returns((BowlerId?)null);

        await _presenter.LoadAsync(new TournamentId(), default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.Close(), Times.Once);

            _divisionsAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Never);
            _squadsAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Never);
            _sweepersAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>()), Times.Never);
            _bowlersAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Disable(), Times.Never);

            _view.Verify(view => view.BindDivisions(It.IsAny<IEnumerable<TournamentManager.Divisions.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindSquads(It.IsAny<IEnumerable<TournamentManager.Squads.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindSweepers(It.IsAny<IEnumerable<TournamentManager.Sweepers.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindBowler(It.IsAny<TournamentManager.Bowlers.Retrieve.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_DivisionsAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(tournamentId, cancellationToken);

        _divisionsAdapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_SquadsAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(tournamentId, cancellationToken);

        _squadsAdapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_SweepersAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(tournamentId, cancellationToken);

        _sweepersAdapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_BowlerAdapterExecute_NotCalled()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        _bowlersAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsId_BowlerAdapterExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler()).Returns(bowlerId);

        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(tournamentId, cancellationToken);

        _bowlersAdapter.Verify(adapter => adapter.ExecuteAsync(bowlerId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_AllAdaptersHaveErrors_DivisionAdapterErrorFlow()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var divisionError = new TournamentManager.Models.ErrorDetail("division");
        _divisionsAdapter.SetupGet(adapter => adapter.Error).Returns(divisionError);

        var squadError = new TournamentManager.Models.ErrorDetail("squad");
        _squadsAdapter.SetupGet(adapter => adapter.Error).Returns(squadError);

        var sweeperError = new TournamentManager.Models.ErrorDetail("sweeper");
        _sweepersAdapter.SetupGet(adapter => adapter.Error).Returns(sweeperError);

        var bowlerError = new TournamentManager.Models.ErrorDetail("bowler");
        _bowlersAdapter.SetupGet(adapter => adapter.Error).Returns(bowlerError);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("division"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_DivisionAdapterNoError_SweeperAndSquadAdapterError_SquadAdapterErrorFlow()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var squadError = new TournamentManager.Models.ErrorDetail("squad");
        _squadsAdapter.SetupGet(adapter => adapter.Error).Returns(squadError);

        var sweeperError = new TournamentManager.Models.ErrorDetail("sweeper");
        _sweepersAdapter.SetupGet(adapter => adapter.Error).Returns(sweeperError);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("squad"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_DivisionAdapterAndSquadAdapterNoError_SweeperAdapterError_SweeperErrorFlow()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var sweeperError = new TournamentManager.Models.ErrorDetail("sweeper");
        _sweepersAdapter.SetupGet(adapter => adapter.Error).Returns(sweeperError);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("sweeper"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_NoAdapterErrors_ViewBindDivisions_CalledSortedByDivisionNumber()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var division1 = new Mock<TournamentManager.Divisions.IViewModel>();
        division1.SetupGet(division => division.Number).Returns(1);

        var division2 = new Mock<TournamentManager.Divisions.IViewModel>();
        division2.SetupGet(division => division.Number).Returns(2);

        var division3 = new Mock<TournamentManager.Divisions.IViewModel>();
        division3.SetupGet(division => division.Number).Returns(3);

        var divisions = new[] { division3.Object, division1.Object, division2.Object };
        _divisionsAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<TournamentManager.Divisions.IViewModel>>(divisions => divisions.ToList()[0].Number == 1)), Times.Once);
            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<TournamentManager.Divisions.IViewModel>>(divisions => divisions.ToList()[1].Number == 2)), Times.Once);
            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<TournamentManager.Divisions.IViewModel>>(divisions => divisions.ToList()[2].Number == 3)), Times.Once);

            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<TournamentManager.Divisions.IViewModel>>(divisions => divisions.ToList().Count == 3)), Times.Once);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_NoAdapterErrors_ViewBindSquads_CalledSortedByDate()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var squad1 = new Mock<TournamentManager.Squads.IViewModel>();
        squad1.SetupGet(squad => squad.SquadDate).Returns(new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Unspecified));

        var squad2 = new Mock<TournamentManager.Squads.IViewModel>();
        squad2.SetupGet(squad => squad.SquadDate).Returns(new DateTime(2015, 1, 2, 0, 0, 0, DateTimeKind.Unspecified));

        var squad2A = new Mock<TournamentManager.Squads.IViewModel>();
        squad2A.SetupGet(squad => squad.Complete).Returns(true);
        squad2A.SetupGet(squad => squad.SquadDate).Returns(new DateTime(2015, 1, 3, 0, 0, 0, DateTimeKind.Unspecified));

        var squad3 = new Mock<TournamentManager.Squads.IViewModel>();
        squad3.SetupGet(squad => squad.SquadDate).Returns(new DateTime(2015, 1, 3, 0, 0, 0, DateTimeKind.Unspecified));

        var squads = new[] { squad3.Object, squad2A.Object, squad1.Object, squad2.Object };
        _squadsAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squads);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<TournamentManager.Squads.IViewModel>>(squads => squads.ToList()[0].SquadDate == new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Unspecified))), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<TournamentManager.Squads.IViewModel>>(squads => squads.ToList()[1].SquadDate == new DateTime(2015, 1, 2, 0, 0, 0, DateTimeKind.Unspecified))), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<TournamentManager.Squads.IViewModel>>(squads => squads.ToList()[2].SquadDate == new DateTime(2015, 1, 3, 0, 0, 0, DateTimeKind.Unspecified))), Times.Once);

            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<TournamentManager.Squads.IViewModel>>(squads => squads.ToList().Count == 3)), Times.Once);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_NoAdapterErrors_ViewBindSweepers_CalledSortedByDate()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        var sweeper1 = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper1.SetupGet(squad => squad.SweeperDate).Returns(new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Unspecified));

        var sweeper2 = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper2.SetupGet(squad => squad.SweeperDate).Returns(new DateTime(2015, 1, 2, 0, 0, 0, DateTimeKind.Unspecified));

        var sweeper2A = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper2A.SetupGet(sweeper => sweeper.Complete).Returns(true);
        sweeper2A.SetupGet(sweeper => sweeper.SweeperDate).Returns(new DateTime(2015, 1, 3, 0, 0, 0, DateTimeKind.Unspecified));

        var sweeper3 = new Mock<TournamentManager.Sweepers.IViewModel>();
        sweeper3.SetupGet(squad => squad.SweeperDate).Returns(new DateTime(2015, 1, 3, 0, 0, 0, DateTimeKind.Unspecified));

        var sweepers = new[] { sweeper3.Object, sweeper1.Object, sweeper2.Object };
        _sweepersAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var tournamentId = TournamentId.New();

        await _presenter.LoadAsync(tournamentId, default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(sweepers => sweepers.ToList()[0].SweeperDate == new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Unspecified))), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(sweepers => sweepers.ToList()[1].SweeperDate == new DateTime(2015, 1, 2, 0, 0, 0, DateTimeKind.Unspecified))), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(sweepers => sweepers.ToList()[2].SweeperDate == new DateTime(2015, 1, 3, 0, 0, 0, DateTimeKind.Unspecified))), Times.Once);

            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<TournamentManager.Sweepers.IViewModel>>(sweepers => sweepers.ToList().Count == 3)), Times.Once);
        });
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsEmptyId_NoAdapterErrors_ViewBindBowler_NotCalled()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.Empty);

        await _presenter.LoadAsync(new TournamentId(), default);

        _view.Verify(view => view.BindBowler(It.IsAny<TournamentManager.Bowlers.Retrieve.IViewModel>()), Times.Never);
    }

    [Test]
    public async Task Load_TournamentId_ViewSelectBowler_ReturnsId_NoAdapterErrors_ViewBindBowler_CalledCorrectly()
    {
        _view.Setup(view => view.SelectBowler()).Returns(BowlerId.New());

        var bowler = new Mock<TournamentManager.Bowlers.Retrieve.IViewModel>();
        _bowlersAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowler.Object);

        await _presenter.LoadAsync(new TournamentId(), default);

        _view.Verify(view => view.BindBowler(bowler.Object), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValid_Called()
    {
        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidFalse_InvalidFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(false);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _adapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<TournamentManager.Bowlers.IViewModel>(), It.IsAny<TournamentId>(), It.IsAny<DivisionId>(), It.IsAny<IEnumerable<SquadId>>(), It.IsAny<IEnumerable<SquadId>>(), It.IsAny<bool>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()), Times.Never);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterExecute_CalledCorrectly([Values] bool superSweeper)
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var bowler = new Mock<TournamentManager.Bowlers.IViewModel>();
        var tournamentId = TournamentId.New();
        var divisionId = DivisionId.New();
        var sweepers = new List<SquadId>();
        var squads = new List<SquadId>();
        var average = 200;

        _view.SetupGet(view => view.Bowler).Returns(bowler.Object);
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);
        _view.SetupGet(view => view.DivisionId).Returns(divisionId);
        _view.SetupGet(view => view.Squads).Returns(squads);
        _view.SetupGet(view => view.Sweepers).Returns(sweepers);
        _view.SetupGet(view => view.Average).Returns(average);
        _view.SetupGet(view => view.SuperSweeper).Returns(superSweeper);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(bowler.Object, tournamentId, divisionId, squads, sweepers, superSweeper, average, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterHasErrors_ErrorFlow()
    {
        var errors = new[] { new TournamentManager.Models.ErrorDetail("error1"), new TournamentManager.Models.ErrorDetail("error2") };
        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        _view.Setup(view => view.IsValid()).Returns(true);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2"), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_ViewIsValidTrue_AdapterSuccessful_SuccessFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Registration added"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}
