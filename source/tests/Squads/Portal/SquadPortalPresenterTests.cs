
namespace NortheastMegabuck.Tests.Squads.Portal;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Squads.Portal.IView> _view;
    private Mock<NortheastMegabuck.Squads.Retrieve.IAdapter> _adapter;

    private NortheastMegabuck.Squads.Portal.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Squads.Portal.IView>();
        _adapter = new Mock<NortheastMegabuck.Squads.Retrieve.IAdapter>();

        _presenter = new NortheastMegabuck.Squads.Portal.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Load_RetrieveSquadAdapterExecute_CalledCorrectly()
    {
        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(squad.Object);

        var squadId = new SquadId();
        _view.SetupGet(view => view.Id).Returns(squadId);

        _presenter.Load();

        _adapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Load_RetrieveSquadAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        var squadId = new SquadId();
        _view.SetupGet(view => view.Id).Returns(squadId);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);

            _view.Verify(view => view.SetPortalTitle(It.IsAny<string>()), Times.Never);
        });
    }

    [Test]
    public void Load_RetrieveSquadAdapterHasNoError_ViewSetPortalTitle_CalledCorrectly()
    {
        var squad = new Mock<NortheastMegabuck.Squads.IViewModel>();
        squad.SetupGet(s => s.Date).Returns(new DateTime(2000, 1, 2, 9, 30, 0));
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(squad.Object);

        var squadId = new SquadId();
        _view.SetupGet(view => view.Id).Returns(squadId);

        _presenter.Load();

        _view.Verify(view => view.SetPortalTitle("01/02/2000 09:30AM"), Times.Once);
    }
}
