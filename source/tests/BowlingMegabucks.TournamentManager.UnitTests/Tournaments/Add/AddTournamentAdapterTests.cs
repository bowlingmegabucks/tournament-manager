namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Add.IBusinessLogic> _businessLogic;

    private BowlingMegabucks.TournamentManager.Tournaments.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Add.IBusinessLogic>();

        _adapter = new BowlingMegabucks.TournamentManager.Tournaments.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel
        {
            TournamentName = "name"
        };
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(viewModel, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<BowlingMegabucks.TournamentManager.Models.Tournament>(tournament => tournament.Name == "name"), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel();

        await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(false);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBusinessLogicId()
    {
        var id = TournamentId.New();
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Tournament>(), It.IsAny<CancellationToken>())).ReturnsAsync(id);

        var viewModel = new BowlingMegabucks.TournamentManager.Tournaments.ViewModel();

        var result = await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(false);

        Assert.That(result, Is.EqualTo(id));
    }
}
