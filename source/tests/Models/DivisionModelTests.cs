using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        Assert.That(model.HandicapPercentage, Is.EqualTo(handicapPercentage));
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
}
