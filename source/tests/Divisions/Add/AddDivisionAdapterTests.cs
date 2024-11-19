namespace NortheastMegabuck.Tests.Divisions.Add;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Divisions.Add.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Divisions.Add.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Divisions.Add.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Divisions.Add.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var viewModel = new NortheastMegabuck.Divisions.ViewModel
        {
            DivisionName = "name"
        };
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(viewModel, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(It.Is<NortheastMegabuck.Models.Division>(tournament => tournament.Name == "name"), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_Errors_SetToBusinessLogicErrors([Range(0, 2)] int count)
    {
        var errors = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("error"), count);
        _businessLogic.SetupGet(businessLogic => businessLogic.Errors).Returns(errors);

        var viewModel = new NortheastMegabuck.Divisions.ViewModel();

        await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(true);

        Assert.That(_adapter.Errors, Is.EqualTo(errors));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBusinessLogicId()
    {
        var divisionId = NortheastMegabuck.DivisionId.New();
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisionId);

        var viewModel = new NortheastMegabuck.Divisions.ViewModel();

        var result = await _adapter.ExecuteAsync(viewModel, default).ConfigureAwait(true);

        Assert.That(result, Is.EqualTo(divisionId));
    }
}
