using NortheastMegabuck.Tests.Extensions;

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

    [Test]
    public void Execute_ViewIsValid_Called()
    {
        _namePresenter.Execute();

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidFalse_NothingElseCalled()
    {
        _view.IsValid_False();

        _namePresenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _updateBowlerAdapter.Verify(adapter => adapter.Execute(It.IsAny<BowlerId>(), It.IsAny<NortheastMegabuck.Bowlers.Update.INameViewModel>()), Times.Never);
            _view.Verify(view => view.DisplayErrors(It.IsAny<IEnumerable<string>>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_UpdateBowlerNameAdapterExecute_CalledCorrectly()
    {
        _view.IsValid_True();

        var bowlerId = BowlerId.New();
        _view.SetupGet(view => view.Id).Returns(bowlerId);

        var bowlerName = new Mock<NortheastMegabuck.Bowlers.Update.INameViewModel>().Object;
        _view.SetupGet(view => view.BowlerName).Returns(bowlerName);

        _namePresenter.Execute();

        _updateBowlerAdapter.Verify(adapter => adapter.Execute(bowlerId, bowlerName), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidTrue_UpdateBowlerNameAdapterExecuteHasErrors_ErrorFlow()
    {
        _view.IsValid_True();

        var errors = new[] { new NortheastMegabuck.Models.ErrorDetail("error1"), new NortheastMegabuck.Models.ErrorDetail("error2") };
        _updateBowlerAdapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        _namePresenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayErrors(new[] { "error1", "error2" }), Times.Once);
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_UpdateBowlerNameAdapterExecuteSuccessful_ViewDisplayMessage_CalledCorrectly()
    {
        _view.IsValid_True();

        var fullName = "fullName";
        _view.SetupGet(view => view.FullName).Returns(fullName);

        _namePresenter.Execute();

        _view.Verify(view => view.DisplayMessage("fullName's name updated"), Times.Once);
    }
}
