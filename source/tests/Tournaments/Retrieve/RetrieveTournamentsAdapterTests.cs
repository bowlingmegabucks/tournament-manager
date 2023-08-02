namespace NortheastMegabuck.Tests.Tournaments.Retrieve;

[TestFixture]
internal class Adapter
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Tournaments.Retrieve.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Tournaments.Retrieve.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_Called()
    {
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(cancellationToken);

        _businessLogic.Verify(businessLogic => businessLogic.ExecuteAsync(cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicErrorDetailNull_AdapterErrorDetailNull()
    {
        _businessLogic.Setup(businessLogic => businessLogic.Error).Returns((NortheastMegabuck.Models.ErrorDetail)null);

        await _adapter.ExecuteAsync(default);

        Assert.That(_adapter.Error, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicErrorDetailNotNull_AdapterErrorDetailEqualToBusinessLogicErrorDetail()
    {
        var errorDetail = new NortheastMegabuck.Models.ErrorDetail("message");
        _businessLogic.Setup(businessLogic => businessLogic.Error).Returns(errorDetail);

        await _adapter.ExecuteAsync(default);

        Assert.That(_adapter.Error, Is.EqualTo(errorDetail));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsBusinessLogicResponse()
    {
        var tournament1 = new NortheastMegabuck.Models.Tournament { EntryFee = 1 };
        var tournament2 = new NortheastMegabuck.Models.Tournament { EntryFee = 2 };
        var tournament3 = new NortheastMegabuck.Models.Tournament { EntryFee = 3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<CancellationToken>())).ReturnsAsync(tournaments);

        var actual = await _adapter.ExecuteAsync(default);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.TypeOf<List<NortheastMegabuck.Tournaments.ViewModel>>());
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.EntryFee == 1), Is.True, "tournament1 Missing");
            Assert.That(actual.Any(tournament => tournament.EntryFee == 2), Is.True, "tournament2 Missing");
            Assert.That(actual.Any(tournament => tournament.EntryFee == 3), Is.True, "tournament3 Missing");
        });
    }

    [Test]
    public void Execute_TournamentId_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _adapter.Execute(tournamentId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void Execute_TournamentId_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        var result = _adapter.Execute(TournamentId.New());

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_TournamentId_BusinessLogicExecuteReturnsTournament_TournamentReturned()
    {
        var tournament = new NortheastMegabuck.Models.Tournament { Id = TournamentId.New() };
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<TournamentId>())).Returns(tournament);

        var result = _adapter.Execute(TournamentId.New());

        Assert.That(result.Id, Is.EqualTo(tournament.Id));
    }

    [Test]
    public void Execute_SquadId_BusinessLogicExecute_CalledCorrectly()
    {
        var squadId = SquadId.New();

        _adapter.Execute(squadId);

        _businessLogic.Verify(businessLogic => businessLogic.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_SquadIdId_BusinessLogicExecuteReturnsNull_NullReturned()
    {
        var result = _adapter.Execute(SquadId.New());

        Assert.That(result, Is.Null);
    }

    [Test]
    public void Execute_SquadId_BusinessLogicExecuteReturnsSquad_SquadReturned()
    {
        var tournament = new NortheastMegabuck.Models.Tournament { Id = TournamentId.New() };
        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<SquadId>())).Returns(tournament);

        var result = _adapter.Execute(SquadId.New());

        Assert.That(result.Id, Is.EqualTo(tournament.Id));
    }
}
