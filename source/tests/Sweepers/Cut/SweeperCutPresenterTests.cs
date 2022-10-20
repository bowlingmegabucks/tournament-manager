using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Sweepers.Cut;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Sweepers.Cut.IView> _view;
    private Mock<NortheastMegabuck.Sweepers.Cut.IAdapter> _adapter;

    private NortheastMegabuck.Sweepers.Cut.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Sweepers.Cut.IView>();
        _adapter = new Mock<NortheastMegabuck.Sweepers.Cut.IAdapter>();

        _presenter = new NortheastMegabuck.Sweepers.Cut.Presenter(_view.Object, _adapter.Object);
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

            _view.Verify(view => view.BindResults(It.IsAny<IEnumerable<NortheastMegabuck.Sweepers.Cut.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_SquadId_AdapterExecuteNoError_ViewBindResults_CalledCorrectly()
    {
        var results = new Mock<IEnumerable<NortheastMegabuck.Sweepers.Cut.IViewModel>>().Object;
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(results);

        _presenter.Execute(SquadId.New());

        _view.Verify(view => view.BindResults(results), Times.Once);
    }
}
