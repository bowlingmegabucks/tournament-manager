namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic> _businessLogic;

    private BowlingMegabucks.TournamentManager.Tournaments.Retrieve.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<BowlingMegabucks.TournamentManager.Tournaments.Retrieve.IBusinessLogic>();

        _adapter = new BowlingMegabucks.TournamentManager.Tournaments.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_Called()
    {
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicErrorDetailNull_AdapterErrorDetailNull()
    {
        _businessLogic.Setup(businessLogic => businessLogic.ErrorDetail).Returns((BowlingMegabucks.TournamentManager.Models.ErrorDetail)null);

        await _adapter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicErrorDetailNotNull_AdapterErrorDetailEqualToBusinessLogicErrorDetail()
    {
        var errorDetail = new BowlingMegabucks.TournamentManager.Models.ErrorDetail("message");
        _businessLogic.Setup(businessLogic => businessLogic.ErrorDetail).Returns(errorDetail);

        await _adapter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.EqualTo(errorDetail));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBusinessLogicResponse()
    {
        var tournament1 = new BowlingMegabucks.TournamentManager.Models.Tournament { EntryFee = 1 };
        var tournament2 = new BowlingMegabucks.TournamentManager.Models.Tournament { EntryFee = 2 };
        var tournament3 = new BowlingMegabucks.TournamentManager.Models.Tournament { EntryFee = 3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tournaments);

        var actual = await _adapter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.TypeOf<List<BowlingMegabucks.TournamentManager.Tournaments.ViewModel>>());
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.EntryFee == 1), Is.True, "tournament1 Missing");
            Assert.That(actual.Any(tournament => tournament.EntryFee == 2), Is.True, "tournament2 Missing");
            Assert.That(actual.Any(tournament => tournament.EntryFee == 3), Is.True, "tournament3 Missing");
        });
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
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        var result = await _adapter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_TournamentId_BusinessLogicExecuteReturnsTournament_TournamentReturned()
    {
        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament { Id = TournamentId.New() };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var result = await _adapter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.That(result.Id, Is.EqualTo(tournament.Id));
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
    public async Task ExecuteAsync_SquadIdId_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        var result = await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_SquadId_BusinessLogicExecuteReturnsSquad_SquadReturned()
    {
        var tournament = new BowlingMegabucks.TournamentManager.Models.Tournament { Id = TournamentId.New() };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var result = await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(result.Id, Is.EqualTo(tournament.Id));
    }
}
