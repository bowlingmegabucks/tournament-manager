
namespace NewEnglandClassic.Tests.Sweepers;

[TestFixture]
internal class SweeperViewModelTests
{
    private NewEnglandClassic.Models.Sweeper _model;
    private NewEnglandClassic.Sweepers.IViewModel _viewModel;

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
            Complete = true,
            Divisions = new Dictionary<NewEnglandClassic.Divisions.Id, int?>
            {
                { NewEnglandClassic.Divisions.Id.New(), 1 },
                { NewEnglandClassic.Divisions.Id.New(), 2 },
                { NewEnglandClassic.Divisions.Id.New(), 3 },
            }
        };

        _viewModel = new NewEnglandClassic.Sweepers.ViewModel(_model);
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
    public void Constructor_Model_CompleteMapped()
        => Assert.That(_viewModel.Complete, Is.EqualTo(_model.Complete));

    [Test]
    public void Constructor_Model_DivisionsMapped()
        => Assert.That(_viewModel.Divisions, Is.EqualTo(_model.Divisions));
}
