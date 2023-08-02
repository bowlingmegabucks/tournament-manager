
namespace NortheastMegabuck.Tests.Bowlers.Search;

[TestFixture]
internal sealed class Presenter
{
    private Mock<NortheastMegabuck.Bowlers.Search.IView> _view;
    private Mock<NortheastMegabuck.Bowlers.Search.IAdapter> _adapter;

    private NortheastMegabuck.Bowlers.Search.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Bowlers.Search.IView>();
        _adapter = new Mock<NortheastMegabuck.Bowlers.Search.IAdapter>();

        _presenter = new NortheastMegabuck.Bowlers.Search.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterExecute_CalledCorrectly()
    {
        var searchCritiera = new NortheastMegabuck.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(searchCritiera), Times.Once);
    }

    [Test]
    public void Execute_AdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        var searchCritiera = new NortheastMegabuck.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.BindResults(It.IsAny<IEnumerable<NortheastMegabuck.Bowlers.Search.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterHasNoError_NoResults_NoResultsFlow()
    {
        var searchCritiera = new NortheastMegabuck.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("No Results"), Times.Once);

            _view.Verify(view => view.BindResults(It.IsAny<IEnumerable<NortheastMegabuck.Bowlers.Search.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterHasNoError_ViewBindResults_CalledSorted()
    {
        var bowler1 = new Mock<NortheastMegabuck.Bowlers.Search.IViewModel>();
        bowler1.SetupGet(bowler => bowler.LastName).Returns("Smith");
        bowler1.SetupGet(bowler => bowler.FirstName).Returns("John");
        bowler1.SetupGet(bowler => bowler.Id).Returns(BowlerId.New());

        var bowler2 = new Mock<NortheastMegabuck.Bowlers.Search.IViewModel>();
        bowler2.SetupGet(bowler => bowler.LastName).Returns("Doe");
        bowler2.SetupGet(bowler => bowler.FirstName).Returns("John");
        bowler2.SetupGet(bowler => bowler.Id).Returns(BowlerId.New());

        var bowler3 = new Mock<NortheastMegabuck.Bowlers.Search.IViewModel>();
        bowler3.SetupGet(bowler => bowler.LastName).Returns("Doe");
        bowler3.SetupGet(bowler => bowler.FirstName).Returns("Jane");
        bowler3.SetupGet(bowler => bowler.Id).Returns(BowlerId.New());

        var bowlers = new[] { bowler1.Object, bowler2.Object, bowler3.Object };

        _adapter.Setup(adapter => adapter.Execute(It.IsAny<NortheastMegabuck.Models.BowlerSearchCriteria>())).Returns(bowlers);

        var searchCritiera = new NortheastMegabuck.Models.BowlerSearchCriteria();
        _view.SetupGet(view => view.SearchCriteria).Returns(searchCritiera);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.Is<IEnumerable<NortheastMegabuck.Bowlers.Search.IViewModel>>(results => results.Count() == 3)), Times.Once);

            _view.Verify(view => view.BindResults(It.Is<IEnumerable<NortheastMegabuck.Bowlers.Search.IViewModel>>(results => results.ToList()[0].Id == bowler3.Object.Id)), Times.Once);
            _view.Verify(view => view.BindResults(It.Is<IEnumerable<NortheastMegabuck.Bowlers.Search.IViewModel>>(results => results.ToList()[1].Id == bowler2.Object.Id)), Times.Once);
            _view.Verify(view => view.BindResults(It.Is<IEnumerable<NortheastMegabuck.Bowlers.Search.IViewModel>>(results => results.ToList()[2].Id == bowler1.Object.Id)), Times.Once);
        });
    }
}
