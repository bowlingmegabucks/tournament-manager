using BowlingMegabucks.TournamentManager.UnitTests.Extensions;

namespace BowlingMegabucks.TournamentManager.UnitTests.Bowlers;

[TestFixture]
internal sealed class Repository
{
    private Mock<TournamentManager.Database.IDataContext> _dataContext;

    private TournamentManager.Bowlers.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<TournamentManager.Database.IDataContext>();

        _repository = new TournamentManager.Bowlers.Repository(_dataContext.Object);
    }

    [Test]
    public void Search_AllSearchCriteriaNullOrWhitespace_ReturnsAllBowlers([Values(null, "", " ")] string value)
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria()
        {
            LastName = value,
            FirstName = value,
            EmailAddress = value
        };

        var actual = _repository.Search(searchCriteria);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(3));

            Assert.That(actual.Any(tournament => tournament.Id == bowler1.Id), Is.True, "bowler1 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == bowler2.Id), Is.True, "bowler2 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == bowler3.Id), Is.True, "bowler3 Id Not Found");
        });
    }

    [Test]
    public void Search_OnlyLastNameSetForSearchCriteria_CorrectBowlersReturned([Values(null, "", " ")] string value)
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria()
        {
            LastName = "Do",
            FirstName = value,
            EmailAddress = value
        };

        var actual = _repository.Search(searchCriteria);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));

            Assert.That(actual.Any(tournament => tournament.Id == bowler1.Id), Is.True, "bowler1 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == bowler2.Id), Is.True, "bowler2 Id Not Found");
        });
    }

    [Test]
    public void Search_OnlyFirstNameSetForSearchCriteria_CorrectBowlersReturned([Values(null, "", " ")] string value)
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria()
        {
            LastName = value,
            FirstName = "Jo",
            EmailAddress = value
        };

        var actual = _repository.Search(searchCriteria);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(2));

            Assert.That(actual.Any(tournament => tournament.Id == bowler1.Id), Is.True, "bowler1 Id Not Found");
            Assert.That(actual.Any(tournament => tournament.Id == bowler3.Id), Is.True, "bowler3 Id Not Found");
        });
    }

    [Test]
    public void Search_OnlyEmailAddressSetForSearchCriteria_CorrectBowlersReturned([Values(null, "", " ")] string value)
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria()
        {
            LastName = value,
            FirstName = value,
            EmailAddress = "johnsmith@gmail.com"
        };

        var actual = _repository.Search(searchCriteria);

        Assert.Multiple(() =>
        {
            Assert.That(actual.Count(), Is.EqualTo(1));

            Assert.That(actual.Any(tournament => tournament.Id == bowler3.Id), Is.True, "bowler3 Id Not Found");
        });
    }

    [Test]
    public void Search_AllBowlerFieldsSet_ResultsAreCumulative_NoResultsReturned()
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria()
        {
            LastName = "D",
            FirstName = "J",
            EmailAddress = "johnsmith@gmail.com"
        };

        var actual = _repository.Search(searchCriteria);

        Assert.That(actual, Is.Empty);
    }

    [Test]
    public void Search_BowlerIsNotOnWithoutRegistrationFrom_IsReturned()
    {
        var squad1 = SquadId.New();
        var squad2 = SquadId.New();
        var squad3 = SquadId.New();
        var squad4 = SquadId.New();
        var squad5 = SquadId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Squads = [new TournamentManager.Database.Entities.SquadRegistration { SquadId = squad1 }]
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { SquadId = squad2},
                new TournamentManager.Database.Entities.SquadRegistration { SquadId = squad3}
            ]
        };

        var bowler = new TournamentManager.Database.Entities.Bowler
        {
            Registrations = [registration1, registration2]
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria
        {
            WithoutRegistrationOnSquads = [squad4, squad5]
        };

        var results = _repository.Search(searchCriteria).ToList();

        Assert.That(results, Has.Count.EqualTo(1));
    }

    [Test]
    public void Search_BowlerIsOnWithoutRegistrationFrom_IsReturned()
    {
        var squad1 = SquadId.New();
        var squad2 = SquadId.New();
        var squad3 = SquadId.New();
        var squad4 = SquadId.New();
        var squad5 = SquadId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Squads = [new TournamentManager.Database.Entities.SquadRegistration { SquadId = squad1 }]
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Squads =
            [
                new TournamentManager.Database.Entities.SquadRegistration { SquadId = squad2},
                new TournamentManager.Database.Entities.SquadRegistration { SquadId = squad3}
            ]
        };

        var bowler = new TournamentManager.Database.Entities.Bowler
        {
            Registrations = [registration1, registration2]
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria
        {
            WithoutRegistrationOnSquads = [squad4, squad5, squad3]
        };

        var results = _repository.Search(searchCriteria).ToList();

        Assert.That(results, Is.Empty);
    }

    [Test]
    public void Search_RegisteredInTournamentOnRegistration_ReturnsBowler()
    {
        var tournamentId = TournamentId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = tournamentId
            }
        };

        var registration3 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new TournamentManager.Database.Entities.Bowler
        {
            Registrations = [registration1, registration2, registration3]
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria
        {
            RegisteredInTournament = tournamentId
        };

        var result = _repository.Search(searchCriteria).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public void Search_RegisteredInTournamentNotOnRegistration_ReturnsBowler()
    {
        var tournamentId = TournamentId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration3 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new TournamentManager.Database.Entities.Bowler
        {
            Registrations = [registration1, registration2, registration3]
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria
        {
            RegisteredInTournament = tournamentId
        };

        var result = _repository.Search(searchCriteria).ToList();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Search_NotRegisteredInTournamentOnRegistration_ReturnsNoBowlers()
    {
        var tournamentId = TournamentId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = tournamentId
            }
        };

        var registration3 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new TournamentManager.Database.Entities.Bowler
        {
            Registrations = [registration1, registration2, registration3]
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria
        {
            NotRegisteredInTournament = tournamentId
        };

        var result = _repository.Search(searchCriteria).ToList();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Search_NotRegisteredInTournamentNotOnRegistration_ReturnsBowler()
    {
        var tournamentId = TournamentId.New();

        var registration1 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration3 = new TournamentManager.Database.Entities.Registration
        {
            Division = new TournamentManager.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new TournamentManager.Database.Entities.Bowler
        {
            Registrations = [registration1, registration2, registration3]
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new TournamentManager.Models.BowlerSearchCriteria
        {
            NotRegisteredInTournament = tournamentId
        };

        var result = _repository.Search(searchCriteria).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task UpdateAsync_BowlerName_NameUpdated()
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "firstName1",
            MiddleInitial = "middleInitial1",
            LastName = "lastName1",
            Suffix = "suffix1"
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler1 }.SetUpDbContext());

        await _repository.UpdateAsync(bowler1.Id, "firstName2", "middleInitial2", "lastName2", "suffix2", default).ConfigureAwait(false);

        var updated = _repository.Search(new TournamentManager.Models.BowlerSearchCriteria { LastName = "lastName2" });

        Assert.Multiple(() =>
        {
            Assert.That(updated.Count(), Is.EqualTo(1));

            Assert.That(updated.Count(bowler => bowler.FirstName == "firstName2"), Is.EqualTo(1));
            Assert.That(updated.Count(bowler => bowler.MiddleInitial == "middleInitial2"), Is.EqualTo(1));
            Assert.That(updated.Count(bowler => bowler.LastName == "lastName2"), Is.EqualTo(1));
            Assert.That(updated.Count(bowler => bowler.Suffix == "suffix2"), Is.EqualTo(1));
        });

    }

    [Test]
    public async Task RetrieveAsync_BowlerId_ReturnsBowler()
    {
        var bowler1 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var bowler = await _repository.RetrieveAsync(bowler2.Id, default).ConfigureAwait(false);

        Assert.That(bowler.LastName, Is.EqualTo(bowler2.LastName));
    }
}