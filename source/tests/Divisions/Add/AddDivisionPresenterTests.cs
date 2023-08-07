namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal sealed class Presenter
{
    private Mock<NortheastMegabuck.Divisions.Add.IView> _view;
    private Mock<NortheastMegabuck.Divisions.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<NortheastMegabuck.Divisions.Add.IAdapter> _addAdapter;

    private NortheastMegabuck.Divisions.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Divisions.Add.IView>();
        _retrieveAdapter = new Mock<NortheastMegabuck.Divisions.Retrieve.IAdapter>();
        _addAdapter = new Mock<NortheastMegabuck.Divisions.Add.IAdapter>();

        _presenter = new NortheastMegabuck.Divisions.Add.Presenter(_view.Object, _retrieveAdapter.Object, _addAdapter.Object);
    }

    [Test]
    public async Task GetNextDivisionNumberAsync_RetrieveDivisionsAdapterExecute_CalledCorrectly()
    {
        var division = new Mock<NortheastMegabuck.Divisions.IViewModel>();
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
        var division = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        var error = new NortheastMegabuck.Models.ErrorDetail("error");
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
        var division = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        var divisions = Enumerable.Repeat(new Mock<NortheastMegabuck.Divisions.IViewModel>(), divisionCount).Select(mock => mock.Object).ToList();
        _retrieveAdapter.Setup(retrieveAdapter => retrieveAdapter.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        await _presenter.GetNextDivisionNumberAsync(default).ConfigureAwait(false);

        division.VerifySet(d => d.Number = expected, Times.Once);
    }
}
