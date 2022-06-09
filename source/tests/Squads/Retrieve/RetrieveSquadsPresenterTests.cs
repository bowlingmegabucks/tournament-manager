
namespace NewEnglandClassic.Tests.Squads.Retrieve;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Squads.Retrieve.IView> _view;
    private Mock<NewEnglandClassic.Squads.Retrieve.IAdapter> _getSquadsAdapter;

    private NewEnglandClassic.Squads.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Squads.Retrieve.IView>();
        _getSquadsAdapter = new Mock<NewEnglandClassic.Squads.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Squads.Retrieve.Presenter(_view.Object, _getSquadsAdapter.Object);
    }

    [Test]
    public void Execute_GetSquadsAdapterForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();

        _presenter.Execute(tournamentId);

        _getSquadsAdapter.Verify(a => a.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetSquadsAdapterHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");

        _getSquadsAdapter.SetupGet(getSquadsAdapter => getSquadsAdapter.Error).Returns(error);

        var tournamentId = Guid.NewGuid();

        _presenter.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.Disable(), Times.Once);
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindSquads(It.IsAny<IEnumerable<NewEnglandClassic.Squads.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetSquadsAdapterReturnsSquads_ViewBindSquads_CalledCorrectly()
    {
        var squad1 = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad1.SetupGet(squad => squad.Date).Returns(DateTime.Now);
        squad1.SetupGet(squad => squad.MaxPerPair).Returns(1);

        var squad2 = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad2.SetupGet(squad => squad.Date).Returns(DateTime.Now.AddDays(1));
        squad2.SetupGet(squad => squad.MaxPerPair).Returns(2);

        var squad3 = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad3.SetupGet(squad => squad.Date).Returns(DateTime.Now.AddHours(-1));
        squad3.SetupGet(squad => squad.MaxPerPair).Returns(3);

        var squads = new[] { squad1.Object, squad2.Object, squad3.Object };
        _getSquadsAdapter.Setup(getSquadsAdapter => getSquadsAdapter.ForTournament(It.IsAny<Guid>())).Returns(squads);

        var tournamentId = Guid.NewGuid();

        _presenter.Execute(tournamentId);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(collection => collection.ToList()[0].MaxPerPair == 3)), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(collection => collection.ToList()[1].MaxPerPair == 1)), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(collection => collection.ToList()[2].MaxPerPair == 2)), Times.Once);
        });
    }
}
