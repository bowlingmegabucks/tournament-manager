using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.LaneAssignments.IView> _view;
    private Mock<NortheastMegabuck.LaneAssignments.ILaneAvailability> _laneAvailability;
    private Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter> _retrieveAdapter;

    private NortheastMegabuck.LaneAssignments.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.LaneAssignments.IView>();
        _laneAvailability = new Mock<NortheastMegabuck.LaneAssignments.ILaneAvailability>();
        _retrieveAdapter = new Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.LaneAssignments.Presenter(_view.Object, _laneAvailability.Object, _retrieveAdapter.Object);
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
}
