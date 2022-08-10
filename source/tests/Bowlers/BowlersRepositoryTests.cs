using NewEnglandClassic.Tests.Extensions;

namespace NewEnglandClassic.Tests.Bowlers;

[TestFixture]
internal class Repository
{
    private Mock<NewEnglandClassic.Database.IDataContext> _dataContext;

    private NewEnglandClassic.Bowlers.IRepository _repository;

    [SetUp]
    public void SetUp()
    {
        _dataContext = new Mock<NewEnglandClassic.Database.IDataContext>();

        _repository = new NewEnglandClassic.Bowlers.Repository(_dataContext.Object);
    }

    [Test]
    public void Search_AllSearchCriteriaNullOrWhitespace_ReturnsAllBowlers([Values(null,""," ")]string value)
    {
        var bowler1 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());
        
        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria()
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
        var bowler1 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria()
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
        var bowler1 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };
        
        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria()
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
        var bowler1 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };
        
        var bowler3 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());
        
        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria()
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
    public void Search_AllFieldsSet_ResultsAreCumulative_NoResultsReturned()
    {
        var bowler1 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Doe",
            EmailAddress = "johndoe@gmail.com"
        };

        var bowler2 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "Jane",
            LastName = "Doe",
            EmailAddress = "janedoe@gmail.com"
        };

        var bowler3 = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = BowlerId.New(),
            FirstName = "John",
            LastName = "Smith",
            EmailAddress = "johnsmith@gmail.com"
        };

        var bowlers = new[] { bowler1, bowler2, bowler3 };

        _dataContext.Setup(dataContext => dataContext.Bowlers).Returns(bowlers.SetUpDbContext());

        var searchCriteria = new NewEnglandClassic.Models.BowlerSearchCriteria()
        {
            LastName = "D",
            FirstName = "J",
            EmailAddress = "johnsmith@gmail.com"
        };

        var actual = _repository.Search(searchCriteria);

        Assert.That(actual, Is.Empty);
    }
}