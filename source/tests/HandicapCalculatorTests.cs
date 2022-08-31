using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NortheastMegabuck.Tests;
internal class HandicapCalculator
{
    private NortheastMegabuck.IHandicapCalculator _calculator;

    [OneTimeSetUp]
    public void SetUp()
        => _calculator = new NortheastMegabuck.HandicapCalculator();

    [Test]
    public void Calculate_Handicap_NoAverage_HandicapZero()
    {
        var registration = new NortheastMegabuck.Database.Entities.Registration
        {
            Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                Id = DivisionId.New(),
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
    public void Calculate_Handicap_NoHandicapInfoOnDivision_HandicapZero()
    {
        var registration = new NortheastMegabuck.Database.Entities.Registration
        {
            Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                Id = DivisionId.New(),
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
    public void Calculate_Handicap_AverageGreaterThanOrEqualToBase_HandicapZero([Values(215, 216)] int average)
    {
        var registration = new NortheastMegabuck.Database.Entities.Registration
        {
            Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                Id = DivisionId.New(),
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
    public void Calculate_Handicap_HandicapUnderMaxPerGame_HandicapMapped()
    {
        var registration = new NortheastMegabuck.Database.Entities.Registration
        {
            Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                Id = DivisionId.New(),
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
    public void Calculate_Handicap_HandicapOverMaxPerGame_HandicapMapped()
    {
        var registration = new NortheastMegabuck.Database.Entities.Registration
        {
            Bowler = new NortheastMegabuck.Database.Entities.Bowler(),
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                Id = DivisionId.New(),
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
