
namespace NortheastMegabuck.Tests.Squads.Results;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Squads.Results.IView> _view;
    private Mock<NortheastMegabuck.Squads.Results.IAdapter> _adapter;

    private NortheastMegabuck.Squads.Results.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Squads.Results.IView>();
        _adapter = new Mock<NortheastMegabuck.Squads.Results.IAdapter>();

        _presenter = new NortheastMegabuck.Squads.Results.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_AdapterExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        _view.SetupGet(view => view.SquadId).Returns(squadId);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(squadId), Times.Once);
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

            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<ICollection<NortheastMegabuck.Squads.Results.IViewModel>>()), Times.Never);
        });
    }

    [Test]
    public void Execute_AdapterHasNoError_ViewBindResults_CalledCorrectly()
    {
        var division1Score1 = new NortheastMegabuck.Squads.Results.ViewModel
        {
            DivisionName = "division1",
            BowlerName = "score1"
        };

        var division1Score2 = new NortheastMegabuck.Squads.Results.ViewModel
        {
            DivisionName = "division1",
            BowlerName = "score2"
        };

        var division2Score = new NortheastMegabuck.Squads.Results.ViewModel
        {
            DivisionName = "division2",
            BowlerName = "score"
        };

        var scores = new[] { division1Score2, division2Score, division1Score1 }.GroupBy(score => score.DivisionName);
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<SquadId>())).Returns(scores);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.BindResults(It.IsAny<string>(), It.IsAny<ICollection<NortheastMegabuck.Squads.Results.IViewModel>>()), Times.Exactly(2));

            _view.Verify(view => view.BindResults("division1", It.Is<ICollection<NortheastMegabuck.Squads.Results.IViewModel>>(score => score.Count(s => s.DivisionName == "division1") == 2)), Times.Once);
            _view.Verify(view => view.BindResults("division2", It.Is<ICollection<NortheastMegabuck.Squads.Results.IViewModel>>(score => score.Count(s => s.DivisionName == "division2") == 1)), Times.Once);
        });
    }
}
