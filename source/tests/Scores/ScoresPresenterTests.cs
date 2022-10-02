
namespace NortheastMegabuck.Tests.Scores;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Scores.IView> _view;
    private Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter> _retrieveLaneAssignmentsAdapter;

    private NortheastMegabuck.Scores.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Scores.IView>();
        _retrieveLaneAssignmentsAdapter = new Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.Scores.Presenter(_view.Object, _retrieveLaneAssignmentsAdapter.Object);
    }

    [Test]
    public void LoadLaneAssignments_RetrieveLaneAssignmentsAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.LoadLaneAssignments();

        _retrieveLaneAssignmentsAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void LoadLaneAssignments_RetrieveLaneAssignmentsAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveLaneAssignmentsAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.LoadLaneAssignments();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void LoadLaneAssignments_RetrieveLaneAssignmentsSuccess_ViewBindLaneAssignments_CalledCorrectly()
    {
        var assignment1 = new NortheastMegabuck.LaneAssignments.ViewModel("15A");
        var assignment2 = new NortheastMegabuck.LaneAssignments.ViewModel(string.Empty);
        var assignment3 = new NortheastMegabuck.LaneAssignments.ViewModel("7B");

        var laneAssignments = new[] { assignment1, assignment2, assignment3 };

        _retrieveLaneAssignmentsAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(laneAssignments);

        _presenter.LoadLaneAssignments();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.ToList()[0].LaneAssignment == "7B")), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.ToList()[1].LaneAssignment == "15A")), Times.Once);
        });
    }
}
