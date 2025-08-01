﻿namespace BowlingMegabucks.TournamentManager.UnitTests.Divisions;

[TestFixture]
internal sealed class ViewModel
{
    [Test]
    public void Constructor_Model_IdMapped()
    {
        var model = new TournamentManager.Models.Division();

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.Id, Is.EqualTo(model.Id));
    }

    [Test]
    public void Constructor_Model_NumberMapped()
    {
        var model = new TournamentManager.Models.Division
        {
            Number = 1
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.Number, Is.EqualTo(model.Number));
    }

    [Test]
    public void Constructor_Model_DivisionNameMapped()
    {
        var model = new TournamentManager.Models.Division
        {
            Name = "Test Division"
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.DivisionName, Is.EqualTo(model.Name));
    }

    [Test]
    public void Constructor_Model_TournamentIdMapped()
    {
        var model = new TournamentManager.Models.Division
        {
            TournamentId = TournamentId.New()
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.TournamentId, Is.EqualTo(model.TournamentId));
    }

    [Test]
    public void Constructor_Model_MinimumAgeMapped([Values(null, 5)] short? minimumAge)
    {
        var model = new TournamentManager.Models.Division
        {
            MinimumAge = minimumAge
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.MinimumAge, Is.EqualTo(model.MinimumAge));
    }

    [Test]
    public void Constructor_Model_MaximumAgeMapped([Values(null, 5)] short? maximumAge)
    {
        var model = new TournamentManager.Models.Division
        {
            MaximumAge = maximumAge
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.MaximumAge, Is.EqualTo(model.MaximumAge));
    }

    [Test]
    public void Constructor_Model_MinimumAverageMapped([Values(null, 200)] int? minimumAverage)
    {
        var model = new TournamentManager.Models.Division
        {
            MinimumAverage = minimumAverage
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.MinimumAverage, Is.EqualTo(model.MinimumAverage));
    }

    [Test]
    public void Constructor_Model_MaximumAverageMapped([Values(null, 200)] int? maximumAverage)
    {
        var model = new TournamentManager.Models.Division
        {
            MaximumAverage = maximumAverage
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.MaximumAverage, Is.EqualTo(model.MaximumAverage));
    }

    [Test]
    public void Constructor_Model_HandicapPercentageMapped([Values(null, .7, 1)] decimal? handicapPercentage)
    {
        var model = new TournamentManager.Models.Division
        {
            HandicapPercentage = handicapPercentage
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.HandicapPercentage, Is.EqualTo(model.HandicapPercentage * 100m));
    }

    [Test]
    public void Constructor_Model_HandicapBaseMapped([Values(null, 200)] int? handicapBase)
    {
        var model = new TournamentManager.Models.Division
        {
            HandicapBase = handicapBase
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.HandicapBase, Is.EqualTo(model.HandicapBase));
    }

    [Test]
    public void Constructor_Model_MaximumHandicapPerGameMapped([Values(null, 20)] int? maximumHandicapPerGame)
    {
        var model = new TournamentManager.Models.Division
        {
            MaximumHandicapPerGame = maximumHandicapPerGame
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.MaximumHandicapPerGame, Is.EqualTo(model.MaximumHandicapPerGame));
    }

    [Test]
    public void Constructor_Model_GenderMapped_Male()
    {
        var model = new TournamentManager.Models.Division
        {
            Gender = TournamentManager.Models.Gender.Male
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.Gender, Is.EqualTo(model.Gender));
    }

    [Test]
    public void Constructor_Model_GenderMapped_Female()
    {
        var model = new TournamentManager.Models.Division
        {
            Gender = TournamentManager.Models.Gender.Female
        };

        var viewModel = new TournamentManager.Divisions.ViewModel(model);

        Assert.That(viewModel.Gender, Is.EqualTo(model.Gender));
    }
}
