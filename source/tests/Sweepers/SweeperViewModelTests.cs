
namespace NortheastMegabuck.Tests.Sweepers;

[TestFixture]
internal sealed class SweeperViewModelTests
{
    private NortheastMegabuck.Models.Sweeper _model;
    private NortheastMegabuck.Sweepers.ViewModel _viewModel;

    [OneTimeSetUp]
    public void SetUp()
    {
        _model = new()
        {
            Id = SquadId.New(),
            TournamentId = TournamentId.New(),
            EntryFee = 100,
            Games = 3,
            CashRatio = 0.5m,
            Date = DateTime.Now,
            MaxPerPair = 11,
            StartingLane = 15,
            NumberOfLanes = 5,
            Complete = true,
            Divisions = new Dictionary<DivisionId, int?>
            {
                { NortheastMegabuck.DivisionId.New(), 1 },
                { NortheastMegabuck.DivisionId.New(), 2 },
                { NortheastMegabuck.DivisionId.New(), 3 },
            }
        };

        _viewModel = new NortheastMegabuck.Sweepers.ViewModel(_model);
    }

    [Test]
    public void Constructor_Model_IdMapped()
        => Assert.That(_viewModel.Id, Is.EqualTo(_model.Id));

    [Test]
    public void Constructor_Model_TournamentIdMapped()
        => Assert.That(_viewModel.TournamentId, Is.EqualTo(_model.TournamentId));

    [Test]
    public void Constructor_Model_EntryFeeMapped()
        => Assert.That(_viewModel.EntryFee, Is.EqualTo(_model.EntryFee));

    [Test]
    public void Constructor_Model_GamesMapped()
        => Assert.That(_viewModel.Games, Is.EqualTo(_model.Games));

    [Test]
    public void Constructor_Model_CashRatioMapped()
        => Assert.That(_viewModel.CashRatio, Is.EqualTo(_model.CashRatio));

    [Test]
    public void Constructor_Model_DateMapped()
        => Assert.That(_viewModel.Date, Is.EqualTo(_model.Date));

    [Test]
    public void Constructor_Model_MaxPerPairMapped()
        => Assert.That(_viewModel.MaxPerPair, Is.EqualTo(_model.MaxPerPair));

    [Test]
    public void Constructor_Model_StartingLaneMapped()
        => Assert.That(_viewModel.StartingLane, Is.EqualTo(_model.StartingLane));

    [Test]
    public void Constructor_Model_NumberOfLanesMapped()
        => Assert.That(_viewModel.NumberOfLanes, Is.EqualTo(_model.NumberOfLanes));

    [Test]
    public void Constructor_Model_CompleteMapped()
        => Assert.That(_viewModel.Complete, Is.EqualTo(_model.Complete));

    [Test]
    public void Constructor_Model_DivisionsMapped()
        => Assert.That(_viewModel.Divisions, Is.EqualTo(_model.Divisions));
}
