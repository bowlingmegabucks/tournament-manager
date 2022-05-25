using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Squad
{
    [Test]
    public void Constructor_SquadEntity_IdMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            Id = Guid.NewGuid(),
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public void Constructor_SquadEntity_TournamentIdMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            TournamentId = Guid.NewGuid(),
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.TournamentId, Is.EqualTo(entity.TournamentId));
    }

    [Test]
    public void Constructor_SquadEntity_CashRatioMapped([Values(null, 5.5)] decimal? cashRatio)
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            CashRatio = cashRatio,
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.CashRatio, Is.EqualTo(entity.CashRatio));
    }

    [Test]
    public void Constructor_SquadEntity_FinalsRatioMapped([Values(null, 4.5)] decimal? finalsRatio)
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            FinalsRatio = finalsRatio,
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.FinalsRatio, Is.EqualTo(entity.FinalsRatio));
    }

    [Test]
    public void Constructor_SquadEntity_DateMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            Date = DateTime.Now,
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.Date, Is.EqualTo(entity.Date));
    }

    [Test]
    public void Constructor_SquadEntity_MaxPerPairMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            MaxPerPair = 5,
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.MaxPerPair, Is.EqualTo(entity.MaxPerPair));
    }

    [Test]
    public void Constructor_SquadEntity_CompleteMapped([Values] bool complete)
    {
        var entity = new NewEnglandClassic.Database.Entities.TournamentSquad
        {
            Complete = complete,
            Tournament = new NewEnglandClassic.Database.Entities.Tournament()
        };

        var model = new NewEnglandClassic.Models.Squad(entity);

        Assert.That(model.Complete, Is.EqualTo(entity.Complete));
    }
}
