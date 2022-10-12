
namespace NortheastMegabuck.Tests.Scores;

[TestFixture]
internal class ViewModel
{
    [Test]
    public void Constructor_LaneAssignmentsIViewModel_BowlerIdMapped()
    {
        var laneAssignmentViewModel = new NortheastMegabuck.LaneAssignments.ViewModel
        {
            BowlerId = BowlerId.New()
        };

        var viewModel = new NortheastMegabuck.Scores.ViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.BowlerId, Is.EqualTo(laneAssignmentViewModel.BowlerId));
    }

    [Test]
    public void Constructor_LaneAssignmentsIViewModel_LaneAssignmentMapped()
    {
        var laneAssignmentViewModel = new NortheastMegabuck.LaneAssignments.ViewModel("1A");

        var viewModel = new NortheastMegabuck.Scores.ViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.LaneAssignment, Is.EqualTo(laneAssignmentViewModel.LaneAssignment));
    }

    [Test]
    public void Constructor_LaneAssignmentsIViewModel_BowlerNameMapped()
    {
        var laneAssignmentViewModel = new NortheastMegabuck.LaneAssignments.ViewModel
        {
            BowlerName = "bowlerName"
        };

        var viewModel = new NortheastMegabuck.Scores.ViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.BowlerName, Is.EqualTo(laneAssignmentViewModel.BowlerName));
    }

    [Test]
    public void Contructor_LaneAssignmentsIViewModel_ScoresEmpty()
    {
        var laneAssignmentViewModel = new NortheastMegabuck.LaneAssignments.ViewModel();

        var viewModel = new NortheastMegabuck.Scores.ViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.Scores, Is.Empty);
    }
}
