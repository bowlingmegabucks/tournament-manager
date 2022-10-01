
namespace NortheastMegabuck.Tests.Scores;

[TestFixture]
internal class ViewModel
{
    private Mock<NortheastMegabuck.LaneAssignments.IViewModel> _laneAssignment;

    [OneTimeSetUp]
    public void SetUp()
    {
        _laneAssignment = new Mock<NortheastMegabuck.LaneAssignments.IViewModel>();

        _laneAssignment.SetupGet(laneAssignment => laneAssignment.BowlerId).Returns(BowlerId.New());
        _laneAssignment.SetupGet(laneAssignment => laneAssignment.LaneAssignment).Returns("2C");
        _laneAssignment.SetupGet(laneAssignment => laneAssignment.BowlerName).Returns("bowlerName");
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_BowlerIdMapped()
    {
        var model = new NortheastMegabuck.Scores.ViewModel(_laneAssignment.Object);

        Assert.That(model.BowlerId, Is.EqualTo(_laneAssignment.Object.BowlerId));
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_LaneAssignmentMapped()
    {
        var model = new NortheastMegabuck.Scores.ViewModel(_laneAssignment.Object);

        Assert.That(model.LaneAssignment, Is.EqualTo(_laneAssignment.Object.LaneAssignment));
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_BowlerNameMapped()
    {
        var model = new NortheastMegabuck.Scores.ViewModel(_laneAssignment.Object);

        Assert.That(model.BowlerName, Is.EqualTo(_laneAssignment.Object.BowlerName));
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_ScoresEmpty()
    {
        var model = new NortheastMegabuck.Scores.ViewModel(_laneAssignment.Object);

        Assert.That(model.Scores, Is.Empty);
    }
}
