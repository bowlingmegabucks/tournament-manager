
namespace NortheastMegabuck.Tests.Squads.Retrieve;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Squads.Retrieve.IView> _view;
    private Mock<NortheastMegabuck.Squads.Retrieve.IAdapter> _getSquadsAdapter;

    private NortheastMegabuck.Squads.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Squads.Retrieve.IView>();
        _getSquadsAdapter = new Mock<NortheastMegabuck.Squads.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.Squads.Retrieve.Presenter(_view.Object, _getSquadsAdapter.Object);
    }

    [Test]
    public void Execute_GetSquadsAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Execute();

        _getSquadsAdapter.Verify(a => a.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetSquadsAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");

        _getSquadsAdapter.SetupGet(getSquadsAdapter => getSquadsAdapter.Error).Returns(error);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.Disable(), Times.Once);
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindSquads(It.IsAny<IEnumerable<NortheastMegabuck.Squads.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetSquadsAdapterReturnsSquads_ViewBindSquads_CalledCorrectly()
    {
        var squad1 = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad1.SetupGet(squad => squad.Date).Returns(DateTime.Now);
        squad1.SetupGet(squad => squad.MaxPerPair).Returns(1);

        var squad2 = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad2.SetupGet(squad => squad.Date).Returns(DateTime.Now.AddDays(1));
        squad2.SetupGet(squad => squad.MaxPerPair).Returns(2);

        var squad3 = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad3.SetupGet(squad => squad.Date).Returns(DateTime.Now.AddHours(-1));
        squad3.SetupGet(squad => squad.MaxPerPair).Returns(3);

        var squads = new[] { squad1.Object, squad2.Object, squad3.Object };
        _getSquadsAdapter.Setup(getSquadsAdapter => getSquadsAdapter.Execute(It.IsAny<TournamentId>())).Returns(squads);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NortheastMegabuck.Squads.IViewModel>>(collection => collection.ToList()[0].MaxPerPair == 3)), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NortheastMegabuck.Squads.IViewModel>>(collection => collection.ToList()[1].MaxPerPair == 1)), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NortheastMegabuck.Squads.IViewModel>>(collection => collection.ToList()[2].MaxPerPair == 2)), Times.Once);
        });
    }

    [Test]
    public void AddSquad_ViewAddSquad_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.AddSquad();

        _view.Verify(view => view.AddSquad(tournamentId), Times.Once);
    }

    [Test]
    public void AddSquad_ViewAddSquadReturnsNull_ViewRefreshSquads_NotCalled()
    {
        _view.Setup(view => view.AddSquad(It.IsAny<TournamentId>())).Returns((SquadId?)null);

        _presenter.AddSquad();

        _view.Verify(view => view.RefreshSquads(), Times.Never);
    }

    [Test]
    public void AddSquad_ViewAddSquadReturnsId_ViewRefreshSquads_Called()
    {
        _view.Setup(view => view.AddSquad(It.IsAny<TournamentId>())).Returns(SquadId.New());

        _presenter.AddSquad();

        _view.Verify(view => view.RefreshSquads(), Times.Once);
    }
}
