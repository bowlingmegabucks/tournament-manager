using Newtonsoft.Json.Serialization;

namespace NortheastMegabuck.Tests.Tournaments.Retrieve;

[TestFixture]
internal class Presenters
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IView> _view;
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IAdapter> _adapter;

    private NortheastMegabuck.Tournaments.Retrieve.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Tournaments.Retrieve.IView>();
        _adapter = new Mock<NortheastMegabuck.Tournaments.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.Tournaments.Retrieve.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public async Task ExecuteAsync_AdapterExecute_Called()
    {
        CancellationToken cancellationToken = default;

        await _presenter.ExecuteAsync(cancellationToken);

        _adapter.Verify(adapter => adapter.ExecuteAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorNotNull_ErrorFlow()
    {
        var errorDetail = new NortheastMegabuck.Models.ErrorDetail("message");
        _adapter.SetupGet(adapter=> adapter.Error).Returns(errorDetail);

        await _presenter.ExecuteAsync(default);

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrorMessage("message"), Times.Once);
            _view.Verify(view => view.DisableOpenTournament(), Times.Once);

            _view.Verify(view => view.BindTournaments(It.IsAny<ICollection<NortheastMegabuck.Tournaments.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorDetailNull_NoTournamentsReturned_ViewDisableOpenTournamentCalled()
    {
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(Enumerable.Empty<NortheastMegabuck.Tournaments.IViewModel>());

        await _presenter.ExecuteAsync(default);

        _view.Verify(view => view.DisableOpenTournament(), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_AdapterErrorDetailNull_TournamentsReturned_ViewBindTournamentsCalled()
    {
        var tournaments = Enumerable.Repeat(new Mock<NortheastMegabuck.Tournaments.IViewModel>().Object, 3).ToList();
        _adapter.Setup(adapter => adapter.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tournaments);

        await _presenter.ExecuteAsync(default);

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
        _view.Setup(view => view.CreateNewTournament()).Returns((null, string.Empty, 0));

        _presenter.NewTournament();

        _view.Verify(view => view.OpenTournament(It.IsAny<TournamentId>(), It.IsAny<string>(), It.IsAny<short>()), Times.Never);
    }

    [Test]
    public void NewTournament_ViewCreateNewTournamentReturnsId_ViewOpenTournamentCalledCorrectly()
    {
        var id = TournamentId.New();

        _view.Setup(view => view.CreateNewTournament()).Returns((id, "tournamentName", 5));

        _presenter.NewTournament();

        _view.Verify(view => view.OpenTournament(id, "tournamentName", 5), Times.Once);
    }
}
