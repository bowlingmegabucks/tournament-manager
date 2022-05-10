namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Division
{
    [Test]
    public void Constructor_ViewModel_IdMapped()
    {
        var guid = Guid.NewGuid();

        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(guid);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Constructor_ViewModel_NumberMapped()
    {
        short number = 5;

        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Number).Returns(number);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.Number, Is.EqualTo(number));
    }

    [Test]
    public void Constructor_ViewModel_NameMapped()
    {
        var name = "Division Name";

        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.DivisionName).Returns(name);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.Name, Is.EqualTo(name));
    }

    [Test]
    public void Constructor_ViewModel_TournamentIdMapped()
    {
        var tournamentId = Guid.NewGuid();

        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.TournamentId).Returns(tournamentId);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.TournamentId, Is.EqualTo(tournamentId));
    }

    [Test]
    public void Constructor_ViewModel_MinumumAgeMapped([Values(null, 5)]short? age)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MinimumAge).Returns(age);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.MinimumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_ViewModel_MaximumAgeMapped([Values(null, 5)] short? age)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MaximumAge).Returns(age);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.MaximumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_ViewModel_MinimumAverageMapped([Values(null, 200)] int? average)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MinimumAverage).Returns(average);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.MinimumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_ViewModel_MaximumAverageMapped([Values(null, 200)] int? average)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MaximumAverage).Returns(average);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.MaximumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_ViewModel_HandicapPercentageMapped([Values(null, .7, 1)] decimal? handicapPercentage)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.HandicapPercentage).Returns(handicapPercentage);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.HandicapPercentage, Is.EqualTo(handicapPercentage / 100m));
    }

    [Test]
    public void Constructor_ViewModel_HandicapBaseMapped([Values(null, 200)] int? handicapBase)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.HandicapBase).Returns(handicapBase);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.HandicapBase, Is.EqualTo(handicapBase));
    }

    [Test]
    public void Constructor_ViewModel_MaximumHandicapPerGameMapped([Values(null, 50)] int? maximumHandicapPerGame)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MaximumHandicapPerGame).Returns(maximumHandicapPerGame);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.MaximumHandicapPerGame, Is.EqualTo(maximumHandicapPerGame));
    }

    [Test]
    public void Constructor_ViewModel_GenderMapped([Values] NewEnglandClassic.Models.Gender gender)
    {
        var viewModel = new Mock<NewEnglandClassic.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Gender).Returns(gender);

        var model = new NewEnglandClassic.Models.Division(viewModel.Object);

        Assert.That(model.Gender, Is.EqualTo(gender));
    }

    [Test]
    public void Constructor_Entities_IdMapped()
    {
        var guid = Guid.NewGuid();

        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            Id = guid
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.Id, Is.EqualTo(guid));
    }

    [Test]
    public void Constructor_Entities_NumberMapped()
    {
        short number = 5;

        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            Number = number
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.Number, Is.EqualTo(number));
    }

    [Test]
    public void Constructor_Entities_NameMapped()
    {
        var name = "Division Name";

        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            Name = name
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.Name, Is.EqualTo(name));
    }

    [Test]
    public void Constructor_Entities_TournamentIdMapped()
    {
        var tournamentId = Guid.NewGuid();

        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            TournamentId = tournamentId
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.TournamentId, Is.EqualTo(tournamentId));
    }

    [Test]
    public void Constructor_Entities_MinumumAgeMapped([Values(null, 5)] short? age)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            MinimumAge = age
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.MinimumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_Entities_MaximumAgeMapped([Values(null, 5)] short? age)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            MaximumAge = age
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.MaximumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_Entities_MinimumAverageMapped([Values(null, 200)] int? average)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            MinimumAverage = average
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.MinimumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_Entities_MaximumAverageMapped([Values(null, 200)] int? average)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            MaximumAverage = average
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.MaximumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_Entities_HandicapPercentageMapped([Values(null, .7, 1)] decimal? handicapPercentage)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            HandicapPercentage = handicapPercentage
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.HandicapPercentage, Is.EqualTo(handicapPercentage));
    }

    [Test]
    public void Constructor_Entities_HandicapBaseMapped([Values(null, 200)] int? handicapBase)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            HandicapBase = handicapBase
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.HandicapBase, Is.EqualTo(handicapBase));
    }

    [Test]
    public void Constructor_Entities_MaximumHandicapPerGameMapped([Values(null, 50)] int? maximumHandicapPerGame)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            MaximumHandicapPerGame = maximumHandicapPerGame
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.MaximumHandicapPerGame, Is.EqualTo(maximumHandicapPerGame));
    }

    [Test]
    public void Constructor_Entities_GenderMapped([Values] NewEnglandClassic.Models.Gender gender)
    {
        var entity = new NewEnglandClassic.Database.Entities.Division
        {
            Gender = gender
        };

        var model = new NewEnglandClassic.Models.Division(entity);

        Assert.That(model.Gender, Is.EqualTo(gender));
    }
}
