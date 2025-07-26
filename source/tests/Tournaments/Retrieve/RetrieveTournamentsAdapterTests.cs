using BowlingMegabucks.TournamentManager.Tournaments.GetTournaments;
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Tests.Tournaments.Retrieve;

[TestFixture]
internal sealed class Adapter
{
    private Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic> _businessLogic;
    private Mock<Abstractions.Messaging.IQueryHandler<GetTournamentsQuery, IEnumerable<TournamentManager.Models.Tournament>>> _queryHandler;

    private TournamentManager.Tournaments.Retrieve.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<TournamentManager.Tournaments.Retrieve.IBusinessLogic>();
        _queryHandler = new Mock<Abstractions.Messaging.IQueryHandler<GetTournamentsQuery, IEnumerable<TournamentManager.Models.Tournament>>>();

        _adapter = new TournamentManager.Tournaments.Retrieve.Adapter(_businessLogic.Object, _queryHandler.Object);
    }

    [Test]
    public async Task ExecuteAsync_QueryHandlerHandleAsync_Called()
    {
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        _queryHandler.Verify(queryHandler => queryHandler.HandleAsync(It.IsAny<GetTournamentsQuery>(), cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_NoErrorFromQueryHandler_AdapterErrorDetailNull()
    {
        _queryHandler.Setup(queryHandler => queryHandler.HandleAsync(It.IsAny<GetTournamentsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<TournamentManager.Models.Tournament>());

        await _adapter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(_adapter.Error, Is.Null);
    }

    [Test]
    public async Task ExecuteAsync_QueryHandlerReturnsError_AdapterErrorDetailEqualToQueryHandlerError()
    {
        _queryHandler.Setup(queryHandler => queryHandler.HandleAsync(It.IsAny<GetTournamentsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Error.Validation(description: "Test Error"));

        await _adapter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.That(_adapter.Error.Message, Is.EqualTo("Test Error"));
        Assert.That(_adapter.Error.ReturnCode, Is.EqualTo(-1));
    }

    [Test]
    public async Task ExecuteAsync_ReturnsQueryHandlerResponse()
    {
        var tournament1 = new TournamentManager.Models.Tournament { EntryFee = 1 };
        var tournament2 = new TournamentManager.Models.Tournament { EntryFee = 2 };
        var tournament3 = new TournamentManager.Models.Tournament { EntryFee = 3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _queryHandler.Setup(queryHandler => queryHandler.HandleAsync(It.IsAny<GetTournamentsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tournaments);

        var actual = await _adapter.ExecuteAsync(default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.TypeOf<List<TournamentManager.Tournaments.ViewModel>>());
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
        var tournament = new TournamentManager.Models.Tournament { Id = TournamentId.New() };
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
        var tournament = new TournamentManager.Models.Tournament { Id = TournamentId.New() };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<SquadId>(), It.IsAny<CancellationToken>())).ReturnsAsync(tournament);

        var result = await _adapter.ExecuteAsync(SquadId.New(), default).ConfigureAwait(false);

        Assert.That(result.Id, Is.EqualTo(tournament.Id));
    }
}
