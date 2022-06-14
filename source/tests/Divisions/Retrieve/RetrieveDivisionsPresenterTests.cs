namespace NewEnglandClassic.Tests.Divisions.Retrieve;
internal class Presenter
{
    private Mock<NewEnglandClassic.Divisions.Retrieve.IView> _view;
    private Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter> _adapter;

    private NewEnglandClassic.Divisions.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Divisions.Retrieve.IView>();
        _adapter = new Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Divisions.Retrieve.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_AdapterErrorNotNull_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once());

            _view.Verify(view => view.BindDivisions(It.IsAny<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterErrorNull_ViewBindDivisions_CalledCorrectly()
    {
        var divisions = new List<NewEnglandClassic.Divisions.IViewModel>();
        _adapter.Setup(adapter => adapter.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        _presenter.Execute();

        _view.Verify(view => view.BindDivisions(divisions), Times.Once);
    }

    [Test]
    public void AddDivision_ViewAddDivision_Called()
    {
        var id = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(id);
        
        _presenter.AddDivision();

        _view.Verify(view => view.AddDivision(id), Times.Once);
    }

    [Test]
    public void AddDivision_ViewAddDivisionReturnsId_ViewRefreshDivisions_Called()
    {
        var id = Guid.NewGuid();
        _view.Setup(view => view.AddDivision(It.IsAny<Guid>())).Returns(id);

        _presenter.AddDivision();

        _view.Verify(view => view.RefreshDivisions(), Times.Once);
    }

    [Test]
    public void AddDivision_ViewAddDivisionReturnsNull_ViewRefreshDivisions_NotCalled()
    {
        _view.Setup(view => view.AddDivision(It.IsAny<Guid>())).Returns((Guid?)null);

        _presenter.AddDivision();

        _view.Verify(view => view.RefreshDivisions(), Times.Never);
    }
}
