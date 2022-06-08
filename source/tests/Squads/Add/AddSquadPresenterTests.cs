using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Squads.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NewEnglandClassic.Squads.Add.IView> _view;
    private Mock<NewEnglandClassic.Tournaments.Retrieve.IAdapter> _retrieveTournamentAdapter;

    private NewEnglandClassic.Squads.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NewEnglandClassic.Squads.Add.IView>();
        _retrieveTournamentAdapter = new Mock<NewEnglandClassic.Tournaments.Retrieve.IAdapter>();

        _presenter = new NewEnglandClassic.Squads.Add.Presenter(_view.Object, _retrieveTournamentAdapter.Object);
    }

    [Test]
    public void GetTournamentRatios_RetrieveTournamentAdapterExecute_CalledCorrectly()
    {
        var tournamentId = Guid.NewGuid();
        
        var squad = new Mock<NewEnglandClassic.Squads.IViewModel>();
        squad.SetupGet(s => s.TournamentId).Returns(tournamentId);

        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var tournament = new Mock<NewEnglandClassic.Tournaments.IViewModel>();
        _retrieveTournamentAdapter.Setup(retrieveTournamentAdapter => retrieveTournamentAdapter.Execute(It.IsAny<Guid>())).Returns(tournament.Object);

        _presenter.GetTournamentRatios();

        _retrieveTournamentAdapter.Verify(retrieveTournamentAdapter => retrieveTournamentAdapter.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void GetTournamentRatios_RetrieveTournamentAdapterHasError_ErrorFlow()
    {
        var squad = new Mock<NewEnglandClassic.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        var error = new NewEnglandClassic.Models.ErrorDetail("error");
        _retrieveTournamentAdapter.SetupGet(retrieveTournamentAdapter => retrieveTournamentAdapter.Error).Returns(error);

        _presenter.GetTournamentRatios();

        Assert.Multiple(()=>
        {
            _view.Verify(view => view.DisplayError(error.Message), Times.Once);
            
            _view.Verify(view => view.SetTournamentFinalsRatio(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.SetTournamentCashRatio(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void GetTournamentRatios_RetrieveTournamentAdapterHasNoError_SuccessFlow()
    {
        var tournament = new Mock<NewEnglandClassic.Tournaments.IViewModel>();
        tournament.SetupGet(t => t.FinalsRatio).Returns(1m);
        tournament.SetupGet(t => t.CashRatio).Returns(3m);

        _retrieveTournamentAdapter.Setup(retrieveTournamentAdapter => retrieveTournamentAdapter.Execute(It.IsAny<Guid>())).Returns(tournament.Object);

        var squad = new Mock<NewEnglandClassic.Squads.IViewModel>();
        _view.SetupGet(view => view.Squad).Returns(squad.Object);

        _presenter.GetTournamentRatios();
        
        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            
            _view.Verify(view => view.SetTournamentFinalsRatio("1.0"), Times.Once);
            _view.Verify(view => view.SetTournamentCashRatio("3.0"), Times.Once);
        });
    }
}
