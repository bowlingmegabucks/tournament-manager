using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Bowlers.Update;

[TestFixture]
internal class NamePresenter
{
    private Mock<NortheastMegabuck.Bowlers.Update.IBowlerNameView> _view;
    private Mock<NortheastMegabuck.Bowlers.Retrieve.IAdapter> _retrieveBowlerAdapter;
    private Mock<NortheastMegabuck.Bowlers.Update.IAdapter> _updateBowlerAdapter;

    private NortheastMegabuck.Bowlers.Update.NamePresenter _namePresenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Bowlers.Update.IBowlerNameView>();
        _retrieveBowlerAdapter = new Mock<NortheastMegabuck.Bowlers.Retrieve.IAdapter>();
        _updateBowlerAdapter = new Mock<NortheastMegabuck.Bowlers.Update.IAdapter>();

        _namePresenter = new NortheastMegabuck.Bowlers.Update.NamePresenter(_view.Object, _retrieveBowlerAdapter.Object, _updateBowlerAdapter.Object);
    }

    [Test]
    public void Load_RetrieveBowlerAdapterExecute_CalledCorrectly()
    {
        var bowlerId = BowlerId.New();
        _view.SetupGet(view => view.Id).Returns(bowlerId);

        _namePresenter.Load();

        _retrieveBowlerAdapter.Verify(adapter => adapter.Execute(bowlerId), Times.Once);
    }

    [Test]
    public void Load_RetrieveBowlerAdapterErrorNotNull_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveBowlerAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _namePresenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Disable(), Times.Once);

            _view.Verify(view => view.Bind(It.IsAny<NortheastMegabuck.Bowlers.Retrieve.IViewModel>()), Times.Never);
        });
    }

    [Test]
    public void Load_RetrievebowlerAdapterSuccessful_ViewBind_CalledCorrectly()
    {
        var bowler = new Mock<NortheastMegabuck.Bowlers.Retrieve.IViewModel>().Object;
        _retrieveBowlerAdapter.Setup(adapter => adapter.Execute(It.IsAny<BowlerId>())).Returns(bowler);

        _namePresenter.Load();

        _view.Verify(view => view.Bind(bowler), Times.Once);
    }
}
