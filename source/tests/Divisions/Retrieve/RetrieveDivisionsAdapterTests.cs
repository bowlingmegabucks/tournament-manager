namespace NortheastMegabuck.Tests.Divisions.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Divisions.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Divisions.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Divisions.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Divisions.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var tournamentId = TournamentId.New();

        await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsDivisionsFromBusinessLogic()
    {
        var division1 = new NortheastMegabuck.Models.Division { Name = "Division 1" };
        var division2 = new NortheastMegabuck.Models.Division { Name = "Division 2" };
        var divisions = new[] { division1, division2 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(divisions);

        var tournamentId = TournamentId.New();

        var actual = (await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].DivisionName, Is.EqualTo(division1.Name));
            Assert.That(actual[1].DivisionName, Is.EqualTo(division2.Name));
        });
    }
}
