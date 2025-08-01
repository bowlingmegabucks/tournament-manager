﻿
namespace BowlingMegabucks.TournamentManager.UnitTests.Registrations.Retrieve;

[TestFixture]
internal sealed class TournamentRegistrationViewModel
{
    private TournamentManager.Models.Registration _registration;

    [OneTimeSetUp]
    public void SetUp()
    {
        _registration = new TournamentManager.Models.Registration
        {
            Id = RegistrationId.New(),
            Bowler = new TournamentManager.Models.Bowler
            {
                Name = new TournamentManager.Models.PersonName { First = "first", MiddleInitial = "m", Last = "last" },
                Id = BowlerId.New()
            },
            Division = new TournamentManager.Models.Division
            {
                Number = 5,
                Name = "divisionName"
            },
            Squads =
            [
                new TournamentManager.Models.Squad{ Id = SquadId.New()},
                new TournamentManager.Models.Squad{ Id = SquadId.New()}
            ],
            Sweepers =
            [
                new TournamentManager.Models.Sweeper { Id = SquadId.New()},
                new TournamentManager.Models.Sweeper { Id = SquadId.New()},
                new TournamentManager.Models.Sweeper { Id = SquadId.New()}
            ],
            SuperSweeper = true
        };
    }

    [Test]
    public void Constructor_IdMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.Id, Is.EqualTo(_registration.Id));
    }

    [Test]
    public void Constructor_FirstNameMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.FirstName, Is.EqualTo(_registration.Bowler.Name.First));
    }

    [Test]
    public void Constructor_LastNameMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.LastName, Is.EqualTo(_registration.Bowler.Name.Last));
    }

    [Test]
    public void Constructor_BowlerNameMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.BowlerName, Is.EqualTo(_registration.Bowler.ToString()));
    }

    [Test]
    public void Constructor_BowlerIdMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.BowlerId, Is.EqualTo(_registration.Bowler.Id));
    }

    [Test]
    public void Constructor_DivisionNameMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.DivisionName, Is.EqualTo(_registration.Division.Name));
    }

    [Test]
    public void Constructor_SquadsEnteredMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.Multiple(() =>
        {
            Assert.That(viewModel.SquadsEntered, Has.Member(_registration.Squads.Select(squad => squad.Id).First()));
            Assert.That(viewModel.SquadsEntered, Has.Member(_registration.Squads.Select(squad => squad.Id).Last()));
        });
    }

    [Test]
    public void Constructor_SquadsEnteredCountMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SquadsEnteredCount, Is.EqualTo(_registration.Squads.Count()));
    }

    [Test]
    public void Constructor_SweepersEnteredMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

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
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SweepersEnteredCount, Is.EqualTo(_registration.Sweepers.Count()));
    }

    [Test]
    public void Constructor_SuperSweeperEnteredMapped()
    {
        var viewModel = new TournamentManager.Registrations.Retrieve.TournamentRegistrationViewModel(_registration);

        Assert.That(viewModel.SuperSweeperEntered, Is.EqualTo(_registration.SuperSweeper));
    }
}
