namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.LaneAssignments.IView> _view;
    private Mock<NortheastMegabuck.LaneAssignments.ILaneAvailability> _laneAvailability;
    private Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<NortheastMegabuck.LaneAssignments.Update.IAdapter> _updateAdapter;
    private Mock<NortheastMegabuck.Registrations.Add.IAdapter> _addRegistrationAdapter;

    private NortheastMegabuck.LaneAssignments.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.LaneAssignments.IView>();
        _laneAvailability = new Mock<NortheastMegabuck.LaneAssignments.ILaneAvailability>();
        _retrieveAdapter = new Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter>();
        _updateAdapter = new Mock<NortheastMegabuck.LaneAssignments.Update.IAdapter>();
        _addRegistrationAdapter = new Mock<NortheastMegabuck.Registrations.Add.IAdapter>();

        _presenter = new NortheastMegabuck.LaneAssignments.Presenter(_view.Object, _laneAvailability.Object, _retrieveAdapter.Object, _updateAdapter.Object, _addRegistrationAdapter.Object);
    }

    [Test]
    public void Load_LaneAvailabilityGenerate_CalledCorrectly()
    {
        _view.SetupGet(view => view.StartingLane).Returns(1);
        _view.SetupGet(view => view.NumberOfLanes).Returns(10);
        _view.SetupGet(view => view.MaxPerPair).Returns(4);

        _presenter.Load();

        _laneAvailability.Verify(laneAvailability => laneAvailability.Generate(1, 10, 4), Times.Once);
    }

    [Test]
    public void Load_LaneAvailabilityGenerateNoErrors_ViewBuildLanes_CalledCorrectly()
    {
        var lanes = new[] { "1", "2" };
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(lanes);

        _presenter.Load();

        _view.Verify(view => view.BuildLanes(lanes), Times.Once);
    }

    [Test]
    public void Load_LaneAvailabilityGenerateThrowsException_ExceptionFlow()
    {
        var ex = new Exception("exception");
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(ex);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("exception"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _retrieveAdapter.Verify(adapter => adapter.Execute(It.IsAny<SquadId>()), Times.Never);
            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Load_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.Load();

        _retrieveAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Load_LaneAvailabilityGenerateNoErrors_RetrieveAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Load_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindRegistrations_CalledCorrectly()
    {
        var assignment1 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");

        var assignment2 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);

        var assignment3 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(assignments);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 1)), Times.Once);
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public void Load_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindLaneAssignmments_CalledCorrectly()
    {
        var assignment1 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");

        var assignment2 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);

        var assignment3 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(assignments);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => !string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public void Update_UpdateAdapterExecute_CalledCorrectly([Values("","21A")]string position)
    {
        var squadId = SquadId.New();
        
        var bowlerId = BowlerId.New();
        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        registration.SetupGet(r => r.BowlerId).Returns(bowlerId);

        _presenter.Update(squadId, registration.Object, position);

        _updateAdapter.Verify(adapter => adapter.Execute(squadId, bowlerId, position), Times.Once);
    }

    [Test]
    public void Update_UpdateAdapterHasError_ErrorFlow([Values("", "21A")] string position)
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _updateAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        var squadId = SquadId.New();
        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();

        _presenter.Update(squadId, registration.Object, position);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.RemoveLaneAssignment(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>()), Times.Never);
            _view.Verify(view => view.AssignToLane(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>(), It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void Update_UpdateAdapterHasNoError_PositionEmpty_ViewRemoveLaneAssignment_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        var position = string.Empty;

        _presenter.Update(squadId, registration.Object, position);

        _view.Verify(view => view.RemoveLaneAssignment(registration.Object), Times.Once);
    }

    [Test]
    public void Update_UpdateAdapterHasNoError_PositionEmpty_ViewAssignToLane_NotCalled()
    {
        var squadId = SquadId.New();
        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        var position = string.Empty;

        _presenter.Update(squadId, registration.Object, position);

        _view.Verify(view => view.AssignToLane(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void Update_UpdateAdapterHasNoError_PositionHasValue_ViewAssignToLane_CalledCorrectly()
    {
        var squadId = SquadId.New();
        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        var position = "21A";

        _presenter.Update(squadId, registration.Object, position);

        _view.Verify(view => view.AssignToLane(registration.Object, position), Times.Once);
    }

    [Test]
    public void Update_UpdateAdapterHasNoError_PositionHasValue_RemoveLaneAssignment_NotCalled()
    {
        var squadId = SquadId.New();
        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        var position = "21A";

        _presenter.Update(squadId, registration.Object, position);

        _view.Verify(view => view.RemoveLaneAssignment(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>()), Times.Never);
    }

    [Test]
    public void AddToRegistration_ViewSelectBowler_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        var squadId = SquadId.New();

        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.AddToRegistration();

        _view.Verify(view => view.SelectBowler(tournamentId, squadId), Times.Once);
    }

    [Test]
    public void AddToRegistration_ViewSelectBowlerNull_CancelFlow()
    {
        _presenter.AddToRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Add to Registration Canceled"));

            _addRegistrationAdapter.Verify(adapter => adapter.Execute(It.IsAny<BowlerId>(), It.IsAny<SquadId>()), Times.Never);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.AddToUnassigned(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public void AddToRegistration_ViewGetBowlerReturnsId_AddRegistrationAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(bowlerId);

        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        registration.SetupGet(r => r.BowlerName).Returns("bowler name");
        _addRegistrationAdapter.Setup(adapter => adapter.Execute(It.IsAny<BowlerId>(), It.IsAny<SquadId>())).Returns(registration.Object);

        _presenter.AddToRegistration();

        _addRegistrationAdapter.Verify(adapter => adapter.Execute(bowlerId, squadId), Times.Once);
    }

    [Test]
    public void AddToRegistration_ViewGetBowlerReturnsId_AddRegistrationAdapterHasErrors_ErrorFlow()
    {
        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(bowlerId);

        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), 3);
        _addRegistrationAdapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        _presenter.AddToRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error{Environment.NewLine}error{Environment.NewLine}error"), Times.Once);

            _view.Verify(view => view.AddToUnassigned(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public void AddToRegistration_ViewGetBowlerReturnsId_AddRegistrationAdapterSuccessful_Successflow()
    {
        var bowlerId = BowlerId.New();
        _view.Setup(view => view.SelectBowler(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(bowlerId);

        var registration = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        registration.SetupGet(r => r.BowlerName).Returns("bowler name");
        _addRegistrationAdapter.Setup(adapter => adapter.Execute(It.IsAny<BowlerId>(), It.IsAny<SquadId>())).Returns(registration.Object);

        _presenter.AddToRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("bowler name is ready to be assigned to a lane"), Times.Once);
            _view.Verify(view => view.AddToUnassigned(registration.Object), Times.Once);
        });
    }

    [Test]
    public void NewRegistration_ViewNewRegistration_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        var squadId = SquadId.New();

        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.NewRegistration();

        _view.Verify(view => view.NewRegistration(tournamentId, squadId), Times.Once);
    }

    [Test]
    public void NewRegistration_RegistrationFalse_CancelFlow()
    {
        _presenter.NewRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("New Registration Canceled"), Times.Once);

            _view.Verify(view => view.AddToUnassigned(It.IsAny<NortheastMegabuck.LaneAssignments.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_ViewClearLanes_Called()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        _presenter.NewRegistration();

        _view.Verify(view => view.ClearLanes(), Times.Once);
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_ViewClearUnassigned_Called()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        _presenter.NewRegistration();

        _view.Verify(view => view.ClearUnassigned(), Times.Once);
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerate_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        _view.SetupGet(view => view.StartingLane).Returns(1);
        _view.SetupGet(view => view.NumberOfLanes).Returns(10);
        _view.SetupGet(view => view.MaxPerPair).Returns(4);

        _presenter.NewRegistration();

        _laneAvailability.Verify(laneAvailability => laneAvailability.Generate(1, 10, 4), Times.Once);
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_ViewBuildLanes_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var lanes = new[] { "1", "2" };
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(lanes);

        _presenter.NewRegistration();

        _view.Verify(view => view.BuildLanes(lanes), Times.Once);
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateThrowsException_ExceptionFlow()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var ex = new Exception("exception");
        _laneAvailability.Setup(laneAvailability => laneAvailability.Generate(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Throws(ex);

        _presenter.NewRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("exception"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _retrieveAdapter.Verify(adapter => adapter.Execute(It.IsAny<SquadId>()), Times.Never);
            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.NewRegistration();

        _retrieveAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterHasError_ErrorFlow()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.NewRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindRegistrations(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindRegistrations_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var assignment1 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");

        var assignment2 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);

        var assignment3 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(assignments);

        _presenter.NewRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 1)), Times.Once);
            _view.Verify(view => view.BindRegistrations(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }

    [Test]
    public void NewRegistration_ViewNewRegistrationReturnsTrue_LaneAvailabilityGenerateNoErrors_RetrieveAdapterExecuteNoErrors_ViewBindLaneAssignmments_CalledCorrectly()
    {
        _view.Setup(view => view.NewRegistration(It.IsAny<TournamentId>(), It.IsAny<SquadId>())).Returns(true);

        var assignment1 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment1.SetupGet(assignment => assignment.LaneAssignment).Returns("1");

        var assignment2 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment2.SetupGet(assignment => assignment.LaneAssignment).Returns(string.Empty);

        var assignment3 = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();
        assignment3.SetupGet(assignment => assignment.LaneAssignment).Returns("2");

        var assignments = new[] { assignment1.Object, assignment2.Object, assignment3.Object };
        _retrieveAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(assignments);

        _presenter.NewRegistration();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(registrations => registrations.All(registration => !string.IsNullOrWhiteSpace(registration.LaneAssignment)))), Times.Once);
        });
    }
}
