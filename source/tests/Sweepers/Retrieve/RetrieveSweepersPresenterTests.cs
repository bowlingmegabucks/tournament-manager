
namespace NewEnglandClassic.Tests.Sweepers.Retrieve;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Sweepers.Retrieve.IView> _view;
    private Mock<NewEnglandClassic.Sweepers.Retrieve.IAdapter> _getSweepersAdapter;

    private NewEnglandClassic.Sweepers.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Sweepers.Retrieve.IView>();
        _getSweepersAdapter = new Mock<NewEnglandClassic.Sweepers.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Sweepers.Retrieve.Presenter(_view.Object, _getSweepersAdapter.Object);
    }

    [Test]
    public void Execute_GetSweepersAdapterForTournament_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.Execute();

        _getSweepersAdapter.Verify(a => a.ForTournament(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_GetSweepersAdapterHasError_ErrorFlow()
    {
        var error = new NewEnglandClassic.Models.ErrorDetail("error");

        _getSweepersAdapter.SetupGet(getSweepersAdapter => getSweepersAdapter.Error).Returns(error);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.Disable(), Times.Once);
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindSweepers(It.IsAny<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_GetSweepersAdapterReturnsSweepers_ViewBindSweepers_CalledCorrectly()
    {
        var sweeper1 = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper1.SetupGet(sweeper => sweeper.Date).Returns(DateTime.Now);
        sweeper1.SetupGet(sweeper => sweeper.MaxPerPair).Returns(1);

        var sweeper2 = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper2.SetupGet(sweeper => sweeper.Date).Returns(DateTime.Now.AddDays(1));
        sweeper2.SetupGet(sweeper => sweeper.MaxPerPair).Returns(2);

        var sweeper3 = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper3.SetupGet(sweeper => sweeper.Date).Returns(DateTime.Now.AddHours(-1));
        sweeper3.SetupGet(sweeper => sweeper.MaxPerPair).Returns(3);

        var sweepers = new[] { sweeper1.Object, sweeper2.Object, sweeper3.Object };
        _getSweepersAdapter.Setup(getSweepersAdapter => getSweepersAdapter.ForTournament(It.IsAny<Guid>())).Returns(sweepers);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(collection => collection.ToList()[0].MaxPerPair == 3)), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(collection => collection.ToList()[1].MaxPerPair == 1)), Times.Once);
            _view.Verify(view => view.BindSweepers(It.Is<IEnumerable<NewEnglandClassic.Sweepers.IViewModel>>(collection => collection.ToList()[2].MaxPerPair == 2)), Times.Once);
        });
    }

    [Test]
    public void AddSweeper_ViewAddSweeper_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        _view.SetupGet(view => view.TournamentId).Returns(tournamentId);

        _presenter.AddSweeper();

        _view.Verify(view => view.AddSweeper(tournamentId), Times.Once);
    }

    [Test]
    public void AddSweeper_ViewAddSweeperReturnsNull_ViewRefreshSweepers_NotCalled()
    {
        _view.Setup(view => view.AddSweeper(It.IsAny<Guid>())).Returns((SquadId?)null);

        _presenter.AddSweeper();

        _view.Verify(view => view.RefreshSweepers(), Times.Never);
    }

    [Test]
    public void AddSweeper_ViewAddSweeperReturnsGuid_ViewRefreshSweepers_Called()
    {
        _view.Setup(view => view.AddSweeper(It.IsAny<Guid>())).Returns(SquadId.New());

        _presenter.AddSweeper();

        _view.Verify(view => view.RefreshSweepers(), Times.Once);
    }
}
