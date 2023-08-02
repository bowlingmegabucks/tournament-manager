namespace NortheastMegabuck.Tests.Squads.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Squads.Add.IView> _view;
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IAdapter> _retrieveTournamentAdapter;
    private Mock<NortheastMegabuck.Squads.Add.IAdapter> _addSquadAdapter;

    private NortheastMegabuck.Squads.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Squads.Add.IView>();
        _retrieveTournamentAdapter = new Mock<NortheastMegabuck.Tournaments.Retrieve.IAdapter>();
        _addSquadAdapter = new Mock<NortheastMegabuck.Squads.Add.IAdapter>();

        _presenter = new NortheastMegabuck.Squads.Add.Presenter(_view.Object, _retrieveTournamentAdapter.Object, _addSquadAdapter.Object);
    }

    [Test]
    public void GetTournamentDetails_RetrieveTournamentAdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        
        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad.SetupGet(s => s.TournamentId).Returns(tournamentId);

        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var tournament = new Mock<NortheastMegabuck.Tournaments.IViewModel>();
        _retrieveTournamentAdapter.Setup(retrieveTournamentAdapter => retrieveTournamentAdapter.Execute(It.IsAny<TournamentId>())).Returns(tournament.Object);

        _presenter.GetTournamentDetails();

        _retrieveTournamentAdapter.Verify(retrieveTournamentAdapter => retrieveTournamentAdapter.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void GetTournamentDetails_RetrieveTournamentAdapterHasError_ErrorFlow()
    {
        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveTournamentAdapter.SetupGet(retrieveTournamentAdapter => retrieveTournamentAdapter.Error).Returns(error);

        _presenter.GetTournamentDetails();

        Assert.Multiple(()=>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);

            _view.Verify(view => view.SetTournamentEntryFee(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.SetTournamentFinalsRatio(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.SetTournamentCashRatio(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void GetTournamentDetails_RetrieveTournamentAdapterHasNoError_SuccessFlow()
    {
        var tournament = new Mock<NortheastMegabuck.Tournaments.IViewModel>();
        tournament.SetupGet(t => t.FinalsRatio).Returns(1m);
        tournament.SetupGet(t => t.CashRatio).Returns(3m);
        tournament.SetupGet(t => t.EntryFee).Returns(50m);

        _retrieveTournamentAdapter.Setup(retrieveTournamentAdapter => retrieveTournamentAdapter.Execute(It.IsAny<TournamentId>())).Returns(tournament.Object);

        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        _presenter.GetTournamentDetails();
        
        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);

            _view.Verify(view => view.SetTournamentEntryFee("$50.00"), Times.Once);
            _view.Verify(view => view.SetTournamentFinalsRatio("1.0"), Times.Once);
            _view.Verify(view => view.SetTournamentCashRatio("3.0"), Times.Once);
        });
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

        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        _presenter.Execute();

        Assert.Multiple(()=>
        {
            _addSquadAdapter.Verify(addSquadAdapter => addSquadAdapter.Execute(It.IsAny<NortheastMegabuck.Squads.IViewModel>()), Times.Never);

            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            squad.VerifySet(s => s.Id = It.IsAny<SquadId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_AddSquadAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var squadId = SquadId.New();
        _addSquadAdapter.Setup(addSquadAdapter => addSquadAdapter.Execute(It.IsAny<NortheastMegabuck.Squads.IViewModel>())).Returns(squadId);

        _presenter.Execute();

        _addSquadAdapter.Verify(addSquadAdapter => addSquadAdapter.Execute(squad.Object), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidTrue_AddSquadAdapterHasErrors_ErrorFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var errors = new[] 
        { 
            new NortheastMegabuck.Models.ErrorDetail("error1"), 
            new NortheastMegabuck.Models.ErrorDetail("error2"),
            new NortheastMegabuck.Models.ErrorDetail("error3")
        };

        _addSquadAdapter.SetupGet(addSquadAdapter => addSquadAdapter.Errors).Returns(errors);

        _presenter.Execute();

        Assert.Multiple(()=>
        {
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2{Environment.NewLine}error3"), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            squad.VerifySet(s => s.Id = It.IsAny<SquadId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_AddSquadAdapterSuccessful_SuccessPath()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad.SetupGet(s => s.Date).Returns(new DateTime(2000, 1, 2, 9, 30, 00, DateTimeKind.Unspecified));
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var squadId = SquadId.New();
        _addSquadAdapter.Setup(addSquadAdapter => addSquadAdapter.Execute(It.IsAny<NortheastMegabuck.Squads.IViewModel>())).Returns(squadId);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Never);
            _view.Verify(view => view.DisplayMessage("Squad added for 01/02/2000 09:30 AM"), Times.Once);
            squad.VerifySet(s => s.Id = squadId, Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}
