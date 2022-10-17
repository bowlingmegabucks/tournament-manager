
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

    [Test]
    public void Constructor_ClipboardString_LaneAssignmentMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new NortheastMegabuck.Scores.ViewModel(data, 4);

        Assert.That(viewModel.LaneAssignment, Is.EqualTo("1A"));
    }

    [Test]
    public void Constructor_ClipboardString_BowlerIdMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new NortheastMegabuck.Scores.ViewModel(data, 4);

        Assert.That(viewModel.BowlerId, Is.EqualTo(new BowlerId(new Guid("6c28c592-c241-401e-8414-251f658b8ae9"))));
    }

    [Test]
    public void Constructor_ClipboardString_BowlerNameMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new NortheastMegabuck.Scores.ViewModel(data, 4);

        Assert.That(viewModel.BowlerName, Is.EqualTo("Bowler 1"));
    }

    [Test]
    public void Constructor_ClipboardString_ScoresEqualToGamesInSquad_ScoresMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new NortheastMegabuck.Scores.ViewModel(data, 4);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Scores, Has.Count.EqualTo(4));

            Assert.That(viewModel.Scores[1], Is.EqualTo(200));
            Assert.That(viewModel.Scores[2], Is.EqualTo(201));
            Assert.That(viewModel.Scores[3], Is.EqualTo(202));
            Assert.That(viewModel.Scores[4], Is.EqualTo(203));
        });
    }

    [Test]
    public void Constructor_ClipboardString_ScoresGreaterThanGamesInSquad_ScoresMappedWithoutLastScore()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203\t806";

        var viewModel = new NortheastMegabuck.Scores.ViewModel(data, 4);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Scores, Has.Count.EqualTo(4));

            Assert.That(viewModel.Scores[1], Is.EqualTo(200));
            Assert.That(viewModel.Scores[2], Is.EqualTo(201));
            Assert.That(viewModel.Scores[3], Is.EqualTo(202));
            Assert.That(viewModel.Scores[4], Is.EqualTo(203));
        });
    }

    [Test]
    public void Constructor_ClipboardString_ScoresLessThanGamesInSquad_ScoresMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new NortheastMegabuck.Scores.ViewModel(data, 5);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.Scores, Has.Count.EqualTo(4));

            Assert.That(viewModel.Scores[1], Is.EqualTo(200));
            Assert.That(viewModel.Scores[2], Is.EqualTo(201));
            Assert.That(viewModel.Scores[3], Is.EqualTo(202));
            Assert.That(viewModel.Scores[4], Is.EqualTo(203));
        });
    }
}
