
namespace NewEnglandClassic.Tests.Sweepers.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Sweepers.Add.IView> _view;
    private Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter> _retrieveDivisionsAdapter;

    private NewEnglandClassic.Sweepers.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Sweepers.Add.IView>();
        _retrieveDivisionsAdapter = new Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Sweepers.Add.Presenter(_view.Object, _retrieveDivisionsAdapter.Object);
    }

    [Test]
    public void GetDivisions_RetrieveDivisionsAdapter_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.GetDivisions();

        _retrieveDivisionsAdapter.Verify(adapter => adapter.ForTournament(tournamentId));
    }

    [Test]
    public void GetDivisions_RetrieveDivisionsAdapterHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _retrieveDivisionsAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.GetDivisions();

        Assert.Multiple(()=>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.BindDivisions(It.IsAny<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void GetDivisions_RetrieveDivisionsAdapterHasNoError_ViewBindDivisions_CalledCorrectly()
    {
        var divisions = new Mock<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>();
        _retrieveDivisionsAdapter.Setup(adapter => adapter.ForTournament(It.IsAny<Guid>())).Returns(divisions.Object);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.GetDivisions();

        _view.Verify(view => view.BindDivisions(divisions.Object), Times.Once);
    }
}   