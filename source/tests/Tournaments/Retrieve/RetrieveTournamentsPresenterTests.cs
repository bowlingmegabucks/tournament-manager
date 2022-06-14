namespace NewEnglandClassic.Tests.Tournaments.Retrieve;

[TestFixture]
internal class Presenters
{
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IView> _view;
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IAdapter> _adapter;

    private NewEnglandClassic.Tournaments.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Tournaments.Retrieve.IView>();
        _adapter = new Mock<NewEnglandClassic.Tournaments.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Tournaments.Retrieve.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterExecute_Called()
    {
        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(), Times.Once);
    }

    [Test]
    public void Execute_AdapterErrorNotNull_ErrorFlow()
    {
        var errorDetail = new NewEnglandClassic.Models.ErrorDetail("message");
        _adapter.SetupGet(adapter=> adapter.Error).Returns(errorDetail);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrorMessage("message"), Times.Once);
            _view.Verify(view => view.DisableOpenTournament(), Times.Once);

            _view.Verify(view => view.BindTournaments(It.IsAny<ICollection<NewEnglandClassic.Tournaments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterErrorDetailNull_NoTournamentsReturned_ViewDisableOpenTournamentCalled()
    {
        _adapter.Setup(adapter => adapter.Execute()).Returns(Enumerable.Empty<NewEnglandClassic.Tournaments.IViewModel>());

        _presenter.Execute();

        _view.Verify(view => view.DisableOpenTournament(), Times.Once);
    }

    [Test]
    public void Execute_AdapterErrorDetailNull_TournamentsReturned_ViewBindTournamentsCalled()
    {
        var tournaments = Enumerable.Repeat(new Mock<NewEnglandClassic.Tournaments.IViewModel>().Object, 3).ToList();
        _adapter.Setup(adapter => adapter.Execute()).Returns(tournaments);

        _presenter.Execute();

        _view.Verify(view => view.BindTournaments(tournaments), Times.Once);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournament_Called()
    {
        _presenter.NewTournament();

        _view.Verify(view => view.CreateNewTournament(), Times.Once);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournamentReturnsNull_ViewOpenTournamentNotCalled()
    {
        _view.Setup(view => view.CreateNewTournament()).Returns((null, string.Empty));

        _presenter.NewTournament();

        _view.Verify(view => view.OpenTournament(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournamentReturnsId_ViewOpenTournamentCalledCorrectly()
    {
        var guid = Guid.NewGuid();

        _view.Setup(view => view.CreateNewTournament()).Returns((guid, "tournamentName"));

        _presenter.NewTournament();

        _view.Verify(view => view.OpenTournament(guid, "tournamentName"), Times.Once);
    }
}
