namespace NortheastMegabuck.Tests.Divisions.Retrieve;
internal class Presenter
{
    private Mock<NortheastMegabuck.Divisions.Retrieve.IView> _view;
    private Mock<NortheastMegabuck.Divisions.Retrieve.IAdapter> _adapter;

    private NortheastMegabuck.Divisions.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Divisions.Retrieve.IView>();
        _adapter = new Mock<NortheastMegabuck.Divisions.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.Divisions.Retrieve.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_AdapterErrorNotNull_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once());

            _view.Verify(view => view.BindDivisions(It.IsAny<IEnumerable<NortheastMegabuck.Divisions.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterErrorNull_ViewBindDivisions_CalledCorrectly()
    {
        var divisions = new List<NortheastMegabuck.Divisions.IViewModel>();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<TournamentId>())).Returns(divisions);

        _presenter.Execute();

        _view.Verify(view => view.BindDivisions(divisions), Times.Once);
    }

    [Test]
    public void AddDivision_ViewAddDivision_Called()
    {
        var id = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(id);
        
        _presenter.AddDivision();

        _view.Verify(view => view.AddDivision(id), Times.Once);
    }

    [Test]
    public void AddDivision_ViewAddDivisionReturnsId_ViewRefreshDivisions_Called()
    {
        var id = NortheastMegabuck.Divisions.Id.New();
        _view.Setup(view => view.AddDivision(It.IsAny<TournamentId>())).Returns(id);

        _presenter.AddDivision();

        _view.Verify(view => view.RefreshDivisions(), Times.Once);
    }

    [Test]
    public void AddDivision_ViewAddDivisionReturnsNull_ViewRefreshDivisions_NotCalled()
    {
        _view.Setup(view => view.AddDivision(It.IsAny<TournamentId>())).Returns((NortheastMegabuck.Divisions.Id?)null);

        _presenter.AddDivision();

        _view.Verify(view => view.RefreshDivisions(), Times.Never);
    }
}
