
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

    [Test]
    public void DetermineSkip_LessThanOrEqualTo16_Returns0([Range(1, 16)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(0));

    [Test]
    public void DetermineSkip_Between17And24_Returns1([Range(17, 24)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(1));

    [Test]
    public void DetermineSkip_Between25And32_Returns2([Range(25, 32)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(2));

    [Test]
    public void DetermineSkip_Between33And40_Returns3([Range(33, 40)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(3));

    [Test]
    public void DetermineSkip_Between41And48_Returns4([Range(41, 48)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(4));

    [Test]
    public void DetermineSkip_Between49And56_Returns5([Range(49, 56)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(5));

    [Test]
    public void DetermineSkip_GreaterThan56_Returns6([Range(57, 72)] short lanes)
        => Assert.That(_generator.DetermineSkip(lanes), Is.EqualTo(6));
}
