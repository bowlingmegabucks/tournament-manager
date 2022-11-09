using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Squads.Results;

[TestFixture]
internal class BusinessLogic
{
    private Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic> _retrieveTournament;
    private Mock<NortheastMegabuck.Squads.Results.ICalculator> _squadResultCalculator;
    private Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic> _retrieveScores;

    private NortheastMegabuck.Squads.Results.IBusinessLogic _businessLogic;

    [SetUp]
    public void SetUp()
    {
        _retrieveTournament = new Mock<NortheastMegabuck.Tournaments.Retrieve.IBusinessLogic>();
        _squadResultCalculator = new Mock<NortheastMegabuck.Squads.Results.ICalculator>();
        _retrieveScores = new Mock<NortheastMegabuck.Scores.Retrieve.IBusinessLogic>();

        _businessLogic = new NortheastMegabuck.Squads.Results.BusinessLogic(_retrieveTournament.Object, _squadResultCalculator.Object, _retrieveScores.Object);
    }

    [Test]
    public void Execute_RetrieveTournamentExecute_CalledCorrectly()
    {
        var tournament = new NortheastMegabuck.Models.Tournament();
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<SquadId>())).Returns(tournament);

        var squadId = SquadId.New();

        _businessLogic.Execute(squadId);

        _retrieveTournament.Verify(retrieveTournament=> retrieveTournament.Execute(squadId), Times.Once);
    }

    [Test]
    public void Execute_RetrieveTournamentExecuteHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _retrieveTournament.SetupGet(retrieveTournament => retrieveTournament.Error).Returns(error);

        var result = _businessLogic.Execute(SquadId.New());

        Assert.Multiple(() =>
        {
            Assert.That(_businessLogic.Error, Is.EqualTo(error));
            Assert.That(result, Is.Empty);

            _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(It.IsAny<IEnumerable<SquadId>>()), Times.Never);
            _squadResultCalculator.Verify(calculator => calculator.Execute(It.IsAny<NortheastMegabuck.Models.Squad>(), It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<List<NortheastMegabuck.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()), Times.Never);
        });
    }

    [Test]
    public void Execute_RetrieveTournamentExecuteNoError_RetrieveScoresExecute_CalledWithSquadsBeforeOrOnGivenSquadDate()
    {
        var pastSquad = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(-1)
        };

        var currentSquad = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now
        };

        var futureSquad = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(1)
        };

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            Squads = new[] { pastSquad, currentSquad, futureSquad }
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<SquadId>())).Returns(tournament);

        _businessLogic.Execute(currentSquad.Id);

        Assert.Multiple(() =>
        {
            _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(It.Is<IEnumerable<SquadId>>(squads => squads.Count() == 2)), Times.Once);

            _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(It.Is<IEnumerable<SquadId>>(squads => squads.Contains(pastSquad.Id))), Times.Once);
            _retrieveScores.Verify(retrieveScores => retrieveScores.Execute(It.Is<IEnumerable<SquadId>>(squads => squads.Contains(currentSquad.Id))), Times.Once);
        });
    }

    [Test]
    public void Execute_RetrieveTournamentExecuteNoError_RetrieveScoresExecuteNoError_SquadResultCalculatorExecute_CalledInSequentialOrderBasedOnSquadDate()
    {
        var pastSquad = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(-1)
        };

        var currentSquad = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now
        };

        var futureSquad = new NortheastMegabuck.Models.Squad
        {
            Id = SquadId.New(),
            Date = DateTime.Now.AddDays(1)
        };

        var tournament = new NortheastMegabuck.Models.Tournament
        {
            Squads = new[] { currentSquad, futureSquad, pastSquad }
        };
        _retrieveTournament.Setup(retrieveTournament => retrieveTournament.Execute(It.IsAny<SquadId>())).Returns(tournament);

        var mockResultCalculator = new Mock<NortheastMegabuck.Squads.Results.ICalculator>(MockBehavior.Strict);
        var sequence = new MockSequence();
        mockResultCalculator.InSequence(sequence).Setup(calculator => calculator.Execute(It.Is<NortheastMegabuck.Models.Squad>(squad => squad.Id == pastSquad.Id), It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<List<NortheastMegabuck.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
        mockResultCalculator.InSequence(sequence).Setup(calculator => calculator.Execute(It.Is<NortheastMegabuck.Models.Squad>(squad => squad.Id == currentSquad.Id), It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<List<NortheastMegabuck.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>()));
        mockResultCalculator.Setup(calculator => calculator.Execute(It.IsAny<NortheastMegabuck.Models.Squad>(), It.IsAny<NortheastMegabuck.Models.Division>(), It.IsAny<List<NortheastMegabuck.Models.BowlerSquadScore>>(), It.IsAny<IEnumerable<BowlerId>>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(new NortheastMegabuck.Models.SquadResult());
        _businessLogic = new NortheastMegabuck.Squads.Results.BusinessLogic(_retrieveTournament.Object, mockResultCalculator.Object, _retrieveScores.Object);

        var pastSquadBowler1Score1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
            SquadId = pastSquad.Id,
            Division = new NortheastMegabuck.Models.Division { Id = DivisionId.New() },
            GameNumber = 1,
            Score = 200
        };

        var currentSquadBowler2Score1 = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
            SquadId = currentSquad.Id,
            Division = new NortheastMegabuck.Models.Division { Id = DivisionId.New() },
            GameNumber = 1,
            Score = 200
        };

        var scores = new[] { currentSquadBowler2Score1, pastSquadBowler1Score1 };
        _retrieveScores.Setup(retrieveScores => retrieveScores.Execute(It.IsAny<IEnumerable<SquadId>>())).Returns(scores);

        _businessLogic.Execute(currentSquad.Id);

        mockResultCalculator.VerifyAll();
    }

    //next: make sure result calculator is called correctly for each squad in each division (do 2 bowlers, 2 squads, 2 divisions)
}
