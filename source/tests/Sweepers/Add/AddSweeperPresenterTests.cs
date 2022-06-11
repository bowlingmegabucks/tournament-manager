
namespace NewEnglandClassic.Tests.Sweepers.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Sweepers.Add.IView> _view;
    private Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter> _retrieveDivisionsAdapter;
    private Mock<NewEnglandClassic.Sweepers.Add.IAdapter> _adapter;

    private NewEnglandClassic.Sweepers.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Sweepers.Add.IView>();
        _retrieveDivisionsAdapter = new Mock<NewEnglandClassic.Divisions.Retrieve.IAdapter>();
        _adapter = new Mock<NewEnglandClassic.Sweepers.Add.IAdapter>();

        _presenter = new NewEnglandClassic.Sweepers.Add.Presenter(_view.Object, _retrieveDivisionsAdapter.Object, _adapter.Object);
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

    [Test]
    public void Execute_ViewIsValid_Called()
    {
        _presenter.Execute();

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidFalse_NothingElseCalled()
    {
        _view.Setup(view => view.IsValid()).Returns(false);

        var sweeper = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _adapter.Verify(adapter => adapter.Execute(It.IsAny<NewEnglandClassic.Sweepers.IViewModel>()), Times.Never);

            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            sweeper.VerifySet(s => s.Id = It.IsAny<Guid>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_AdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var sweeper = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        var sweeperId = Guid.NewGuid();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<NewEnglandClassic.Sweepers.IViewModel>())).Returns(sweeperId);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(sweeper.Object), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidTrue_AdapterHasErrors_ErrorFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var sweeper = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        var errors = new[]
        {
            new NewEnglandClassic.Models.ErrorDetail("error1"),
            new NewEnglandClassic.Models.ErrorDetail("error2"),
            new NewEnglandClassic.Models.ErrorDetail("error3")
        };

        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2{Environment.NewLine}error3"), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            sweeper.VerifySet(s => s.Id = It.IsAny<Guid>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_AdapterSuccessful_SuccessPath()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var sweeper = new Mock<NewEnglandClassic.Sweepers.IViewModel>();
        sweeper.SetupGet(s => s.Date).Returns(new DateTime(2000, 1, 2, 9, 30, 00));
        _view.SetupGet(view => view.Sweeper).Returns(sweeper.Object);

        var sweeperId = Guid.NewGuid();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<NewEnglandClassic.Sweepers.IViewModel>())).Returns(sweeperId);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Never);
            _view.Verify(view => view.DisplayMessage("Sweeper added for 01/02/2000 09:30 AM"), Times.Once);
            sweeper.VerifySet(s => s.Id = sweeperId, Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}   