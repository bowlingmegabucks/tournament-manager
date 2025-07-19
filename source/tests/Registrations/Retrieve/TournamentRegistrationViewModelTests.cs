
namespace BowlingMegabucks.TournamentManager.Tests.Registrations.Retrieve;

[TestFixture]
internal sealed class TournamentRegistrationViewModel
{
    private BowlingMegabucks.TournamentManager.Models.Registration _registration;

    [OneTimeSetUp]
    public void SetUp()
    {
        _registration = new BowlingMegabucks.TournamentManager.Models.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
            {
                Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "first", MiddleInitial = "m", Last = "last" },
                Id = BowlerId.New()
            },
            Division = new BowlingMegabucks.TournamentManager.Models.Division
            {
                Number = 5,
                Name = "divisionName"
            },
            Squads =
            [
                new BowlingMegabucks.TournamentManager.Models.Squad{ Id = SquadId.New()},
                new BowlingMegabucks.TournamentManager.Models.Squad{ Id = SquadId.New()}
            ],
            Sweepers =
            [
                new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New()},
                new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New()},
                new BowlingMegabucks.TournamentManager.Models.Sweeper { Id = SquadId.New()}
            ],
            SuperSweeper = true
        };
    }

    [Test]
    public void Constructor_IdMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.Id, Is.EqualTo(_registration.Id));
    }

    [Test]
    public void Constructor_FirstNameMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.FirstName, Is.EqualTo(_registration.Bowler.Name.First));
    }

    [Test]
    public void Constructor_LastNameMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.LastName, Is.EqualTo(_registration.Bowler.Name.Last));
    }

    [Test]
    public void Constructor_BowlerNameMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.BowlerName, Is.EqualTo(_registration.Bowler.ToString()));
    }

    [Test]
    public void Constructor_BowlerIdMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.BowlerId, Is.EqualTo(_registration.Bowler.Id));
    }

    [Test]
    public void Constructor_DivisionNameMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.DivisionName, Is.EqualTo(_registration.Division.Name));
    }

    [Test]
    public void Constructor_SquadsEnteredMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.SquadsEntered, Has.Member(_registration.Squads.Select(squad => squad.Id).First()));
            Assert.That(viewModel.SquadsEntered, Has.Member(_registration.Squads.Select(squad => squad.Id).Last()));
        });
    }

    [Test]
    public void Constructor_SquadsEnteredCountMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SquadsEnteredCount, Is.EqualTo(_registration.Squads.Count()));
    }

    [Test]
    public void Constructor_SweepersEnteredMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.SweepersEntered, Has.Member(_registration.Sweepers.Select(sweeper => sweeper.Id).ToList()[0]));
            Assert.That(viewModel.SweepersEntered, Has.Member(_registration.Sweepers.Select(sweeper => sweeper.Id).ToList()[1]));
            Assert.That(viewModel.SweepersEntered, Has.Member(_registration.Sweepers.Select(sweeper => sweeper.Id).ToList()[2]));
        });
    }

    [Test]
    public void Constructor_SweepersEnteredCountMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SweepersEnteredCount, Is.EqualTo(_registration.Sweepers.Count()));
    }

    [Test]
    public void Constructor_SuperSweeperEnteredMapped()
    {
        var viewModel = new BowlingMegabucks.TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SuperSweeperEntered, Is.EqualTo(_registration.SuperSweeper));
    }
}
