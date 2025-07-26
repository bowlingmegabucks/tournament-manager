namespace BowlingMegabucks.TournamentManager.Tests.Divisions.Add;

[TestFixture]
internal sealed class Presenter
{
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IView> _view;
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IAdapter> _addAdapter;

    private BowlingMegabucks.TournamentManager.Divisions.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IView>();
        _retrieveAdapter = new Mock<BowlingMegabucks.TournamentManager.Divisions.Retrieve.IAdapter>();
        _addAdapter = new Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IAdapter>();

        _presenter = new BowlingMegabucks.TournamentManager.Divisions.Add.Presenter(_view.Object, _retrieveAdapter.Object, _addAdapter.Object);
    }

    [Test]
    public async Task GetNextDivisionNumberAsync_RetrieveDivisionsAdapterExecute_CalledCorrectly()
    {
        var division = new Mock<BowlingMegabucks.TournamentManager.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        CancellationToken cancellationToken = default;

        await _presenter.GetNextDivisionNumberAsync(cancellationToken).ConfigureAwait(false);

        _retrieveAdapter.Verify(adapter => adapter.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task GetNextDivisionNumberAsync_RetrieveDivisionAdapterHasErrors_ErrorFlow()
    {
        var division = new Mock<BowlingMegabucks.TournamentManager.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        var error = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(retrieveAdapter => retrieveAdapter.Error).Returns(error);

        await _presenter.GetNextDivisionNumberAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrors(new[] { "error" }), Times.Once);
            division.VerifySet(d => d.Number = It.IsAny<short>(), Times.Never);
        });
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public async Task GetNextDivisionNumber_RetrieveDivisionAdapterHasNoErrors_NextDivisionNumberSet(short divisionCount, short expected)
    {
        var division = new Mock<BowlingMegabucks.TournamentManager.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        var divisions = Enumerable.Repeat(new Mock<BowlingMegabucks.TournamentManager.Divisions.IViewModel>(), divisionCount).Select(mock => mock.Object).ToList();
        _retrieveAdapter.Setup(retrieveAdapter => retrieveAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        await _presenter.GetNextDivisionNumberAsync(default).ConfigureAwait(false);

        division.VerifySet(d => d.Number = expected, Times.Once);
    }
}
