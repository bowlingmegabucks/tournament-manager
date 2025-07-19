
namespace BowlingMegabucks.TournamentManager.Tests.Bowlers.Search;

[TestFixture]
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IAdapter> _adapter;

    private BowlingMegabucks.TournamentManager.Bowlers.Search.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IView>();
        _adapter = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Bowlers.Search.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_AdapterExecute_CalledCorrectly()
    {
        var searchCritiera = new BowlingMegabucks.TournamentManager.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.ExecuteAsync(searchCritiera, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasError_ErrorFlow()
    {
        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        var searchCritiera = new BowlingMegabucks.TournamentManager.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.BindResults(It.IsAny<IEnumerable<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasNoError_NoResults_NoResultsFlow()
    {
        var searchCritiera = new BowlingMegabucks.TournamentManager.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("No Results"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<IEnumerable<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterHasNoError_ViewBindResults_CalledSorted()
    {
        var bowler1 = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>();
        bowler1.SetupGet(bowler => bowler.LastName).Returns("Smith");
        bowler1.SetupGet(bowler => bowler.FirstName).Returns("John");
        bowler1.SetupGet(bowler => bowler.Id).Returns(BowlerId.New());

        var bowler2 = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>();
        bowler2.SetupGet(bowler => bowler.LastName).Returns("Doe");
        bowler2.SetupGet(bowler => bowler.FirstName).Returns("John");
        bowler2.SetupGet(bowler => bowler.Id).Returns(BowlerId.New());

        var bowler3 = new Mock<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>();
        bowler3.SetupGet(bowler => bowler.LastName).Returns("Doe");
        bowler3.SetupGet(bowler => bowler.FirstName).Returns("Jane");
        bowler3.SetupGet(bowler => bowler.Id).Returns(BowlerId.New());

        var bowlers = new[] { bowler1.Object, bowler2.Object, bowler3.Object };

        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.BowlerSearchCriteria>(), It.IsAny<CancellationToken>())).ReturnsAsync(bowlers);

        var searchCritiera = new BowlingMegabucks.TournamentManager.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        await _presenter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>>(results => results.Count() == 3)), Times.Once);

            _view.Verify(view => view.BindResults(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>>(results => results.ToList()[0].Id == bowler3.Object.Id)), Times.Once);
            _view.Verify(view => view.BindResults(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>>(results => results.ToList()[1].Id == bowler2.Object.Id)), Times.Once);
            _view.Verify(view => view.BindResults(It.Is<IEnumerable<BowlingMegabucks.TournamentManager.Bowlers.Search.IViewModel>>(results => results.ToList()[2].Id == bowler1.Object.Id)), Times.Once);
        });
    }
}
