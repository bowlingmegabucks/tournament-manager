using NortheastMegabuck.Tests.Extensions;

namespace NortheastMegabuck.Tests.Bowlers;

[TestFixture]
internal class Repository
{
    private Mock<NortheastMegabuck.Database.IDataContext> _dataContext;

    private NortheastMegabuck.Bowlers.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NortheastMegabuck.Database.IDataContext>();

        _repository = new NortheastMegabuck.Bowlers.Repository(_dataContext.Object);
    }

    [Test]
    public void Search_AllSearchCriteriaNullOrWhitespace_ReturnsAllBowlers([Values(null,""," ")]string value)
    {
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());
        
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria()
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
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria()
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
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };
        
        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria()
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
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };
        
        var bowler3 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());
        
        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria()
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
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria()
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

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Squads = new[] { new NortheastMegabuck.Database.Entities.SquadRegistration { SquadId = squad1 } }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Squads = new[]
            {
                new NortheastMegabuck.Database.Entities.SquadRegistration { SquadId = squad2},
                new NortheastMegabuck.Database.Entities.SquadRegistration { SquadId = squad3}
            }
        };

        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            Registrations = new[] { registration1, registration2 }
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria
        {
            WithoutRegistrationOnSquads = new[] { squad4, squad5 }
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

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Squads = new[] { new NortheastMegabuck.Database.Entities.SquadRegistration { SquadId = squad1 } }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Squads = new[]
            {
                new NortheastMegabuck.Database.Entities.SquadRegistration { SquadId = squad2},
                new NortheastMegabuck.Database.Entities.SquadRegistration { SquadId = squad3}
            }
        };

        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            Registrations = new[] { registration1, registration2 }
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria
        {
            WithoutRegistrationOnSquads = new[] { squad4, squad5,squad3 }
        };

        var results = _repository.Search(searchCriteria).ToList();

        Assert.That(results, Is.Empty);
    }

    [Test]
    public void Search_RegisteredInTournamentOnRegistration_ReturnsBowler()
    {
        var tournamentId = TournamentId.New();

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = tournamentId
            }
        };

        var registration3 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            Registrations = new[] { registration1, registration2, registration3 }
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria
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

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration3 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            Registrations = new[] { registration1, registration2, registration3 }
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria
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

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = tournamentId
            }
        };

        var registration3 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            Registrations = new[] { registration1, registration2, registration3 }
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria
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

        var registration1 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration2 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var registration3 = new NortheastMegabuck.Database.Entities.Registration
        {
            Division = new NortheastMegabuck.Database.Entities.Division
            {
                TournamentId = TournamentId.New()
            }
        };

        var bowler = new NortheastMegabuck.Database.Entities.Bowler
        {
            Registrations = new[] { registration1, registration2, registration3 }
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler }.SetUpDbContext());

        var searchCriteria = new NortheastMegabuck.Models.BowlerSearchCriteria
        {
            NotRegisteredInTournament = tournamentId
        };

        var result = _repository.Search(searchCriteria).ToList();

        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public void Update_BowlerName_NameUpdated()
    {
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "firstName1",
            MiddleInitial = "middleInitial1",
            LastName = "lastName1",
            Suffix = "suffix1"
        };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(new[] { bowler1 }.SetUpDbContext());

        _repository.Update(bowler1.Id, "firstName2", "middleInitial2", "lastName2", "suffix2");

        var updated = _repository.Search(new NortheastMegabuck.Models.BowlerSearchCriteria { LastName = "lastName2" });

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
    public void Retrieve_BowlerId_ReturnsBowler()
    {
        var bowler1 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NortheastMegabuck.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var bowler = _repository.Retrieve(bowler2.Id);

        Assert.That(bowler.LastName, Is.EqualTo(bowler2.LastName));
    }
}