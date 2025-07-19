using BowlingMegabucks.TournamentManager.Squads;

namespace BowlingMegabucks.TournamentManager.Tests.Squads;
internal sealed class HandicapCalculator
{
    private IHandicapCalculatorInternal _calculator;

    [OneTimeSetUp]
    public void SetUp()
        => _calculator = new BowlingMegabucks.TournamentManager.Squads.HandicapCalculator();

    [Test]
    public void Calculate_Entity_Handicap_NoAverage_HandicapZero()
    {
        var registration = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .8m,
                MaximumHandicapPerGame = 10
            },
            Average = null
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Entity_Handicap_NoHandicapInfoOnDivision_HandicapZero()
    {
        var registration = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division
            {
                HandicapBase = null,
                HandicapPercentage = null,
                MaximumHandicapPerGame = null
            },
            Average = 200
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Entity_Handicap_AverageGreaterThanOrEqualToBase_HandicapZero([Values(215, 216)] int average)
    {
        var registration = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .8m,
                MaximumHandicapPerGame = 10
            },
            Average = average
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Entity_Handicap_HandicapUnderMaxPerGame_HandicapMapped()
    {
        var registration = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .7m,
                MaximumHandicapPerGame = 20
            },
            Average = 200
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(10));
    }

    [Test]
    public void Calculate_Entity_Handicap_HandicapOverMaxPerGame_HandicapMapped()
    {
        var registration = new BowlingMegabucks.TournamentManager.Database.Entities.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Database.Entities.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Database.Entities.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .7m,
                MaximumHandicapPerGame = 5
            },
            Average = 200
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(5));
    }

    [Test]
    public void Calculate_Model_Handicap_NoAverage_HandicapZero()
    {
        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Models.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .8m,
                MaximumHandicapPerGame = 10
            },
            Average = null
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Model_Handicap_NoHandicapInfoOnDivision_HandicapZero()
    {
        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Models.Division
            {
                HandicapBase = null,
                HandicapPercentage = null,
                MaximumHandicapPerGame = null
            },
            Average = 200
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Model_Handicap_AverageGreaterThanOrEqualToBase_HandicapZero([Values(215, 216)] int average)
    {
        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Models.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .8m,
                MaximumHandicapPerGame = 10
            },
            Average = average
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(0));
    }

    [Test]
    public void Calculate_Model_Handicap_HandicapUnderMaxPerGame_HandicapMapped()
    {
        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Models.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .7m,
                MaximumHandicapPerGame = 20
            },
            Average = 200
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(10));
    }

    [Test]
    public void Calculate_Model_Handicap_HandicapOverMaxPerGame_HandicapMapped()
    {
        var registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler(),
            Division = new BowlingMegabucks.TournamentManager.Models.Division
            {
                HandicapBase = 215,
                HandicapPercentage = .7m,
                MaximumHandicapPerGame = 5
            },
            Average = 200
        };

        var handicap = _calculator.Calculate(registration);

        Assert.That(handicap, Is.EqualTo(5));
    }
}
