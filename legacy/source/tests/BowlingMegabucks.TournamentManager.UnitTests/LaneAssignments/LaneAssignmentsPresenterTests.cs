namespace BowlingMegabucks.TournamentManager.UnitTests.LaneAssignments;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.LaneAssignments.IView> _view;
    private Mock<TournamentManager.LaneAssignments.ILaneAvailability> _laneAvailability;
    private Mock<TournamentManager.LaneAssignments.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<TournamentManager.LaneAssignments.Update.IAdapter> _updateAdapter;
    private Mock<TournamentManager.Registrations.Add.IAdapter> _addRegistrationAdapter;
    private Mock<TournamentManager.LaneAssignments.IGenerateCrossFactory> _generateCrossFactory;
    private Mock<TournamentManager.Registrations.Delete.IAdapter> _deleteAdapter;

    private TournamentManager.LaneAssignments.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.LaneAssignments.IView>();
        _laneAvailability = new Mock<TournamentManager.LaneAssignments.ILaneAvailability>();
        _retrieveAdapter = new Mock<TournamentManager.LaneAssignments.Retrieve.IAdapter>();
        _updateAdapter = new Mock<TournamentManager.LaneAssignments.Update.IAdapter>();
        _addRegistrationAdapter = new Mock<TournamentManager.Registrations.Add.IAdapter>();
        _generateCrossFactory = new Mock<TournamentManager.LaneAssignments.IGenerateCrossFactory>();
        _deleteAdapter = new Mock<TournamentManager.Registrations.Delete.IAdapter>();

        _presenter = new TournamentManager.LaneAssignments.Presenter(_view.Object, _laneAvailability.Object, _retrieveAdapter.Object, _updateAdapter.Object, _addRegistrationAdapter.Object, _generateCrossFactory.Object, _deleteAdapter.Object);
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerate_CalledCorrectly()
    {
        _view.SetupGet(view => view.StartingLane).Returns(1);
        _view.SetupGet(view => view.NumberOfLanes).Returns(10);
        _view.SetupGet(view => view.MaxPerPair).Returns(4);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        _laneAvailability.Verify(laneAvailability => laneAvailability.Generate(1, 10, 4), Times.Once);
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateNoErrors_ViewBuildLanes_CalledCorrectly()
    {
        var lanes = new[] { "1", "2" };
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(lanes);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.BuildLanes(lanes), Times.Once);
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(ex);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("exception"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _retrieveAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>()), Times.Never);
            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        CancellationToken cancellationToken = default;
        await _presenter.LoadAsync(cancellationToken).ConfigureAwait(false);

        _retrieveAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateNoErrors_RetrieveAdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindRegistrations_CalledCorrectly()
    {
        var assignment1 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");
        assignment1.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignment2 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);
        assignment2.SetupGet(assignment => assignment.DivisionName).Returns("division2");

        var assignment3 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");
        assignment3.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(assignments);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 1)), Times.Once);
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindEntriesPerDivision_CalledCorrectly()
    {
        var assignment1 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");
        assignment1.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignment2 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);
        assignment2.SetupGet(assignment => assignment.DivisionName).Returns("division2");

        var assignment3 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");
        assignment3.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(assignments);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindEntriesPerDivision(It.Is<IDictionary<string, int>>(entries => entries.Count == 2)), Times.Once);

            _view.Verify(view => view.BindEntriesPerDivision(It.Is<IDictionary<string, int>>(entries => entries["division1"] == 2)), Times.Once);
            _view.Verify(view => view.BindEntriesPerDivision(It.Is<IDictionary<string, int>>(entries => entries["division2"] == 1)), Times.Once);
        });
    }

    [Test]
    public async Task LoadAsync_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindLaneAssignments_CalledCorrectly()
    {
        var assignment1 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");
        assignment1.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignment2 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);
        assignment2.SetupGet(assignment => assignment.DivisionName).Returns("division2");

        var assignment3 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");
        assignment3.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(assignments);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => !string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public async Task UpdateAsync_UpdateAdapterExecute_CalledCorrectly([Values("", "21A")] string position)
    {
        var squadId = SquadId.New();

        var bowlerId = BowlerId.New();
        var originalPosition = "1A";
        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        registration.SetupGet(r => r.BowlerId).Returns(bowlerId);
        registration.SetupGet(r => r.LaneAssignment).Returns(originalPosition);

        CancellationToken cancellationToken = default;

        await _presenter.UpdateAsync(squadId, registration.Object, position, cancellationToken).ConfigureAwait(false);

        _updateAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, bowlerId, originalPosition, position, cancellationToken), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_UpdateAdapterHasError_ErrorFlow([Values("", "21A")] string position)
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _updateAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        var squadId = SquadId.New();
        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();

        await _presenter.UpdateAsync(squadId, registration.Object, position, default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.RemoveLaneAssignment(It.IsAny<TournamentManager.LaneAssignments.IViewModel>()), Times.Never);
            _view.Verify(view => view.AssignToLane(It.IsAny<TournamentManager.LaneAssignments.IViewModel>(), It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public async Task UpdateAsync_UpdateAdapterHasNoError_PositionEmpty_ViewRemoveLaneAssignment_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        var position = string.Empty;

        await _presenter.UpdateAsync(squadId, registration.Object, position, default).ConfigureAwait(false);

        _view.Verify(view => view.RemoveLaneAssignment(registration.Object), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_UpdateAdapterHasNoError_PositionEmpty_ViewAssignToLane_NotCalled()
    {
        var squadId = SquadId.New();
        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        var position = string.Empty;

        await _presenter.UpdateAsync(squadId, registration.Object, position, default).ConfigureAwait(false);

        _view.Verify(view => view.AssignToLane(It.IsAny<TournamentManager.LaneAssignments.IViewModel>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task UpdateAsync_UpdateAdapterHasNoError_PositionHasValue_ViewAssignToLane_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        var position = "21A";

        await _presenter.UpdateAsync(squadId, registration.Object, position, default).ConfigureAwait(false);

        _view.Verify(view => view.AssignToLane(registration.Object, position), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_UpdateAdapterHasNoError_PositionHasValue_RemoveLaneAssignment_NotCalled()
    {
        var squadId = SquadId.New();
        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        var position = "21A";

        await _presenter.UpdateAsync(squadId, registration.Object, position, default).ConfigureAwait(false);

        _view.Verify(view => view.RemoveLaneAssignment(It.IsAny<TournamentManager.LaneAssignments.IViewModel>()), Times.Never);
    }

    [Test]
    public async Task AddToRegistrationAsync_ViewSelectBowler_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        var squadId = SquadId.New();

        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        await _presenter.AddToRegistrationAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.SelectBowler(tournamentId, squadId), Times.Once);
    }

    [Test]
    public async Task AddToRegistrationAsync_ViewSelectBowlerNull_CancelFlow()
    {
        await _presenter.AddToRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Add to Registration Canceled"));

            _addRegistrationAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()), Times.Never);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.AddToUnassigned(It.IsAny<TournamentManager.LaneAssignments.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public async Task AddToRegistrationAsync_ViewGetBowlerReturnsId_AddRegistrationAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(bowlerId);

        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        registration.SetupGet(r => r.BowlerName).Returns("bowler name");
        _addRegistrationAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registration.Object);

        CancellationToken cancellationToken = default;

        await _presenter.AddToRegistrationAsync(cancellationToken).ConfigureAwait(false);

        _addRegistrationAdapter.Verify(adapter => adapter.ExecuteAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task AddToRegistrationAsync_ViewGetBowlerReturnsId_AddRegistrationAdapterHasErrors_ErrorFlow()
    {
        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(bowlerId);

        var errors = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("error"), 3);
        _addRegistrationAdapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        await _presenter.AddToRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error{Environment.NewLine}error{Environment.NewLine}error"), Times.Once);

            _view.Verify(view => view.AddToUnassigned(It.IsAny<TournamentManager.LaneAssignments.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public async Task AddToRegistrationAsync_ViewGetBowlerReturnsId_AddRegistrationAdapterSuccessful_SuccessFlow()
    {
        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(bowlerId);

        var registration = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        registration.SetupGet(r => r.BowlerName).Returns("bowler name");
        _addRegistrationAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(registration.Object);

        await _presenter.AddToRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("bowler name is ready to be assigned to a lane"), Times.Once);
            _view.Verify(view => view.AddToUnassigned(registration.Object), Times.Once);
        });
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistration_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        var squadId = SquadId.New();

        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.NewRegistration(tournamentId, squadId), Times.Once);
    }

    [Test]
    public async Task NewRegistrationAsync_RegistrationFalse_CancelFlow()
    {
        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("New Registration Canceled"), Times.Once);

            _view.Verify(view => view.AddToUnassigned(It.IsAny<TournamentManager.LaneAssignments.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_ViewClearLanes_Called()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.ClearLanes(), Times.Once);
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_ViewClearUnassigned_Called()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.ClearUnassigned(), Times.Once);
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerate_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        _view.SetupGet(view => view.StartingLane).Returns(1);
        _view.SetupGet(view => view.NumberOfLanes).Returns(10);
        _view.SetupGet(view => view.MaxPerPair).Returns(4);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        _laneAvailability.Verify(laneAvailability => laneAvailability.Generate(1, 10, 4), Times.Once);
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_ViewBuildLanes_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var lanes = new[] { "1", "2" };
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(lanes);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.BuildLanes(lanes), Times.Once);
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateThrowsException_ExceptionFlow()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var ex = new Exception("exception");
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(ex);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("exception"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _retrieveAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>()), Times.Never);
            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.NewRegistrationAsync(cancellationToken).ConfigureAwait(false);

        _retrieveAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterHasError_ErrorFlow()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var error = new TournamentManager.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindRegistrations_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var assignment1 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");
        assignment1.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignment2 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);
        assignment2.SetupGet(assignment => assignment.DivisionName).Returns("division2");

        var assignment3 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");
        assignment3.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(assignments);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 1)), Times.Once);
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindEntriesPerDivision_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var assignment1 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");
        assignment1.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignment2 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);
        assignment2.SetupGet(assignment => assignment.DivisionName).Returns("division2");

        var assignment3 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");
        assignment3.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(assignments);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindEntriesPerDivision(It.Is<IDictionary<string, int>>(entries => entries.Count == 2)), Times.Once);

            _view.Verify(view => view.BindEntriesPerDivision(It.Is<IDictionary<string, int>>(entries => entries["division1"] == 2)), Times.Once);
            _view.Verify(view => view.BindEntriesPerDivision(It.Is<IDictionary<string, int>>(entries => entries["division2"] == 1)), Times.Once);
        });
    }

    [Test]
    public async Task NewRegistrationAsync_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindLaneAssignments_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var assignment1 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");
        assignment1.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignment2 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);
        assignment2.SetupGet(assignment => assignment.DivisionName).Returns("division2");

        var assignment3 = new Mock<TournamentManager.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");
        assignment3.SetupGet(assignment => assignment.DivisionName).Returns("division1");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(assignments);

        await _presenter.NewRegistrationAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<TournamentManager.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => !string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public void GenerateRecaps_GenerateCrossFactoryExecute_CalledCorrectly([Values] bool staggeredSkipSelected)
    {
        var mockCrossGenerator = new Mock<TournamentManager.LaneAssignments.IGenerate>();
        _generateCrossFactory.Setup(factory => factory.Execute(It.IsAny<bool>())).Returns(mockCrossGenerator.Object);

        _view.SetupGet(view => view.StaggeredSkipSelected).Returns(staggeredSkipSelected);

        _presenter.GenerateRecaps([]);

        _generateCrossFactory.Verify(factory => factory.Execute(staggeredSkipSelected), Times.Once);
    }

    [Test]
    public void GenerateRecaps_CrossGeneratorDetermineSkip_CalledCorrectly()
    {
        var mockCrossGenerator = new Mock<TournamentManager.LaneAssignments.IGenerate>();
        _generateCrossFactory.Setup(factory => factory.Execute(It.IsAny<bool>())).Returns(mockCrossGenerator.Object);

        var recap1 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "1A"
        };

        var recap2 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "1B"
        };

        var recap3 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "4C"
        };

        var recap4 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "5A"
        };

        var recap5 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "6C"
        };

        var recaps = new[] { recap1, recap2, recap3, recap4, recap5 };

        _presenter.GenerateRecaps(recaps);

        mockCrossGenerator.Verify(generator => generator.DetermineSkip(6), Times.Once);
    }

    [Test]
    public void GenerateRecaps_CrossGeneratorExecute_CalledCorrectly()
    {
        var mockCrossGenerator = new Mock<TournamentManager.LaneAssignments.IGenerate>();
        mockCrossGenerator.Setup(generator => generator.DetermineSkip(It.IsAny<int>())).Returns(10);
        _generateCrossFactory.Setup(factory => factory.Execute(It.IsAny<bool>())).Returns(mockCrossGenerator.Object);

        _view.SetupGet(view => view.Games).Returns(15);

        var recap1 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "1A"
        };

        var recap2 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "1B"
        };

        var recap3 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "4C"
        };

        var recap4 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "5A"
        };

        var recap5 = new TournamentManager.LaneAssignments.ViewModel
        {
            LaneAssignment = "6C"
        };

        var recaps = new[] { recap1, recap2, recap3, recap4, recap5 };

        _presenter.GenerateRecaps(recaps);

        Assert.Multiple(() =>
        {
            mockCrossGenerator.Verify(generator => generator.Execute(1, "A", 15, new List<short> { 1, 2, 3, 4, 5, 6 }, 10), Times.Once);
            mockCrossGenerator.Verify(generator => generator.Execute(1, "B", 15, new List<short> { 1, 2, 3, 4, 5, 6 }, 10), Times.Once);
            mockCrossGenerator.Verify(generator => generator.Execute(4, "C", 15, new List<short> { 1, 2, 3, 4, 5, 6 }, 10), Times.Once);
            mockCrossGenerator.Verify(generator => generator.Execute(5, "A", 15, new List<short> { 1, 2, 3, 4, 5, 6 }, 10), Times.Once);
            mockCrossGenerator.Verify(generator => generator.Execute(6, "C", 15, new List<short> { 1, 2, 3, 4, 5, 6 }, 10), Times.Once);
        });
    }

    [Test]
    public void GenerateRecaps_ViewGenerateRecaps_CalledCorrectly()
    {
        var mockCrossGenerator = new Mock<TournamentManager.LaneAssignments.IGenerate>();
        mockCrossGenerator.Setup(generator => generator.DetermineSkip(It.IsAny<int>())).Returns(10);
        mockCrossGenerator.Setup(generator => generator.Execute(It.IsAny<short>(), It.IsAny<string>(), It.IsAny<short>(), It.IsAny<IList<short>>(), It.IsAny<short>())).Returns(["a", "b", "c", "d"]);
        _generateCrossFactory.Setup(factory => factory.Execute(It.IsAny<bool>())).Returns(mockCrossGenerator.Object);

        var recap1 = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerName = "bowler1",
            LaneAssignment = "1A"
        };

        var recap2 = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerName = "bowler2",
            LaneAssignment = "1B"
        };

        var recap3 = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerName = "bowler3",
            LaneAssignment = "4C"
        };

        var recap4 = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerName = "bowler4",
            LaneAssignment = "5A"
        };

        var recap5 = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerName = "bowler5",
            LaneAssignment = "6C"
        };

        var recaps = new[] { recap1, recap2, recap3, recap4, recap5 };

        _presenter.GenerateRecaps(recaps);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.Count() == 5)), Times.Once);

            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.Count(recap => recap.BowlerName == "bowler1") == 1)), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.Count(recap => recap.BowlerName == "bowler2") == 1)), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.Count(recap => recap.BowlerName == "bowler3") == 1)), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.Count(recap => recap.BowlerName == "bowler4") == 1)), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.Count(recap => recap.BowlerName == "bowler5") == 1)), Times.Once);

            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.All(recap => recap.Cross[1] == "a"))), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.All(recap => recap.Cross[2] == "b"))), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.All(recap => recap.Cross[3] == "c"))), Times.Once);
            _view.Verify(view => view.GenerateRecaps(It.Is<IEnumerable<TournamentManager.Scores.IRecapSheetViewModel>>(recaps => recaps.All(recap => recap.Cross[4] == "d"))), Times.Once);
        });
    }

    [Test]
    public async Task DeleteAsync_ViewConfirm_CalledCorrectly()
    {
        await _presenter.DeleteAsync(BowlerId.New(), default).ConfigureAwait(false);

        _view.Verify(view => view.Confirm("Are you sure you want remove bowler from this squad (Refund may be required)?"), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmFalse_NothingElseCalled()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(false);

        await _presenter.DeleteAsync(BowlerId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _deleteAdapter.Verify(adapter => adapter.ExecuteAsync(It.IsAny<BowlerId>(), It.IsAny<SquadId>(), It.IsAny<CancellationToken>()), Times.Never);

            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DeleteRegistration(It.IsAny<BowlerId>()), Times.Never);
        });
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmTrue_DeleteAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var bowlerId = BowlerId.New();
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.DeleteAsync(bowlerId, cancellationToken).ConfigureAwait(false);

        _deleteAdapter.Verify(adapter => adapter.ExecuteAsync(bowlerId, squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmTrue_DeleteAdapterHasError_ErrorFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var error = new TournamentManager.Models.ErrorDetail("error");
        _deleteAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.DeleteAsync(BowlerId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.DeleteRegistration(It.IsAny<BowlerId>()), Times.Never);
        });
    }

    [Test]
    public async Task DeleteAsync_ViewConfirmTrue_DeleteAdapterSuccessful_ViewDeleteRegistration_CalledCorrectly()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var bowlerId = BowlerId.New();

        await _presenter.DeleteAsync(bowlerId, default).ConfigureAwait(false);

        _view.Verify(view => view.DeleteRegistration(bowlerId), Times.Once);
    }
}