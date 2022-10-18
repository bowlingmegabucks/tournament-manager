
namespace NortheastMegabuck.Tests.LaneAssignments;

[TestFixture]
internal abstract class Generate
{
    private NortheastMegabuck.LaneAssignments.IGenerate _generator;

    [SetUp]
    public void SetUp() 
        => _generator = InstanciateInterface();

    protected abstract NortheastMegabuck.LaneAssignments.IGenerate InstanciateInterface();

    [Test]
    public void Execute_StartLane1_EnoughLanesToNotCircleBack_MappedCorrectly()
    {
        short startingLane = 1;
        var letter = "A";
        short games = 5;
        IList<short> lanesUsed = Enumerable.Range(1, 40).Select(x => (short)x).ToList();
        short defaultSkip = 3;

        var assignments = _generator.Execute(startingLane, letter, games, lanesUsed, defaultSkip).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(assignments[0], Is.EqualTo("1A"));
            Assert.That(assignments[1], Is.EqualTo(StartLane1_EnoughLanesToNotCircleBack_Skip3_Game2()));
            Assert.That(assignments[2], Is.EqualTo(StartLane1_EnoughLanesToNotCircleBack_Skip3_Game3()));
            Assert.That(assignments[3], Is.EqualTo(StartLane1_EnoughLanesToNotCircleBack_Skip3_Game4()));
            Assert.That(assignments[4], Is.EqualTo(StartLane1_EnoughLanesToNotCircleBack_Skip3_Game5()));
        });
    }

    protected abstract string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game2();
    protected abstract string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game3();
    protected abstract string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game4();
    protected abstract string StartLane1_EnoughLanesToNotCircleBack_Skip3_Game5();

    [Test]
    public void Execute_StartLane35_CircleBack_MappedCorrectly()
    {
        short startingLane = 35;
        var letter = "A";
        short games = 5;
        IList<short> lanesUsed = Enumerable.Range(1, 40).Select(x => (short)x).ToList();
        short defaultSkip = 3;

        var assignments = _generator.Execute(startingLane, letter, games, lanesUsed, defaultSkip).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(assignments[0], Is.EqualTo("35A"));
            Assert.That(assignments[1], Is.EqualTo(StartLane35_CircleBack_Skip3_Game2()));
            Assert.That(assignments[2], Is.EqualTo(StartLane35_CircleBack_Skip3_Game3()));
            Assert.That(assignments[3], Is.EqualTo(StartLane35_CircleBack_Skip3_Game4()));
            Assert.That(assignments[4], Is.EqualTo(StartLane35_CircleBack_Skip3_Game5()));
        });
    }

    protected abstract string StartLane35_CircleBack_Skip3_Game2();
    protected abstract string StartLane35_CircleBack_Skip3_Game3();
    protected abstract string StartLane35_CircleBack_Skip3_Game4();
    protected abstract string StartLane35_CircleBack_Skip3_Game5();
}
