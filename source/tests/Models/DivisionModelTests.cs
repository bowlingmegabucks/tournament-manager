using NortheastMegabuck.Divisions;

namespace NortheastMegabuck.Tests.Models;

[TestFixture]
internal sealed class Division
{
    [Test]
    public void Constructor_ViewModel_IdMapped()
    {
        var id = NortheastMegabuck.DivisionId.New();

        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(id);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_ViewModel_NumberMapped()
    {
        short number = 5;

        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Number).Returns(number);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Number, Is.EqualTo(number));
    }

    [Test]
    public void Constructor_ViewModel_NameMapped()
    {
        var name = "Division Name";

        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.DivisionName).Returns(name);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Name, Is.EqualTo(name));
    }

    [Test]
    public void Constructor_ViewModel_TournamentIdMapped()
    {
        var tournamentId = TournamentId.New();

        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.TournamentId).Returns(tournamentId);

        var model = viewModel.Object.ToModel();

        Assert.That(model.TournamentId, Is.EqualTo(tournamentId));
    }

    [Test]
    public void Constructor_ViewModel_MinimumAgeMapped([Values(null, 5)] short? age)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MinimumAge).Returns(age);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MinimumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_ViewModel_MaximumAgeMapped([Values(null, 5)] short? age)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MaximumAge).Returns(age);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MaximumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_ViewModel_MinimumAverageMapped([Values(null, 200)] int? average)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MinimumAverage).Returns(average);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MinimumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_ViewModel_MaximumAverageMapped([Values(null, 200)] int? average)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MaximumAverage).Returns(average);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MaximumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_ViewModel_HandicapPercentageMapped([Values(null, .7, 1)] decimal? handicapPercentage)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.HandicapPercentage).Returns(handicapPercentage);

        var model = viewModel.Object.ToModel();

        Assert.That(model.HandicapPercentage, Is.EqualTo(handicapPercentage / 100m));
    }

    [Test]
    public void Constructor_ViewModel_HandicapBaseMapped([Values(null, 200)] int? handicapBase)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.HandicapBase).Returns(handicapBase);

        var model = viewModel.Object.ToModel();

        Assert.That(model.HandicapBase, Is.EqualTo(handicapBase));
    }

    [Test]
    public void Constructor_ViewModel_MaximumHandicapPerGameMapped([Values(null, 50)] int? maximumHandicapPerGame)
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.MaximumHandicapPerGame).Returns(maximumHandicapPerGame);

        var model = viewModel.Object.ToModel();

        Assert.That(model.MaximumHandicapPerGame, Is.EqualTo(maximumHandicapPerGame));
    }

    [Test]
    public void Constructor_ViewModel_GenderMapped_Male()
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Gender).Returns(NortheastMegabuck.Models.Gender.Male);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Gender, Is.EqualTo(NortheastMegabuck.Models.Gender.Male));
    }

    [Test]
    public void Constructor_ViewModel_GenderMapped_Female()
    {
        var viewModel = new Mock<NortheastMegabuck.Divisions.IViewModel>();
        viewModel.SetupGet(v => v.Gender).Returns(NortheastMegabuck.Models.Gender.Female);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Gender, Is.EqualTo(NortheastMegabuck.Models.Gender.Female));
    }

    [Test]
    public void Constructor_Entities_IdMapped()
    {
        var id = NortheastMegabuck.DivisionId.New();

        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            Id = id
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.Id, Is.EqualTo(id));
    }

    [Test]
    public void Constructor_Entities_NumberMapped()
    {
        short number = 5;

        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            Number = number
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.Number, Is.EqualTo(number));
    }

    [Test]
    public void Constructor_Entities_NameMapped()
    {
        var name = "Division Name";

        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            Name = name
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.Name, Is.EqualTo(name));
    }

    [Test]
    public void Constructor_Entities_TournamentIdMapped()
    {
        var tournamentId = TournamentId.New();

        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            TournamentId = tournamentId
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.TournamentId, Is.EqualTo(tournamentId));
    }

    [Test]
    public void Constructor_Entities_MinimumAgeMapped([Values(null, 5)] short? age)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            MinimumAge = age
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.MinimumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_Entities_MaximumAgeMapped([Values(null, 5)] short? age)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            MaximumAge = age
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.MaximumAge, Is.EqualTo(age));
    }

    [Test]
    public void Constructor_Entities_MinimumAverageMapped([Values(null, 200)] int? average)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            MinimumAverage = average
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.MinimumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_Entities_MaximumAverageMapped([Values(null, 200)] int? average)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            MaximumAverage = average
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.MaximumAverage, Is.EqualTo(average));
    }

    [Test]
    public void Constructor_Entities_HandicapPercentageMapped([Values(null, .7, 1)] decimal? handicapPercentage)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            HandicapPercentage = handicapPercentage
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.HandicapPercentage, Is.EqualTo(handicapPercentage));
    }

    [Test]
    public void Constructor_Entities_HandicapBaseMapped([Values(null, 200)] int? handicapBase)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            HandicapBase = handicapBase
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.HandicapBase, Is.EqualTo(handicapBase));
    }

    [Test]
    public void Constructor_Entities_MaximumHandicapPerGameMapped([Values(null, 50)] int? maximumHandicapPerGame)
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            MaximumHandicapPerGame = maximumHandicapPerGame
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.MaximumHandicapPerGame, Is.EqualTo(maximumHandicapPerGame));
    }

    [Test]
    public void Constructor_Entities_GenderMapped_Male()
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            Gender = NortheastMegabuck.Models.Gender.Male
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.Gender, Is.EqualTo(NortheastMegabuck.Models.Gender.Male));
    }

    [Test]
    public void Constructor_Entities_GenderMapped_Female()
    {
        var entity = new NortheastMegabuck.Database.Entities.Division
        {
            Gender = NortheastMegabuck.Models.Gender.Female
        };

        var model = new NortheastMegabuck.Models.Division(entity);

        Assert.That(model.Gender, Is.EqualTo(NortheastMegabuck.Models.Gender.Female));
    }

    [Test]
    public void GetHashCode_ReturnsIdHashCode()
    {
        var id = DivisionId.New();
        var division = new NortheastMegabuck.Models.Division { Id = id };

        Assert.That(division.GetHashCode(), Is.EqualTo(id.GetHashCode()));
    }

    [Test]
    public void Equals_ObjNull_ReturnsFalse()
    {
        var division = new NortheastMegabuck.Models.Division();

#pragma warning disable CA1508 // Avoid dead conditional code
        Assert.That(division.Equals(null), Is.False);
#pragma warning restore CA1508 // Avoid dead conditional code
    }

    [Test]
    public void Equals_ObjNotDivision_ReturnsFalse()
    {
        var division = new NortheastMegabuck.Models.Division();

        Assert.That(division.Equals(new NortheastMegabuck.Models.Bowler()), Is.False);
    }

    [Test]
    public void Equals_ObjDivision_IdsDoNotMatch_ReturnsFalse()
    {
        var division = new NortheastMegabuck.Models.Division();
        var other = new NortheastMegabuck.Models.Division();

        Assert.That(division.Equals(other), Is.False);
    }

    [Test]
    public void Equals_ObjDivision_IdsMatch_ReturnsTrue()
    {
        var division = new NortheastMegabuck.Models.Division();
        var other = new NortheastMegabuck.Models.Division { Id = division.Id };

        Assert.That(division.Equals(other), Is.True);
    }
}
