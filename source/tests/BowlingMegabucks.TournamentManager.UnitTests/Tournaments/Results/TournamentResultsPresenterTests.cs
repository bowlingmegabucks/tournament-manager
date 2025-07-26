
namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Results;

[TestFixture]
internal sealed class Presenter
{
    private Mock<TournamentManager.Tournaments.Results.IView> _view;
    private Mock<TournamentManager.Tournaments.Results.IAdapter> _adapter;

    private TournamentManager.Tournaments.Results.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<TournamentManager.Tournaments.Results.IView>();
        _adapter = new Mock<TournamentManager.Tournaments.Results.IAdapter>();

        _presenter = new TournamentManager.Tournaments.Results.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task AtLargeAsync_AdapterAtLarge_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.Id).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.AtLargeAsync(cancellationToken).ConfigureAwait(false);

        _adapter.Verify(adapter => adapter.AtLargeAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task AtLargeAsync_AdapterHasError_ErrorFlow()
    {
        var error = new TournamentManager.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        await _presenter.AtLargeAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<IEnumerable<TournamentManager.Tournaments.Results.IAtLargeViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task AtLargeAsync_AdapterHasNoError_ViewBindResults_CalledCorrectly()
    {
        var result1 = new Mock<TournamentManager.Tournaments.Results.IAtLargeViewModel>();
        result1.SetupGet(result => result.DivisionName).Returns("Division 1");
        result1.SetupGet(result => result.Score).Returns(100);

        var result2 = new Mock<TournamentManager.Tournaments.Results.IAtLargeViewModel>();
        result2.SetupGet(result => result.DivisionName).Returns("Division 1");
        result2.SetupGet(result => result.Score).Returns(101);

        var result3 = new Mock<TournamentManager.Tournaments.Results.IAtLargeViewModel>();
        result3.SetupGet(result => result.DivisionName).Returns("Division 2");
        result3.SetupGet(result => result.Score).Returns(200);

        var result4 = new Mock<TournamentManager.Tournaments.Results.IAtLargeViewModel>();
        result4.SetupGet(result => result.DivisionName).Returns("Division 2");
        result4.SetupGet(result => result.Score).Returns(201);

        var results = new[] { result1.Object, result2.Object, result3.Object, result4.Object };
        _adapter.Setup(adapter => adapter.AtLargeAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(results);

        await _presenter.AtLargeAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<IEnumerable<TournamentManager.Tournaments.Results.IAtLargeViewModel>>()), Times.Exactly(2));

            _view.Verify(view => view.BindResults("Division 1", It.Is<IEnumerable<TournamentManager.Tournaments.Results.IAtLargeViewModel>>(atLarge => atLarge.Count(x => x.Score == 100) == 1 && atLarge.Count(x => x.Score == 101) == 1)), Times.Once);
            _view.Verify(view => view.BindResults("Division 2", It.Is<IEnumerable<TournamentManager.Tournaments.Results.IAtLargeViewModel>>(atLarge => atLarge.Count(x => x.Score == 200) == 1 && atLarge.Count(x => x.Score == 201) == 1)), Times.Once);
        });
    }
}
