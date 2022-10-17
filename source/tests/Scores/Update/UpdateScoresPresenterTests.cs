using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Scores.Update;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Scores.Update.IView> _view;
    private Mock<NortheastMegabuck.Scores.Update.IAdapter> _adapter;

    private NortheastMegabuck.Scores.Update.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Scores.Update.IView>();
        _adapter = new Mock<NortheastMegabuck.Scores.Update.IAdapter>();

        _presenter = new NortheastMegabuck.Scores.Update.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterExecute_CalledCorrectly()
    {
        var scores = new Mock<IEnumerable<NortheastMegabuck.Scores.Update.IViewModel>>();
        _view.SetupGet(view => view.Scores).Returns(scores.Object);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(scores.Object), Times.Once);
    }

    [Test]
    public void Execute_AdapterHasErrors_ErrorFlow()
    {
        var error1 = new NortheastMegabuck.Models.ErrorDetail("error1");
        var error2 = new NortheastMegabuck.Models.ErrorDetail("error2");

        var errors = new[] { error1, error2 };
        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError($"error1{Environment.NewLine}error2"), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterHasNoErrors_SuccessFlow()
    {
        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Scores updated"), Times.Once);

            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.KeepOpen(), Times.Never);
        });
    }
}
