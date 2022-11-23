
namespace NortheastMegabuck.Tests.Tournaments.Results;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Tournaments.Results.IView> _view;
    private Mock<NortheastMegabuck.Tournaments.Results.IAdapter> _adapter;

    private NortheastMegabuck.Tournaments.Results.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Tournaments.Results.IView>();
        _adapter = new Mock<NortheastMegabuck.Tournaments.Results.IAdapter>();

        _presenter = new NortheastMegabuck.Tournaments.Results.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void AtLarge_AdapterAtLarge_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view=> view.Id).Returns(tournamentId);

        _presenter.AtLarge();

        _adapter.Verify(adapter => adapter.AtLarge(tournamentId), Times.Once);
    }

    [Test]
    public void AtLarge_AdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.AtLarge();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<IEnumerable<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void AtLarge_AdapterHasNoError_ViewBindResults_CalledCorrectly()
    {
        var result1 = new Mock<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>();
        result1.SetupGet(result => result.DivisionName).Returns("Division 1");
        result1.SetupGet(result => result.Score).Returns(100);

        var result2 = new Mock<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>();
        result2.SetupGet(result => result.DivisionName).Returns("Division 1");
        result2.SetupGet(result=> result.Score).Returns(101);

        var result3 = new Mock<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>();
        result3.SetupGet(result => result.DivisionName).Returns("Division 2");
        result3.SetupGet(result => result.Score).Returns(200);

        var result4 = new Mock<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>();
        result4.SetupGet(result => result.DivisionName).Returns("Division 2");
        result4.SetupGet(result => result.Score).Returns(201);

        var results = new[] { result1.Object, result2.Object, result3.Object, result4.Object };
        _adapter.Setup(adapter => adapter.AtLarge(It.IsAny<TournamentId>())).Returns(results);

        _presenter.AtLarge();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<IEnumerable<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>>()), Times.Exactly(2));

            _view.Verify(view => view.BindResults("Division 1", It.Is<IEnumerable<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>>(atLarge => atLarge.Count(x => x.Score == 100) == 1 && atLarge.Count(x => x.Score == 101) == 1)), Times.Once);
            _view.Verify(view => view.BindResults("Division 2", It.Is<IEnumerable<NortheastMegabuck.Tournaments.Results.IAtLargeViewModel>>(atLarge => atLarge.Count(x => x.Score == 200) == 1 && atLarge.Count(x => x.Score == 201) == 1)), Times.Once);
        });
    }
}
