
namespace NewEnglandClassic.Tests.Registrations.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Registrations.Add.IView> _view;

    private Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter> _divisionsAdapter;
    private Mock<NewEnglandClassic.Squads.Retrieve.IAdapter> _squadsAdapter;
    private Mock<NewEnglandClassic.Sweepers.Retrieve.IAdapter> _sweepersAdapter;

    private NewEnglandClassic.Registrations.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Registrations.Add.IView>();

        _divisionsAdapter = new Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter>();
        _squadsAdapter = new Mock<NewEnglandClassic.Squads.Retrieve.IAdapter>();
        _sweepersAdapter = new Mock<NewEnglandClassic.Sweepers.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Registrations.Add.Presenter(_view.Object, _divisionsAdapter.Object, _squadsAdapter.Object, _sweepersAdapter.Object);
    }

    [Test]
    public void Load_DivisionsAdapterForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        _divisionsAdapter.Verify(adapter => adapter.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void Load_SquadsAdapterForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        _squadsAdapter.Verify(adapter => adapter.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void Load_SweepersAdapterForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        _sweepersAdapter.Verify(adapter => adapter.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void Load_AllAdaptersHaveErrors_DivisionAdapterErrorFlow()
    {
        var divisionError = new NewEnglandClassic.Models.ErrorDetail("division");
        _divisionsAdapter.SetupGet(adapter => adapter.Error).Returns(divisionError);

        var squadError = new NewEnglandClassic.Models.ErrorDetail("squad");
        _squadsAdapter.SetupGet(adapter => adapter.Error).Returns(squadError);

        var sweeperError = new NewEnglandClassic.Models.ErrorDetail("sweeper");
        _sweepersAdapter.SetupGet(adapter => adapter.Error).Returns(sweeperError);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("division"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);
        });
    }

    [Test]
    public void Load_DivisionAdapterNoError_SweeperAndSquadAdapterError_SquadAdapterErrorFlow()
    {
        var squadError = new NewEnglandClassic.Models.ErrorDetail("squad");
        _squadsAdapter.SetupGet(adapter => adapter.Error).Returns(squadError);

        var sweeperError = new NewEnglandClassic.Models.ErrorDetail("sweeper");
        _sweepersAdapter.SetupGet(adapter => adapter.Error).Returns(sweeperError);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("squad"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);
        });
    }

    [Test]
    public void Load_DivisionAdapterAndSquadAdapterNoError_SweeperAdapterError_SweeperErrorFlow()
    {
        var sweeperError = new NewEnglandClassic.Models.ErrorDetail("sweeper");
        _sweepersAdapter.SetupGet(adapter => adapter.Error).Returns(sweeperError);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("sweeper"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);
        });
    }

    [Test]
    public void Load_NoAdapterErrors_ViewBindDivisions_CalledSortedByDivisionNumber()
    {
        var division1 = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        division1.SetupGet(division => division.Number).Returns(1);

        var division2 = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        division2.SetupGet(division => division.Number).Returns(2);

        var division3 = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        division3.SetupGet(division => division.Number).Returns(3);

        var divisions = new[] { division3.Object, division1.Object, division2.Object };
        _divisionsAdapter.Setup(adapter => adapter.ForTournament(It.IsAny<Guid>())).Returns(divisions);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>(divisions => divisions.ToList()[0].Number == 1)), Times.Once);
            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>(divisions => divisions.ToList()[1].Number == 2)), Times.Once);
            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>(divisions => divisions.ToList()[2].Number == 3)), Times.Once);

            _view.Verify(view => view.BindDivisions(It.Is<IEnumerable<NewEnglandClassic.Divisions.IViewModel>>(divisions => divisions.ToList().Count == 3)), Times.Once);
        });
    }

    [Test]
    public void Load_NoAdapterErrors_ViewBindSquads_CalledSortedByDate()
    {
        var squad1 = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad1.SetupGet(squad => squad.Date).Returns(new DateTime(2015, 1, 1));

        var squad2 = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad2.SetupGet(squad => squad.Date).Returns(new DateTime(2015, 1, 2));

        var squad3 = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad3.SetupGet(squad => squad.Date).Returns(new DateTime(2015, 1, 3));

        var squads = new[] { squad3.Object, squad1.Object, squad2.Object };
        _squadsAdapter.Setup(adapter => adapter.ForTournament(It.IsAny<Guid>())).Returns(squads);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(squads => squads.ToList()[0].Date == new DateTime(2015, 1, 1))), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(squads => squads.ToList()[1].Date == new DateTime(2015, 1, 2))), Times.Once);
            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(squads => squads.ToList()[2].Date == new DateTime(2015, 1, 3))), Times.Once);

            _view.Verify(view => view.BindSquads(It.Is<IEnumerable<NewEnglandClassic.Squads.IViewModel>>(squads => squads.ToList().Count == 3)), Times.Once);
        });
    }

    [Test]
    public void Load_NoAdapterErrors_ViewBindSweepers_CalledSortedByDate()
    {
        var sweeper1 = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper1.SetupGet(squad => squad.Date).Returns(new DateTime(2015, 1, 1));

        var sweeper2 = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper2.SetupGet(squad => squad.Date).Returns(new DateTime(2015, 1, 2));

        var sweeper3 = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper3.SetupGet(squad => squad.Date).Returns(new DateTime(2015, 1, 3));

        var sweepers = new[] { sweeper3.Object, sweeper1.Object, sweeper2.Object };
        _sweepersAdapter.Setup(adapter => adapter.ForTournament(It.IsAny<Guid>())).Returns(sweepers);

        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Load();
        
        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(sweepers => sweepers.ToList()[0].Date == new DateTime(2015, 1, 1))), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(sweepers => sweepers.ToList()[1].Date == new DateTime(2015, 1, 2))), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(sweepers => sweepers.ToList()[2].Date == new DateTime(2015, 1, 3))), Times.Once);

            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(sweepers => sweepers.ToList().Count == 3)), Times.Once);
        });
    }
}
