
namespace NortheastMegabuck.Tests.Tournaments.Seeding;

[TestFixture]
internal sealed class Adapter
{
    private Mock<NortheastMegabuck.Tournaments.Seeding.IBusinessLogic> _businessLogic;

    private NortheastMegabuck.Tournaments.Seeding.Adapter _adapter;

    [SetUp]
    public void SetUp()
    {
        _businessLogic = new Mock<NortheastMegabuck.Tournaments.Seeding.IBusinessLogic>();

        _adapter = new NortheastMegabuck.Tournaments.Seeding.Adapter(_businessLogic.Object);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicExecute_CalledCorrectly()
    {
        var tournamentId = TournamentId.New();
        CancellationToken cancellationToken = default;

        await _adapter.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        _businessLogic.Verify(businessLogic=> businessLogic.ExecuteAsync(tournamentId, cancellationToken), Times.Once);
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicHasError_ErrorFlow()
    {
        var error = new NortheastMegabuck.Models.ErrorDetail("error");
        _businessLogic.SetupGet(businessLogic=> businessLogic.Error).Returns(error);

        var result = await _adapter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Empty);

            Assert.That(_adapter.Error, Is.EqualTo(error));
        });
    }

    [Test]
    public async Task ExecuteAsync_BusinessLogicHasNoError_ReturnsSeedingByDivision()
    {
        var division1 = new NortheastMegabuck.Models.Division() { Name = "Division 1" };
        var division2 = new NortheastMegabuck.Models.Division() { Name = "Division 2" };

        var bowler1Division1 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 300) { Division = division1 };
        var bowler2Division1 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 299) { Division = division1 };
        var bowler3Division1 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 298) { Division = division1 };
        var bowler4Division1 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 255) { Division = division1 };
        var bowler5Division1 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 254) { Division = division1 };

        var bowler1Division2 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 200) { Division = division2 };
        var bowler2Division2 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 199) { Division = division2 };
        var bowler3Division2 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 198) { Division = division2 };
        var bowler4Division2 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 197) { Division = division2 };
        var bowler5Division2 = new NortheastMegabuck.Models.BowlerSquadScore(BowlerId.New(), 196) { Division = division2 };

        var division1Result = new NortheastMegabuck.Models.TournamentFinalsSeeding
        {
            Division = division1,
            Qualifiers = new[] { bowler1Division1, bowler2Division1 },
            NonQualifiers = new[] { bowler3Division1, bowler4Division1, bowler5Division1 }
        };

        var division2Result = new NortheastMegabuck.Models.TournamentFinalsSeeding
        {
            Division = division2,
            Qualifiers = new[] { bowler1Division2, bowler2Division2, bowler3Division2 },
            NonQualifiers = new[] { bowler4Division2, bowler5Division2 }
        };

        var results = new[] { division1Result, division2Result };
        _businessLogic.Setup(businessLogic => businessLogic.ExecuteAsync(It.IsAny<TournamentId>(), It.IsAny<CancellationToken>())).ReturnsAsync(results);

        var actual = await _adapter.ExecuteAsync(TournamentId.New(), default).ConfigureAwait(false);

        Assert.Multiple(() =>
        {
            var division1Seeding = actual.Where(seeding => seeding.DivisionName == "Division 1").ToList();

            Assert.That(division1Seeding.Count(seed => seed.Seed == 1 && seed.Score == 300), Is.EqualTo(1));
            Assert.That(division1Seeding.Count(seed => seed.Seed == 2 && seed.Score == 299), Is.EqualTo(1));
            Assert.That(division1Seeding.Count(seed => seed.Seed == 3 && seed.Score == 298), Is.EqualTo(1));
            Assert.That(division1Seeding.Count(seed => seed.Seed == 4 && seed.Score == 255), Is.EqualTo(1));
            Assert.That(division1Seeding.Count(seed => seed.Seed == 5 && seed.Score == 254), Is.EqualTo(1));

            var division2Seeding = actual.Where(seeding => seeding.DivisionName == "Division 2").ToList();

            Assert.That(division2Seeding.Count(seed => seed.Seed == 1 && seed.Score == 200), Is.EqualTo(1));
            Assert.That(division2Seeding.Count(seed => seed.Seed == 2 && seed.Score == 199), Is.EqualTo(1));
            Assert.That(division2Seeding.Count(seed => seed.Seed == 3 && seed.Score == 198), Is.EqualTo(1));
            Assert.That(division2Seeding.Count(seed => seed.Seed == 4 && seed.Score == 197), Is.EqualTo(1));
            Assert.That(division2Seeding.Count(seed => seed.Seed == 5 && seed.Score == 196), Is.EqualTo(1));
        });
    }
}
