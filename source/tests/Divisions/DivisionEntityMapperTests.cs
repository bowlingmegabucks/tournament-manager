
namespace NewEnglandClassic.Tests.Divisions;

[TestFixture]
internal class EntityMapper
{
    private NewEnglandClassic.Divisions.IEntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new NewEnglandClassic.Divisions.EntityMapper();

    [Test]
    public void Id_Mapped()
    {
        var id = DivisionId.New();
        var model = new NewEnglandClassic.Models.Division
        {
            Id = id
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Id, Is.EqualTo(id));
    }
    
    [Test]
    public void Number_Mapped()
    {
        short number = 123;
        var model = new NewEnglandClassic.Models.Division
        {
            Number = number
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Number, Is.EqualTo(number));
    }

    [Test]
    public void Name_Mapped()
    {
        var name = "Test Division";
        var model = new NewEnglandClassic.Models.Division
        {
            Name = name
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Name, Is.EqualTo(name));
    }

    [Test]
    public void TournamentId_Mapped()
    {
        var id = Guid.NewGuid();
        var model = new NewEnglandClassic.Models.Division
        {
            TournamentId = id
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.TournamentId, Is.EqualTo(id));
    }

    [Test]
    public void MinimumAge_Mapped([Values(null, 50)] short? minimumAge)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            MinimumAge = minimumAge
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MinimumAge, Is.EqualTo(minimumAge));
    }

    [Test]
    public void MaximumAge_Mapped([Values(null, 50)] short? maximumAge)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            MaximumAge = maximumAge
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MaximumAge, Is.EqualTo(maximumAge));
    }

    [Test]
    public void MinimumAverage_Mapped([Values(null, 200)] int? minimumAverage)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            MinimumAverage = minimumAverage
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MinimumAverage, Is.EqualTo(minimumAverage));
    }

    [Test]
    public void MaximumAverage_Mapped([Values(null, 200)] int? maximumAverage)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            MaximumAverage = maximumAverage
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MaximumAverage, Is.EqualTo(maximumAverage));
    }

    [Test]
    public void HandicapPercentage_Mapped([Values(null, .5, 1)] decimal? handicapPercentage)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            HandicapPercentage = handicapPercentage
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.HandicapPercentage, Is.EqualTo(handicapPercentage));
    }

    [Test]
    public void HandicapBase_Mapped([Values(null, 200)] int? handicapBase)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            HandicapBase = handicapBase
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.HandicapBase, Is.EqualTo(handicapBase));
    }

    [Test]
    public void MaximumHandicapPerGame_Mapped([Values(null, 0, 50)] int? maximumHandicap)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            MaximumHandicapPerGame = maximumHandicap
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.MaximumHandicapPerGame, Is.EqualTo(maximumHandicap));
    }

    [Test]
    public void Gender_Mapped([Values] NewEnglandClassic.Models.Gender gender)
    {
        var model = new NewEnglandClassic.Models.Division
        {
            Gender = gender
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Gender, Is.EqualTo(gender));
    }

    [Test]
    public void Gender_Mapped()
    {
        var model = new NewEnglandClassic.Models.Division
        {
            Gender = null
        };

        var entity = _mapper.Execute(model);

        Assert.That(entity.Gender, Is.Null);
    }
}
