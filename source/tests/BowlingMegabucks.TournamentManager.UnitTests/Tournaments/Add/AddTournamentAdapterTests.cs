namespace BowlingMegabucks.TournamentManager.UnitTests.Tournaments.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Tournaments.Add.IBusinessLogic> _businessLogic;

    private TournamentManager.Tournaments.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Tournaments.Add.IBusinessLogic>();

        _adapter = new TournamentManager.Tournaments.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new TournamentManager.Tournaments.ViewModel
        {
            TournamentName = "name"
        };
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(viewModel, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<TournamentManager.Models.Tournament>(tournament => tournament.Name == "name"), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new TournamentManager.Tournaments.ViewModel();

        await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBusinessLogicId()
    {
        var id = TournamentId.New();
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentManager.Models.Tournament>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var viewModel = new TournamentManager.Tournaments.ViewModel();

        var result = await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(id));
    }
}
