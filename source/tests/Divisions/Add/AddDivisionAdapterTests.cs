namespace BowlingMegabucks.TournamentManager.Tests.Divisions.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IBusinessLogic> _businessLogic;

    private BowlingMegabucks.TournamentManager.Divisions.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<BowlingMegabucks.TournamentManager.Divisions.Add.IBusinessLogic>();

        _adapter = new BowlingMegabucks.TournamentManager.Divisions.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Divisions.ViewModel
        {
            DivisionName = "name"
        };
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(viewModel, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<BowlingMegabucks.TournamentManager.Models.Division>(tournament => tournament.Name == "name"), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new BowlingMegabucks.TournamentManager.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new BowlingMegabucks.TournamentManager.Divisions.ViewModel();

        await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(true);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBusinessLogicId()
    {
        var divisionId = BowlingMegabucks.TournamentManager.DivisionId.New();
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<BowlingMegabucks.TournamentManager.Models.Division>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisionId);

        var viewModel = new BowlingMegabucks.TournamentManager.Divisions.ViewModel();

        var result = await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(true);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
