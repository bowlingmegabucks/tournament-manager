
namespace NortheastMegabuck.Tests.Sweepers.Portal;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Sweepers.Portal.IView> _view;
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IAdapter> _retrieveAdapter;
    private Mock<NortheastMegabuck.Sweepers.Complete.IAdapter> _completeAdapter;

    private NortheastMegabuck.Sweepers.Portal.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Sweepers.Portal.IView>();
        _retrieveAdapter = new Mock<NortheastMegabuck.Sweepers.Retrieve.IAdapter>();
        _completeAdapter = new Mock<NortheastMegabuck.Sweepers.Complete.IAdapter>();

        _presenter = new NortheastMegabuck.Sweepers.Portal.Presenter(_view.Object, _retrieveAdapter.Object, _completeAdapter.Object);
    }

    [Test]
    public void Load_RetrieveSquadAdapterExecute_CalledCorrectly()
    {
        var squad = new NortheastMegabuck.Sweepers.ViewModel();
        _retrieveAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(squad);

        var squadId = SquadId.New();
        _view.SetupGet(view => view.Id).Returns(squadId);

        _presenter.Load();

        _retrieveAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Load_RetrieveSquadAdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Load();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);

            _view.Verify(view => view.SetPortalTitle(It.IsAny<string>()), Times.Never);
            _view.VerifySet(view => view.StartingLane = It.IsAny<int>(), Times.Never);
            _view.VerifySet(view => view.NumberOfLanes = It.IsAny<int>(), Times.Never);
            _view.VerifySet(view => view.MaxPerPair = It.IsAny<int>(), Times.Never);
            _view.VerifySet(view => view.Complete = It.IsAny<bool>(), Times.Never);
        });
    }

    [Test]
    public void Load_RetrieveSquadAdapterSuccessful_ViewFieldsSetCorrectly([Values] bool complete)
    {
        var squad = new NortheastMegabuck.Sweepers.ViewModel
        { 
            Date = new DateTime(2000,1,2,9,30,30),
            StartingLane = 1,
            NumberOfLanes = 2,
            MaxPerPair = 3,
            Complete = complete
        };
        _retrieveAdapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(squad);

        _presenter.Load();

        Assert.Multiple(() =>
        {

            _view.Verify(view => view.SetPortalTitle("01/02/2000 09:30AM"), Times.Once);
            _view.VerifySet(view => view.StartingLane = 1, Times.Once);
            _view.VerifySet(view => view.NumberOfLanes = 2, Times.Once);
            _view.VerifySet(view => view.MaxPerPair = 3, Times.Once);
            _view.VerifySet(view => view.Complete = complete, Times.Once);
        });
    }

    [Test]
    public void Complete_ViewConfirm_CalledCorrectly()
    {
        _presenter.Complete();

        _view.Verify(view => view.Confirm("Are you sure you want to complete this sweeper?"), Times.Once);
    }

    [Test]
    public void Complete_ViewConfirmFalse_CancelFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(false);

        _presenter.Complete();

        Assert.Multiple(() =>
        {
            _completeAdapter.Verify(adapter => adapter.Execute(It.IsAny<SquadId>()), Times.Never);

            _view.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Complete_ViewConfirmTrue_CompleteAdapterExecute_CalledCorrectly()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var squadId = SquadId.New();
        _view.SetupGet(view => view.Id).Returns(squadId);

        _presenter.Complete();

        _completeAdapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
    }

    [Test]
    public void Complete_ViewConfirmTrue_CompleteAdapterHasError_ErrorFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _completeAdapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Complete();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);

            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Complete_ViewConfirmTrue_ComplateAdapterSuccessful_SuccessFlow()
    {
        _view.Setup(view => view.Confirm(It.IsAny<string>())).Returns(true);

        _presenter.Complete();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("Sweeper successfully completed"), Times.Once);
            _view.Verify(view => view.Close(), Times.Once);
        });
    }
}
