
namespace BowlingMegabucks.TournamentManager.UnitTests.Scores;

[TestFixture]
internal sealed class GridViewModel
{
    private Mock<TournamentManager.LaneAssignments.IViewModel> _laneAssignment;

    [OneTimeSetUp]
    public void SetUp()
    {
        _laneAssignment = new Mock<TournamentManager.LaneAssignments.IViewModel>();

        _laneAssignment.SetupGet(laneAssignment => laneAssignment.BowlerId).Returns(BowlerId.New());
        _laneAssignment.SetupGet(laneAssignment => laneAssignment.LaneAssignment).Returns("2C");
        _laneAssignment.SetupGet(laneAssignment => laneAssignment.BowlerName).Returns("bowlerName");
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_BowlerIdMapped()
    {
        var model = new TournamentManager.Scores.GridViewModel(_laneAssignment.Object);

        Assert.That(model.BowlerId, Is.EqualTo(_laneAssignment.Object.BowlerId));
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_LaneAssignmentMapped()
    {
        var model = new TournamentManager.Scores.GridViewModel(_laneAssignment.Object);

        Assert.That(model.LaneAssignment, Is.EqualTo(_laneAssignment.Object.LaneAssignment));
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_BowlerNameMapped()
    {
        var model = new TournamentManager.Scores.GridViewModel(_laneAssignment.Object);

        Assert.That(model.BowlerName, Is.EqualTo(_laneAssignment.Object.BowlerName));
    }

    [Test]
    public void Constructor_LaneAssignmentViewModel_ScoresEmpty()
    {
        var model = new TournamentManager.Scores.GridViewModel(_laneAssignment.Object);

        Assert.That(model.Scores, Is.Empty);
    }

    [Test]
    public void Constructor_LaneAssignmentsIViewModel_BowlerIdMapped()
    {
        var laneAssignmentViewModel = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerId = BowlerId.New()
        };

        var viewModel = new TournamentManager.Scores.GridViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.BowlerId, Is.EqualTo(laneAssignmentViewModel.BowlerId));
    }

    [Test]
    public void Constructor_LaneAssignmentsIViewModel_LaneAssignmentMapped()
    {
        var laneAssignmentViewModel = new TournamentManager.LaneAssignments.ViewModel("1A");

        var viewModel = new TournamentManager.Scores.GridViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.LaneAssignment, Is.EqualTo(laneAssignmentViewModel.LaneAssignment));
    }

    [Test]
    public void Constructor_LaneAssignmentsIViewModel_BowlerNameMapped()
    {
        var laneAssignmentViewModel = new TournamentManager.LaneAssignments.ViewModel
        {
            BowlerName = "bowlerName"
        };

        var viewModel = new TournamentManager.Scores.GridViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.BowlerName, Is.EqualTo(laneAssignmentViewModel.BowlerName));
    }

    [Test]
    public void Constructor_LaneAssignmentsIViewModel_ScoresEmpty()
    {
        var laneAssignmentViewModel = new TournamentManager.LaneAssignments.ViewModel();

        var viewModel = new TournamentManager.Scores.GridViewModel(laneAssignmentViewModel);

        Assert.That(viewModel.Scores, Is.Empty);
    }

    [Test]
    public void Constructor_ClipboardString_LaneAssignmentMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new TournamentManager.Scores.GridViewModel(data, 4);

        Assert.That(viewModel.LaneAssignment, Is.EqualTo("1A"));
    }

    [Test]
    public void Constructor_ClipboardString_BowlerIdMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new TournamentManager.Scores.GridViewModel(data, 4);

        Assert.That(viewModel.BowlerId, Is.EqualTo(new BowlerId(new Guid("6c28c592-c241-401e-8414-251f658b8ae9"))));
    }

    [Test]
    public void Constructor_ClipboardString_BowlerNameMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new TournamentManager.Scores.GridViewModel(data, 4);

        Assert.That(viewModel.BowlerName, Is.EqualTo("Bowler 1"));
    }

    [Test]
    public void Constructor_ClipboardString_ScoresEqualToGamesInSquad_ScoresMapped()
    {
        var data = "1A\t6c28c592-c241-401e-8414-251f658b8ae9\tBowler 1\t3\t0\t200\t201\t202\t203";

        var viewModel = new TournamentManager.Scores.GridViewModel(data, 4);

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

        var viewModel = new TournamentManager.Scores.GridViewModel(data, 4);

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

        var viewModel = new TournamentManager.Scores.GridViewModel(data, 5);

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
