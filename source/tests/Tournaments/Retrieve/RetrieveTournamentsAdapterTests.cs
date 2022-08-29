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
    public void Execute_BusinessLogicExecute_Called()
    {
        _adapter.Execute();

        _businessLogic.Verify(businessLogic => businessLogic.Execute(), Times.Once);
    }

    [Test]
    public void Execute_BusinessLogicErrorDetailNull_AdapterErrorDetailNull()
    {
        _businessLogic.Setup(businessLogic => businessLogic.Error).Returns((NortheastMegabuck.Models.ErrorDetail)null);

        _adapter.Execute();

        Assert.That(_adapter.Error, Is.Null);
    }

    [Test]
    public void Execute_BusinessLogicErrorDetailNotNull_AdapterErrorDetailEqualToBusinessLogicErrorDetail()
    {
        var errorDetail = new NortheastMegabuck.Models.ErrorDetail("message");
        _businessLogic.Setup(businessLogic => businessLogic.Error).Returns(errorDetail);

        _adapter.Execute();

        Assert.That(_adapter.Error, Is.EqualTo(errorDetail));
    }

    [Test]
    public void Execute_ReturnsBusinessLogicResponse()
    {
        var tournament1 = new NortheastMegabuck.Models.Tournament { EntryFee = 1 };
        var tournament2 = new NortheastMegabuck.Models.Tournament { EntryFee = 2 };
        var tournament3 = new NortheastMegabuck.Models.Tournament { EntryFee = 3 };

        var tournaments = new[] { tournament1, tournament2, tournament3 };

        _businessLogic.Setup(businessLogic => businessLogic.Execute()).Returns(tournaments);

        var actual = _adapter.Execute();

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
}
