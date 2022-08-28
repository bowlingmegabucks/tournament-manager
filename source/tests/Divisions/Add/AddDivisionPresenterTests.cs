namespace NewEnglandClassic.Tests.Divisions.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Divisions.Add.IView> _view;
    private Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<NewEnglandClassic.Divisions.Add.IAdapter> _addAdapter;

    private NewEnglandClassic.Divisions.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Divisions.Add.IView>();
        _retrieveAdapter = new Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter>();
        _addAdapter = new Mock<NewEnglandClassic.Divisions.Add.IAdapter>();

        _presenter = new NewEnglandClassic.Divisions.Add.Presenter(_view.Object, _retrieveAdapter.Object, _addAdapter.Object);
    }

    [Test]
    public void GetNextDivisionNumber_RetrieveDivisionsAdapterExecute_CalledCorrectly()
    {
        var division = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);
        
        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        _presenter.GetNextDivisionNumber();

        _retrieveAdapter.Verify(adapter => adapter.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void GetNextDivisionNumber_RetrieveDivisionAdapterHasErrors_ErrorFlow()
    {
        var division = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(retrieveAdapter => retrieveAdapter.Error).Returns(error);

        _presenter.GetNextDivisionNumber();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrors(new[] { "error" }), Times.Once);
            division.VerifySet(d => d.Number = It.IsAny<short>(), Times.Never);
        });
    }

    [TestCase(0, 1)]
    [TestCase(1, 2)]
    [TestCase(2, 3)]
    public void GetNextDivisionNumber_RetrieveDivisionAdapterHasNoErrors_NextDivisionNumberSet(short divisionCount, short expected)
    {
        var division = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        _view.SetupGet(view => view.Division).Returns(division.Object);

        var tournamentId = TournamentId.New();
        division.SetupGet(d => d.TournamentId).Returns(tournamentId);

        var divisions = Enumerable.Repeat(new Mock<NewEnglandClassic.Divisions.IViewModel>(), divisionCount).Select(mock => mock.Object).ToList();
        _retrieveAdapter.Setup(retrieveAdapter => retrieveAdapter.Execute(It.IsAny<TournamentId>())).Returns(divisions);

        _presenter.GetNextDivisionNumber();

        division.VerifySet(d => d.Number = expected, Times.Once);
    }
}
