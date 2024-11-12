
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
    public async Task LoadAsync_RetrieveLaneAssignmentsAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(cancellationToken).ConfigureAwait(false);

        _retrieveLaneAssignmentsAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadScoresAdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);
        CancellationToken cancellationToken = default;

        await _presenter.LoadAsync(cancellationToken).ConfigureAwait(false);

        _retrieveSquadScoresAdapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task LoadAsync_RetrieveLaneAssignmentsAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveLaneAssignmentsAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindSquadScores(It.IsAny<IEnumerable<NortheastMegabuck.Scores.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadScoresAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveSquadScoresAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindLaneAssignments(It.IsAny<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>()), Times.Never);
            _view.Verify(view => view.BindSquadScores(It.IsAny<IEnumerable<NortheastMegabuck.Scores.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_RetrieveLaneAssignmentsAdapterAndRetrieveSquadScoresAdapterHaveErrors_LaneAssignmentAdapterErrorDisplayed()
    {
        var error1 = new NortheastMegabuck.Models.ErrorDetail("error1");
        _retrieveLaneAssignmentsAdapter.SetupGet(adapter => adapter.Error).Returns(error1);

        var error2 = new NortheastMegabuck.Models.ErrorDetail("error2");
        _retrieveSquadScoresAdapter.SetupGet(adapter => adapter.Error).Returns(error2);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error1"), Times.Once);
            _view.Verify(view => view.DisplayError("error2"), Times.Never);
        });
    }

    [Test]
    public async Task LoadAsync_RetrieveLaneAssignmentsSuccess_ViewBindLaneAssignments_CalledCorrectly()
    {
        var assignment1 = new NortheastMegabuck.LaneAssignments.ViewModel("15A");
        var assignment2 = new NortheastMegabuck.LaneAssignments.ViewModel(string.Empty);
        var assignment3 = new NortheastMegabuck.LaneAssignments.ViewModel("7B");

        var laneAssignments = new[] { assignment1, assignment2, assignment3 };

        _retrieveLaneAssignmentsAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(laneAssignments);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.Count() == 2)), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.ToList()[0].LaneAssignment == "7B")), Times.Once);
            _view.Verify(view => view.BindLaneAssignments(It.Is<IEnumerable<NortheastMegabuck.LaneAssignments.IViewModel>>(assignments => assignments.ToList()[1].LaneAssignment == "15A")), Times.Once);
        });
    }

    [Test]
    public async Task LoadAsync_RetrieveSquadScoresAdapterSuccess_ViewBindSquadScores_CalledCorrectly()
    {
        var scores = new List<NortheastMegabuck.Scores.IViewModel>();
        _retrieveSquadScoresAdapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(scores);

        await _presenter.LoadAsync(default).ConfigureAwait(false);

        _view.Verify(view => view.BindSquadScores(scores), Times.Once);
    }
}
