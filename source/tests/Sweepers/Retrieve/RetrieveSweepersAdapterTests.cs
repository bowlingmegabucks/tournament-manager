namespace NortheastMegabuck.Tests.Sweepers.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Sweepers.Retrieve.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Sweepers.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Sweepers.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecute_TournamentId_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var tournamentId = TournamentId.New();

        await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsSweepersFromBusinessLogic()
    {
        var sweeper1 = new NortheastMegabuck.Models.Sweeper { MaxPerPair = 1 };
        var sweeper2 = new NortheastMegabuck.Models.Sweeper { MaxPerPair = 2 };
        var sweepers = new[] { sweeper1, sweeper2 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(sweepers);

        var tournamentId = TournamentId.New();

        var actual = (await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].MaxPerPair, Is.EqualTo(sweeper1.MaxPerPair));
            Assert.That(actual[1].MaxPerPair, Is.EqualTo(sweeper2.MaxPerPair));
        });
    }

    [Test]
    public void Execute_SquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _adapter.Execute(squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadId_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new NortheastMegabuck.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var squadId = SquadId.New();

        _adapter.Execute(squadId);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public void Execute_SquadId_ReturnsSquadsFromBusinessLogic()
    {
        var squad = new NortheastMegabuck.Models.Sweeper { MaxPerPair = 1 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<SquadId>())).Returns(squad);

        var squadId = SquadId.New();

        var actual = _adapter.Execute(squadId);

        Assert.That(actual.MaxPerPair, Is.EqualTo(squad.MaxPerPair));
    }
}
