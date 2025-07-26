
namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Results;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Squads.Results.IView> _view;
    private Mock<TournamentManager.Squads.Results.IAdapter> _adapter;

    private TournamentManager.Squads.Results.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Squads.Results.IView>();
        _adapter = new Mock<TournamentManager.Squads.Results.IAdapter>();

        _presenter = new TournamentManager.Squads.Results.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_AdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<ICollection<TournamentManager.Squads.Results.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasNoError_ViewBindResults_CalledCorrectly()
    {
        var division1Score1 = new TournamentManager.Squads.Results.ViewModel
        {
            DivisionName = "division1",
            BowlerName = "score1"
        };

        var division1Score2 = new TournamentManager.Squads.Results.ViewModel
        {
            DivisionName = "division1",
            BowlerName = "score2"
        };

        var division2Score = new TournamentManager.Squads.Results.ViewModel
        {
            DivisionName = "division2",
            BowlerName = "score",
            Handicap = 1
        };

        var scores = new[] { division1Score2, division2Score, division1Score1 }.GroupBy(score => score.DivisionName);
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(scores);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<ICollection<TournamentManager.Squads.Results.IViewModel>>()), Times.Exactly(2));

            _view.Verify(view => view.BindResults("division1", false, It.Is<ICollection<TournamentManager.Squads.Results.IViewModel>>(score => score.Count(s => s.DivisionName == "division1") == 2)), Times.Once);
            _view.Verify(view => view.BindResults("division2", true, It.Is<ICollection<TournamentManager.Squads.Results.IViewModel>>(score => score.Count(s => s.DivisionName == "division2") == 1)), Times.Once);
        });
    }
}
