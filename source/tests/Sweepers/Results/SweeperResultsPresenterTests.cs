using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Sweepers.Results;

[TestFixture]
internal sealed class Presenter
{
    private Mock<NortheastMegabuck.Sweepers.Results.IView> _view;
    private Mock<NortheastMegabuck.Sweepers.Results.IAdapter> _adapter;

    private NortheastMegabuck.Sweepers.Results.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Sweepers.Results.IView>();
        _adapter = new Mock<NortheastMegabuck.Sweepers.Results.IAdapter>();

        _presenter = new NortheastMegabuck.Sweepers.Results.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_SquadId_AdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _presenter.Execute(squadId);

        _adapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_AdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<ICollection<NortheastMegabuck.Sweepers.Results.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_SquadId_AdapterExecuteNoError_ViewBindResults_CalledCorrectly()
    {
        var results = new List<NortheastMegabuck.Sweepers.Results.IViewModel>();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(results);

        _presenter.Execute(SquadId.New());

        _view.Verify(view => view.BindResults(results), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_AdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _presenter.Execute(tournamentId);

        _adapter.Verify(adapter => adapter.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_AdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Execute(TournamentId.New());

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<ICollection<NortheastMegabuck.Sweepers.Results.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_TournamentId_AdapterExecuteNoError_ViewBindResults_CalledCorrectly()
    {
        var results = new List<NortheastMegabuck.Sweepers.Results.IViewModel>();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<TournamentId>())).Returns(results);

        _presenter.Execute(TournamentId.New());

        _view.Verify(view => view.BindResults(results), Times.Once);
    }
}
