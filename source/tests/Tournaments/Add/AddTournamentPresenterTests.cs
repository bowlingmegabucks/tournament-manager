namespace NortheastMegabuck.Tests.Tournaments.Add;

[TestFixture]
internal class Presenter
{
    private Mock<NortheastMegabuck.Tournaments.Add.IView> _view;
    private Mock<NortheastMegabuck.Tournaments.Add.IAdapter> _adapter;

    private NortheastMegabuck.Tournaments.Add.Presenter _presenter;

    [SetUp]
    public void SetUp()
    {
        _view = new Mock<NortheastMegabuck.Tournaments.Add.IView>();
        _adapter = new Mock<NortheastMegabuck.Tournaments.Add.IAdapter>();

        _presenter = new NortheastMegabuck.Tournaments.Add.Presenter(_view.Object, _adapter.Object);
    }

    [Test]
    public void Execute_ViewIsValid_Called()
    {
        _presenter.Execute();

        _view.Verify(view => view.IsValid(), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidFalse_InvalidViewFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(false);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);

            _adapter.Verify(adapter => adapter.Execute(It.IsAny<NortheastMegabuck.Tournaments.IViewModel>()), Times.Never);

            _view.Verify(view => view.DisplayErrors(It.IsAny<IEnumerable<string>>()), Times.Never);
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.VerifySet(view => view.Tournament.Id = It.IsAny<TournamentId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_AdapterExecute_CalledCorrectly()
    {
        var viewModel = new NortheastMegabuck.Tournaments.ViewModel();
        _view.SetupGet(view => view.Tournament).Returns(viewModel);

        _adapter.Setup(adapter => adapter.Execute(It.IsAny<NortheastMegabuck.Tournaments.IViewModel>())).Returns(TournamentId.Empty);

        _view.Setup(view => view.IsValid()).Returns(true);

        _presenter.Execute();

        _adapter.Verify(adapter => adapter.Execute(viewModel), Times.Once);
    }

    [Test]
    public void Execute_ViewIsValidTrue_AdapterHasErrors_AdapterErrorFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("message"), 3);
        _adapter.SetupGet(adapter => adapter.Errors).Returns(errors);

        _presenter.Execute();

        Assert.Multiple(()=>
        {
            _view.Verify(view => view.KeepOpen(), Times.Once);
            _view.Verify(view => view.DisplayErrors(new[] { "message", "message", "message"}), Times.Once);
            
            _view.Verify(view => view.DisplayMessage(It.IsAny<string>()), Times.Never);
            _view.VerifySet(view => view.Tournament.Id = It.IsAny<TournamentId>(), Times.Never);
            _view.Verify(view => view.Close(), Times.Never);
        });
    }

    [Test]
    public void Execute_ViewIsValidTrue_AdapterHasNoErrors_SuccessFlow()
    {
        _view.Setup(view => view.IsValid()).Returns(true);

        var id = TournamentId.New();
        _adapter.Setup(adapter => adapter.Execute(It.IsAny<NortheastMegabuck.Tournaments.IViewModel>())).Returns(id);

        var viewModel = new NortheastMegabuck.Tournaments.ViewModel
        { 
            TournamentName = "name"
        };

        _view.SetupGet(view => view.Tournament).Returns(viewModel);

        _presenter.Execute();

        Assert.Multiple(() =>
        {
            _view.Verify(view => view.DisplayMessage("name successfully added"), Times.Once);
            Assert.That(viewModel.Id, Is.EqualTo(id));
            _view.Verify(view => view.Close(), Times.Once);

            _view.Verify(view => view.KeepOpen(), Times.Never);
            _view.Verify(view => view.DisplayErrors(It.IsAny<IEnumerable<string>>()), Times.Never);
        });
    }
}
