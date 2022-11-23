
namespace NortheastMegabuck.Tests.Tournaments.Seeding;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Tournaments.Seeding.IView> _view;
    private Mock<NortheastMegabuck.Tournaments.Seeding.IAdapter> _adapter;

    private NortheastMegabuck.Tournaments.Seeding.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Tournaments.Seeding.IView>();
        _adapter = new Mock<NortheastMegabuck.Tournaments.Seeding.IAdapter>();

        _presenter = new NortheastMegabuck.Tournaments.Seeding.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        _view.SetupGet(view=> view.Id).Returns(tournamentId);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_AdapterHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _adapter.SetupGet(adapter => adapter.Error).Returns(error);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayError("error"), Times.Once);
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<IEnumerable<NortheastMegabuck.Tournaments.Seeding.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterHasNoError_ViewBindResults_CalledCorrectly()
    {
        var result1 = new Mock<NortheastMegabuck.Tournaments.Seeding.IViewModel>();
        result1.SetupGet(result => result.DivisionName).Returns("Division 1");
        result1.SetupGet(result => result.Score).Returns(100);

        var result2 = new Mock<NortheastMegabuck.Tournaments.Seeding.IViewModel>();
        result2.SetupGet(result => result.DivisionName).Returns("Division 1");
        result2.SetupGet(result=> result.Score).Returns(101);

        var result3 = new Mock<NortheastMegabuck.Tournaments.Seeding.IViewModel>();
        result3.SetupGet(result => result.DivisionName).Returns("Division 2");
        result3.SetupGet(result => result.Score).Returns(200);

        var result4 = new Mock<NortheastMegabuck.Tournaments.Seeding.IViewModel>();
        result4.SetupGet(result => result.DivisionName).Returns("Division 2");
        result4.SetupGet(result => result.Score).Returns(201);

        var results = new[] { result1.Object, result2.Object, result3.Object, result4.Object };
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<TournamentId>())).Returns(results);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<IEnumerable<NortheastMegabuck.Tournaments.Seeding.IViewModel>>()), Times.Exactly(2));

            _view.Verify(view => view.BindResults("Division 1", It.Is<IEnumerable<NortheastMegabuck.Tournaments.Seeding.IViewModel>>(Execute => Execute.Count(x => x.Score == 100) == 1 && Execute.Count(x => x.Score == 101) == 1)), Times.Once);
            _view.Verify(view => view.BindResults("Division 2", It.Is<IEnumerable<NortheastMegabuck.Tournaments.Seeding.IViewModel>>(Execute => Execute.Count(x => x.Score == 200) == 1 && Execute.Count(x => x.Score == 201) == 1)), Times.Once);
        });
    }
}
