namespace BowlingMegabucks.TournamentManager.UnitTests.Scores;

[TestFixture]
internal sealed class EntityMapper
{
    private TournamentManager.Scores.EntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new TournamentManager.Scores.EntityMapper();

    [Test]
    public void Execute_SquadIdMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            SquadId = SquadId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.SquadId, Is.EqualTo(model.SquadId));
    }

    [Test]
    public void Execute_BowlerIdMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            Bowler = new TournamentManager.Models.Bowler { Id = BowlerId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.BowlerId, Is.EqualTo(model.Bowler.Id));
    }

    [Test]
    public void Execute_GameMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            GameNumber = 5
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Game, Is.EqualTo(model.GameNumber));
    }

    [Test]
    public void Execute_ScoreMapped()
    {
        var model = new TournamentManager.Models.SquadScore
        {
            Score = 200
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Score, Is.EqualTo(model.Score));
    }
}
