using BowlingMegabucks.TournamentManager.Bowlers;

namespace BowlingMegabucks.TournamentManager.UnitTests.Models;

[TestFixture]
internal sealed class Bowler
{
    [Test]
    public void Constructor_Entity_IdMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            Id = BowlerId.New()
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public void Constructor_Entity_FirstNameMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            FirstName = "John"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Name.First, Is.EqualTo(entity.FirstName));
    }

    [Test]
    public void Constructor_Entity_MiddleInitialMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            MiddleInitial = "J"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Name.MiddleInitial, Is.EqualTo(entity.MiddleInitial));
    }

    [Test]
    public void Constructor_Entity_LastNameMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            LastName = "Doe"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Name.Last, Is.EqualTo(entity.LastName));
    }

    [Test]
    public void Constructor_Entity_SuffixMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            Suffix = "III"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Name.Suffix, Is.EqualTo(entity.Suffix));
    }

    [Test]
    public void Constructor_Entity_StreetAddressMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            StreetAddress = "123 Main St"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.StreetAddress, Is.EqualTo(entity.StreetAddress));
    }

    [Test]
    public void Constructor_Entity_CityAddressMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            CityAddress = "New York"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.CityAddress, Is.EqualTo(entity.CityAddress));
    }

    [Test]
    public void Constructor_Entity_StateAddressMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            StateAddress = "NY"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.StateAddress, Is.EqualTo(entity.StateAddress));
    }

    [Test]
    public void Constructor_Entity_ZipCodeMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            ZipCode = "12345"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.ZipCode, Is.EqualTo(entity.ZipCode));
    }

    [Test]
    public void Constructor_Entity_EmailAddressMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            EmailAddress = "test@gmail.com"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.EmailAddress, Is.EqualTo(entity.EmailAddress));
    }

    [Test]
    public void Constructor_Entity_PhoneNumberMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            PhoneNumber = "1234567890"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.PhoneNumber, Is.EqualTo(entity.PhoneNumber));
    }

    [Test]
    public void Constructor_Entity_USBCIdMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            USBCId = "123-4567"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.USBCId, Is.EqualTo(entity.USBCId));
    }

    [Test]
    public void Constructor_Entity_DateOfBirthHasValue_Mapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            DateOfBirth = new DateOnly(1980, 1, 1)
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.DateOfBirth, Is.EqualTo(entity.DateOfBirth));
    }

    [Test]
    public void Constructor_Entity_DateOfBirthNull_Mapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            DateOfBirth = null
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.DateOfBirth, Is.Null);
    }

    [Test]
    public void Constructor_Entity_GenderMapped_Male()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            Gender = TournamentManager.Models.Gender.Male
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Gender, Is.EqualTo(entity.Gender));
    }

    [Test]
    public void Constructor_Entity_GenderMapped_Female()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            Gender = TournamentManager.Models.Gender.Female
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.Gender, Is.EqualTo(entity.Gender));
    }

    [Test]
    public void Constructor_Entity_SocialSecurityNumberMapped()
    {
        var entity = new TournamentManager.Database.Entities.Bowler
        {
            SocialSecurityNumber = "ssn"
        };

        var model = new TournamentManager.Models.Bowler(entity);

        Assert.That(model.SocialSecurityNumber, Is.EqualTo(entity.SocialSecurityNumber));
    }

    [Test]
    public void Constructor_IAddViewModel_IdMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(BowlerId.New());

        var model = viewModel.Object.ToModel();

        Assert.That(model.Id, Is.EqualTo(viewModel.Object.Id));
    }

    [Test]
    public void Constructor_IAddViewModel_FirstNameMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.FirstName).Returns("John");

        var model = viewModel.Object.ToModel();

        Assert.That(model.Name.First, Is.EqualTo(viewModel.Object.FirstName));
    }

    [Test]
    public void Constructor_IAddViewModel_MiddleInitialMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.MiddleInitial).Returns("J");

        var model = viewModel.Object.ToModel();

        Assert.That(model.Name.MiddleInitial, Is.EqualTo(viewModel.Object.MiddleInitial));
    }

    [Test]
    public void Constructor_IAddViewModel_LastNameMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.LastName).Returns("Doe");

        var model = viewModel.Object.ToModel();

        Assert.That(model.Name.Last, Is.EqualTo(viewModel.Object.LastName));
    }

    [Test]
    public void Constructor_IAddViewModel_SuffixMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.Suffix).Returns("III");

        var model = viewModel.Object.ToModel();

        Assert.That(model.Name.Suffix, Is.EqualTo(viewModel.Object.Suffix));
    }

    [Test]
    public void Constructor_IAddViewModel_StreetAddressMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.StreetAddress).Returns("123 Main St");

        var model = viewModel.Object.ToModel();

        Assert.That(model.StreetAddress, Is.EqualTo(viewModel.Object.StreetAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_CityAddressMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.CityAddress).Returns("New York");

        var model = viewModel.Object.ToModel();

        Assert.That(model.CityAddress, Is.EqualTo(viewModel.Object.CityAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_StateAddressMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.StateAddress).Returns("NY");

        var model = viewModel.Object.ToModel();

        Assert.That(model.StateAddress, Is.EqualTo(viewModel.Object.StateAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_ZipCodeMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.ZipCode).Returns("12345");

        var model = viewModel.Object.ToModel();

        Assert.That(model.ZipCode, Is.EqualTo(viewModel.Object.ZipCode));
    }

    [Test]
    public void Constructor_IAddViewModel_PhoneNumberMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.PhoneNumber).Returns("1234567890");

        var model = viewModel.Object.ToModel();

        Assert.That(model.PhoneNumber, Is.EqualTo(viewModel.Object.PhoneNumber));
    }

    [Test]
    public void Constructor_IAddViewModel_EmailAddressMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.EmailAddress).Returns("test@gmail.com");

        var model = viewModel.Object.ToModel();

        Assert.That(model.EmailAddress, Is.EqualTo(viewModel.Object.EmailAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_DateOfBirthMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.DateOfBirth).Returns(new DateOnly(2000, 1, 1));

        var model = viewModel.Object.ToModel();

        Assert.That(model.DateOfBirth, Is.EqualTo(viewModel.Object.DateOfBirth));
    }

    [Test]
    public void Constructor_IAddViewModel_DateOfBirthNull_Mapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.DateOfBirth).Returns((DateOnly?)null);

        var model = viewModel.Object.ToModel();

        Assert.That(model.DateOfBirth, Is.Null);
    }

    [Test]
    public void Constructor_IAddViewModel_USBCIdMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.USBCId).Returns("123-4567");

        var model = viewModel.Object.ToModel();

        Assert.That(model.USBCId, Is.EqualTo(viewModel.Object.USBCId));
    }

    [Test]
    public void Constructor_IAddViewModel_SocialSecurityNumberMapped()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.SocialSecurityNumber).Returns("ssn");

        var model = viewModel.Object.ToModel();

        Assert.That(model.SocialSecurityNumber, Is.EqualTo(viewModel.Object.SocialSecurityNumber));
    }

    [Test]
    public void Constructor_IAddViewModel_GenderMapped_Male()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.Gender).Returns(TournamentManager.Models.Gender.Male);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Gender, Is.EqualTo(viewModel.Object.Gender));
    }

    [Test]
    public void Constructor_IAddViewModel_GenderMapped_Female()
    {
        var viewModel = new Mock<IViewModel>();
        viewModel.SetupGet(v => v.Gender).Returns(TournamentManager.Models.Gender.Female);

        var model = viewModel.Object.ToModel();

        Assert.That(model.Gender, Is.EqualTo(viewModel.Object.Gender));
    }

    [Test]
    public void Age_BirthDayYesterday_Correct()
    {
        var bowler = new TournamentManager.Models.Bowler()
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(-1).AddYears(-20))
        };

        var expected = 20;
        var actual = bowler.Age;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Age_BirthDayToday_Correct()
    {
        var bowler = new TournamentManager.Models.Bowler()
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-20))
        };

        var expected = 20;
        var actual = bowler.Age;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void Age_BirthDayTomorrow_Correct()
    {
        var bowler = new TournamentManager.Models.Bowler()
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(1).AddYears(-20))
        };

        var expected = 19;
        var actual = bowler.Age;

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToString_NoSuffix_ReturnsFirstLastName()
    {
        var bowler = new TournamentManager.Models.Bowler();
        bowler.Name.First = "first";
        bowler.Name.MiddleInitial = "m";
        bowler.Name.Last = "last";

        var expected = "first last";
        var actual = bowler.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ToString_Suffix_ReturnsFirstLastCommaSuffix()
    {
        var bowler = new TournamentManager.Models.Bowler();
        bowler.Name.First = "first";
        bowler.Name.MiddleInitial = "m";
        bowler.Name.Last = "last";
        bowler.Name.Suffix = "suffix";

        var expected = "first last, suffix";
        var actual = bowler.ToString();

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void GetHashCode_ReturnsIdHashCode()
    {
        var id = BowlerId.New();
        var bowler = new TournamentManager.Models.Bowler
        {
            Id = id
        };

        Assert.That(bowler.GetHashCode(), Is.EqualTo(id.GetHashCode()));
    }

    [Test]
    public void Equals_ObjNull_ReturnsFalse()
    {
        var id = BowlerId.New();
        var bowler = new TournamentManager.Models.Bowler
        {
            Id = id
        };

#pragma warning disable CA1508 // Avoid dead conditional code
        Assert.That(bowler.Equals(null), Is.False);
#pragma warning restore CA1508 // Avoid dead conditional code
    }

    [Test]
    public void Equals_ObjNotBowlerModel_ReturnsFalse()
    {
        var id = BowlerId.New();
        var bowler = new TournamentManager.Models.Bowler
        {
            Id = id
        };

        var somethingElse = new TournamentManager.Models.Squad();

        Assert.That(bowler.Equals(somethingElse), Is.False);
    }

    [Test]
    public void Equals_ObjBowlerButDifferentId_ReturnsFalse()
    {
        var id = BowlerId.New();
        var bowler = new TournamentManager.Models.Bowler
        {
            Id = id
        };

        var id2 = BowlerId.New();
        var bowler2 = new TournamentManager.Models.Bowler
        {
            Id = id2
        };

        Assert.That(bowler.Equals(bowler2), Is.False);
    }

    [Test]
    public void Equals_ObjBowlerAndSameId_ReturnsTrue()
    {
        var id = BowlerId.New();
        var bowler = new TournamentManager.Models.Bowler
        {
            Id = id
        };

        var bowler2 = new TournamentManager.Models.Bowler
        {
            Id = id,
        };
        bowler2.Name.First = "first";

        Assert.That(bowler.Equals(bowler2), Is.True);
    }
}
