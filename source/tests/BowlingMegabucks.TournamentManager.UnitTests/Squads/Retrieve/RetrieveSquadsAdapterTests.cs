namespace BowlingMegabucks.TournamentManager.UnitTests.Squads.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Squads.Retrieve.IBusinessLogic> _businessLogic;

    private TournamentManager.Squads.Retrieve.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Squads.Retrieve.IBusinessLogic>();

        _adapter = new TournamentManager.Squads.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var tournamentId = TournamentId.New();

        await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_ReturnsSquadsFromBusinessLogic()
    {
        var squad1 = new TournamentManager.Models.Squad { MaxPerPair = 1 };
        var squad2 = new TournamentManager.Models.Squad { MaxPerPair = 2 };
        var squads = new[] { squad1, squad2 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squads);

        var tournamentId = TournamentId.New();

        var actual = (await _adapter.ExecuteAsync(tournamentId, default).ConfigureAwait(false)).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(actual[0].MaxPerPair, Is.EqualTo(squad1.MaxPerPair));
            Assert.That(actual[1].MaxPerPair, Is.EqualTo(squad2.MaxPerPair));
        });
    }

    [Test]
    public async Task ExecuteAsync_SquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(squadId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ErrorsSetToBusinessLogicErrors([Range(0, 1)] int errorCount)
    {
        var error = Enumerable.Repeat(new TournamentManager.Models.ErrorDetail("test"), errorCount).SingleOrDefault();
        _businessLogic.SetupGet(businessLogic => businessLogic.ErrorDetail).Returns(error);

        var squadId = SquadId.New();

        await _adapter.ExecuteAsync(squadId, default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(error));
    }

    [Test]
    public async Task ExecuteAsync_SquadId_ReturnsSquadsFromBusinessLogic()
    {
        var squad = new TournamentManager.Models.Squad { MaxPerPair = 1 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(squad);

        var squadId = SquadId.New();

        var actual = await _adapter.ExecuteAsync(squadId, default).ConfigureAwait(false);

        Assert.That(actual.MaxPerPair, Is.EqualTo(squad.MaxPerPair));
    }
}
