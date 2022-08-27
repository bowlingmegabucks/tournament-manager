
namespace NewEnglandClassic.Tests.Registrations.Retrieve;

[TestFixture]
internal class TournamentRegistrationViewModel
{
    private NewEnglandClassic.Models.Registration _registration;

    [OneTimeSetUp]
    public void SetUp()
    {
        _registration = new NewEnglandClassic.Models.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new NewEnglandClassic.Models.Bowler
            {
                FirstName = "first",
                MiddleInitial = "m",
                LastName = "last"
            },
            Division = new NewEnglandClassic.Models.Division
            {
                Number = 5,
                Name = "divisionName"
            },
            Squads = new[]
            {
                new NewEnglandClassic.Models.Squad{ Id = SquadId.New()},
                new NewEnglandClassic.Models.Squad{ Id = SquadId.New()}
            },
            Sweepers = new[]
            {
                new NewEnglandClassic.Models.Sweeper { Id = SquadId.New()},
                new NewEnglandClassic.Models.Sweeper { Id = SquadId.New()},
                new NewEnglandClassic.Models.Sweeper { Id = SquadId.New()}
            },
            SuperSweeper = true
        };
    }

    [Test]
    public void Constructor_IdMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.Id, Is.EqualTo(_registration.Id));
    }

    [Test]
    public void Constructor_FirstNameMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.FirstName, Is.EqualTo(_registration.Bowler.FirstName));
    }

    [Test]
    public void Constructor_LastNameMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.LastName, Is.EqualTo(_registration.Bowler.LastName));
    }

    [Test]
    public void Constructor_BowlerNameMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.BowlerName, Is.EqualTo(_registration.Bowler.ToString()));
    }

    [Test]
    public void Constructor_DivisionNameMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.DivisionName, Is.EqualTo(_registration.Division.Name));
    }

    [Test]
    public void Constructor_SquadsEnteredMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.SquadsEntered, Has.Member(_registration.Squads.Select(squad => squad.Id).First()));
            Assert.That(viewModel.SquadsEntered, Has.Member(_registration.Squads.Select(squad => squad.Id).Last()));
        });
    }

    [Test]
    public void Constructor_SquadsEnteredCountMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SquadsEnteredCount, Is.EqualTo(_registration.Squads.Count()));
    }

    [Test]
    public void Constructor_SweepersEnteredMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

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
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SweepersEnteredCount, Is.EqualTo(_registration.Sweepers.Count()));
    }

    [Test]
    public void Constructor_SuperSweeperEnteredMapped()
    {
        var viewModel = new NewEnglandClassic.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SuperSweeperEntered, Is.EqualTo(_registration.SuperSweeper));
    }
}
