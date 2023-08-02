
namespace NortheastMegabuck.Tests.Scores;

[TestFixture]
internal sealed class Presenter
{
    private Mock<NortheastMegabuck.Scores.IView> _view;
    private Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter> _retrieveLaneAssignmentsAdapter;
    private Mock<NortheastMegabuck.Scores.Retrieve.IAdapter> _retrieveSquadScoresAdapter;

    private NortheastMegabuck.Scores.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Scores.IView>();
        _retrieveLaneAssignmentsAdapter = new Mock<NortheastMegabuck.LaneAssignments.Retrieve.IAdapter>();
        _retrieveSquadScoresAdapter = new Mock<NortheastMegabuck.Scores.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.Scores.Presenter(_view.Object, _retrieveLaneAssignmentsAdapter.Object, _retrieveSquadScoresAdapter.Object);
    }

    [Test]
    public void Load_RetrieveLaneAssignmentsAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.Load();

        _retrieveLaneAssignmentsAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Load_RetrieveSquadScoresAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.Load();

        _retrieveSquadScoresAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Load_RetrieveLaneAssignmentsAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveLaneAssignmentsAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindSquadScores(It.IsAny<IEnumerable<NortheastMegabuck.Scores.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Load_RetrieveSquadScoresAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveSquadScoresAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindSquadScores(It.IsAny<IEnumerable<NortheastMegabuck.Scores.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Load_RetrieveLaneAssignmentsAdapterAndRetrieveSquadScoresAdapterHaveErrors_LaneAssignmentAdapterErrorDisplayed()
    {
        var error1 = new NortheastMegabuck.Models.ErrorDetail("error1");
        _retrieveLaneAssignmentsAdapter.SetupGet(adapter => adapter.Error).Returns(error1);

        var error2 = new NortheastMegabuck.Models.ErrorDetail("error2");
        _retrieveSquadScoresAdapter.SetupGet(adapter => adapter.Error).Returns(error2);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error1"), Times.Once);
            _view.Verify(view => view.DisplayError("error2"), Times.Never);
        });
    }

    [Test]
    public void Load_RetrieveLaneAssignmentsSuccess_ViewBindLaneAssignments_CalledCorrectly()
    {
        var assignment1 = new NortheastMegabuck.LaneAssignments.ViewModel("15A");
        var assignment2 = new NortheastMegabuck.LaneAssignments.ViewModel(string.Empty);
        var assignment3 = new NortheastMegabuck.LaneAssignments.ViewModel("7B");

        var laneAssignments = new[] { assignment1, assignment2, assignment3 };

        _retrieveLaneAssignmentsAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(laneAssignments);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.ToList()[0].LaneAssignment == "7B")), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.ToList()[1].LaneAssignment == "15A")), Times.Once);
        });
    }

    [Test]
    public void Load_RetrieveSquadScoresAdapterSuccess_ViewBindSquadScores_CalledCorrectly()
    {
        var scores = new List<NortheastMegabuck.Scores.IViewModel>();
        _retrieveSquadScoresAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(scores);

        _presenter.Load();

        _view.Verify(view => view.BindSquadScores(scores), Times.Once);
    }
}
