
using System.Security.Cryptography.Xml;

namespace NortheastMegabuck.Tests.Tournaments.Results;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Tournaments.Results.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Tournaments.Results.IAdapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Tournaments.Results.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Tournaments.Results.Adapter(_businessLogic.Object);
    }

    [Test]
    public void AtLarge_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();

        _adapter.AtLarge(tournamentId);

        _businessLogic.Verify(businessLogic=> businessLogic.Execute(tournamentId), Times.Once);
    }

    [Test]
    public void AtLarge_BusinessLogicHasError_ErrorMapped()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic => businessLogic.Error).Returns(error);

        var result = _adapter.AtLarge(TournamentId.New());

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);
            Assert.That(_adapter.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public void AtLarge_NoError_ResultsMapped()
    {
        var previousCasherId = BowlerId.New();

        var division1Result = new NortheastMegabuck.Models.AtLargeResults
        {
            DivisionId = DivisionId.New(),
            AdvancingScores = new[]
            {
                new NortheastMegabuck.Models.BowlerSquadScore(200),
                new NortheastMegabuck.Models.BowlerSquadScore(199)
            }
        };

        var division2Result = new NortheastMegabuck.Models.AtLargeResults
        {
            DivisionId = DivisionId.New(),
            AdvancingScores = new[]
            {
                new NortheastMegabuck.Models.BowlerSquadScore(198),
                new NortheastMegabuck.Models.BowlerSquadScore(previousCasherId, 197),
                new NortheastMegabuck.Models.BowlerSquadScore(196)
            },
            AdvancersWhoPreviouslyCashed = new[] { previousCasherId }
        };

        var tournamentResults = new[]
        {
            new NortheastMegabuck.Models.TournamentResults { AtLarge = division1Result },
            new NortheastMegabuck.Models.TournamentResults {AtLarge = division2Result }
         };

        _businessLogic.Setup(businessLogic => businessLogic.Execute(It.IsAny<TournamentId>())).Returns(tournamentResults);

        var results = _adapter.AtLarge(TournamentId.New()).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(results, Has.Count.EqualTo(5));

            Assert.That(results.Count(result => result.Place == 1), Is.EqualTo(2));
            Assert.That(results.Count(result => result.Place == 2), Is.EqualTo(2));
            Assert.That(results.Count(result => result.Place == 3), Is.EqualTo(1));

            Assert.That(results.Count(result => result.PreviousCasher), Is.EqualTo(1));
        });
    }
}
