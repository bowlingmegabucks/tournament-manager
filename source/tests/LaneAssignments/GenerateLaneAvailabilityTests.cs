namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal sealed class LaneAvailability
{
    private NortheastMegabuck.LaneAssignments.ILaneAvailability _laneAvailability;

    [OneTimeSetUp]
    public void SetUp()
        => _laneAvailability = new NortheastMegabuck.LaneAssignments.LaneAvailability();

    [Test]
    public void StartingLaneEven_ThrowsInvalidOperationException()
    {
        var startingLane = 2;
        var numberOfLanes = 4;
        var maxPerPair = 2;

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair));
            Assert.That(ex.Message, Is.EqualTo("Starting lane must be odd"));
        });
    }

    [Test]
    public void NumberOfLanesOdd_ThrowsInvalidOperationException()
    {
        var startingLane = 1;
        var numberOfLanes = 5;
        var maxPerPair = 2;

        Assert.Multiple(() =>
        {
            var ex = Assert.Throws<InvalidOperationException>(() => _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair));
            Assert.That(ex.Message, Is.EqualTo("Number of lanes must be even"));
        });
    }

    [Test]
    public void StartingLaneOne_FourLanes_TwoPerPair_CorrectLaneAssignments()
    {
        var startingLane = 1;
        var numberOfLanes = 4;
        var maxPerPair = 2;

        var expected = new[] { "1A", "2B", "3A", "4B" };
        var actual = _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void StartingLaneOne_FourLanes_ThreePerPair_CorrectLaneAssignments()
    {
        var startingLane = 1;
        var numberOfLanes = 4;
        var maxPerPair = 3;

        var expected = new[] { "1A", "2B", "2C", "3A", "4B", "4C" };
        var actual = _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void StartingLaneOne_FourLanes_FourPerPair_CorrectLaneAssignments()
    {
        var startingLane = 1;
        var numberOfLanes = 4;
        var maxPerPair = 4;

        var expected = new[] { "1A", "1B", "2C", "2D", "3A", "3B", "4C", "4D" };
        var actual = _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void StartingLaneOne_FourLanes_FivePerPair_CorrectLaneAssignments()
    {
        var startingLane = 1;
        var numberOfLanes = 4;
        var maxPerPair = 5;

        var expected = new[] { "1A", "1B", "2C", "2D", "2E", "3A", "3B", "4C", "4D", "4E" };
        var actual = _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair);

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void StartingLaneOne_FourLanes_OneOrMoreThanFivePerPair_ThrowsArgumentOutOfRangeException([Values(1, 6, 7, 8, 9, 10)] int maxPerPair)
    {
        var startingLane = 1;
        var numberOfLanes = 4;

        Assert.Throws<ArgumentOutOfRangeException>(() => _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair));
    }

    [Test]
    public void StartingLaneThirteen_FourLanes_TwoPerPair_CorrectLaneAssignments()
    {
        var startingLane = 13;
        var numberOfLanes = 4;
        var maxPerPair = 2;

        var expected = new[] { "13A", "14B", "15A", "16B" };
        var actual = _laneAvailability.Generate(startingLane, numberOfLanes, maxPerPair);

        Assert.That(actual, Is.EqualTo(expected));
    }
}
