using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests.Scores;

[TestFixture]
internal sealed class EntityMapper
{
    private NortheastMegabuck.Scores.EntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new NortheastMegabuck.Scores.EntityMapper();

    [Test]
    public void Execute_SquadIdMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            SquadId = SquadId.New()
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.SquadId, Is.EqualTo(model.SquadId));
    }

    [Test]
    public void Execute_BowlerIdMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() }
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.BowlerId, Is.EqualTo(model.Bowler.Id));
    }

    [Test]
    public void Execute_GameMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            GameNumber = 5
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Game, Is.EqualTo(model.GameNumber));
    }

    [Test]
    public void Execute_ScoreMapped()
    {
        var model = new NortheastMegabuck.Models.SquadScore
        {
            Score = 200
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Score, Is.EqualTo(model.Score));
    }
}
