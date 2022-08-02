namespace NewEnglandClassic.Tests.Models;

[TestFixture]
internal class Bowler
{
    [Test]
    public void Constructor_Entity_IdMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            Id = Guid.NewGuid()
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.Id, Is.EqualTo(entity.Id));
    }

    [Test]
    public void Constructor_Entity_FirstNameMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            FirstName = "John"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.FirstName, Is.EqualTo(entity.FirstName));
    }

    [Test]
    public void Constructor_Entity_MiddleInitialMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            MiddleInitial = "J"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.MiddleInitial, Is.EqualTo(entity.MiddleInitial));
    }

    [Test]
    public void Constructor_Entity_LastNameMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            LastName = "Doe"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.LastName, Is.EqualTo(entity.LastName));
    }

    [Test]
    public void Constructor_Entity_SuffixMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            Suffix = "III"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.Suffix, Is.EqualTo(entity.Suffix));
    }

    [Test]
    public void Constructor_Entity_StreetAddressMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            StreetAddress = "123 Main St"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.StreetAddress, Is.EqualTo(entity.StreetAddress));
    }

    [Test]
    public void Constructor_Entity_CityAddressMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            CityAddress = "New York"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.CityAddress, Is.EqualTo(entity.CityAddress));
    }

    [Test]
    public void Constructor_Entity_StateAddressMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            StateAddress = "NY"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.StateAddress, Is.EqualTo(entity.StateAddress));
    }

    [Test]
    public void Constructor_Entity_ZipCodeMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            ZipCode = "12345"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.ZipCode, Is.EqualTo(entity.ZipCode));
    }

    [Test]
    public void Constructor_Entity_EmailAddressMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            EmailAddress = "test@gmail.com"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.EmailAddress, Is.EqualTo(entity.EmailAddress));
    }

    [Test]
    public void Constructor_Entity_PhoneNumberMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            PhoneNumber = "1234567890"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.PhoneNumber, Is.EqualTo(entity.PhoneNumber));
    }

    [Test]
    public void Constructor_Entity_USBCIdMapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            USBCId = "123-4567"
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.USBCId, Is.EqualTo(entity.USBCId));
    }

    [Test]
    public void Constructor_Entity_DateOfBirthHasValue_Mapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            DateOfBirth = new DateOnly(1980, 1, 1)
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.DateOfBirth, Is.EqualTo(entity.DateOfBirth));
    }

    [Test]
    public void Constructor_Entity_DateOfBirthNull_Mapped()
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            DateOfBirth = null
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.DateOfBirth, Is.Null);
    }

    [Test]
    public void Constructor_Entity_GenderMapped([Values] NewEnglandClassic.Models.Gender? gender)
    {
        var entity = new NewEnglandClassic.Database.Entities.Bowler
        {
            Gender = gender
        };

        var model = new NewEnglandClassic.Models.Bowler(entity);

        Assert.That(model.Gender, Is.EqualTo(entity.Gender));
    }

    [Test]
    public void Constructor_IAddViewModel_IdMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.Id).Returns(Guid.NewGuid());

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.Id, Is.EqualTo(viewModel.Object.Id));
    }

    [Test]
    public void Constructor_IAddViewModel_FirstNameMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.FirstName).Returns("John");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.FirstName, Is.EqualTo(viewModel.Object.FirstName));
    }

    [Test]
    public void Constructor_IAddViewModel_MiddleInitialMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.MiddleInitial).Returns("J");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.MiddleInitial, Is.EqualTo(viewModel.Object.MiddleInitial));
    }

    [Test]
    public void Constructor_IAddViewModel_LastNameMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.LastName).Returns("Doe");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.LastName, Is.EqualTo(viewModel.Object.LastName));
    }

    [Test]
    public void Constructor_IAddViewModel_SuffixMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.Suffix).Returns("III");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.Suffix, Is.EqualTo(viewModel.Object.Suffix));
    }

    [Test]
    public void Constructor_IAddViewModel_StreetAddressMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.StreetAddress).Returns("123 Main St");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.StreetAddress, Is.EqualTo(viewModel.Object.StreetAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_CityAddressMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.CityAddress).Returns("New York");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.CityAddress, Is.EqualTo(viewModel.Object.CityAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_StateAddressMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.StateAddress).Returns("NY");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.StateAddress, Is.EqualTo(viewModel.Object.StateAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_ZipCodeMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.ZipCode).Returns("12345");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.ZipCode, Is.EqualTo(viewModel.Object.ZipCode));
    }

    [Test]
    public void Constructor_IAddViewModel_PhoneNumberMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.PhoneNumber).Returns("1234567890");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.PhoneNumber, Is.EqualTo(viewModel.Object.PhoneNumber));
    }

    [Test]
    public void Constructor_IAddViewModel_EmailAddressMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.EmailAddress).Returns("test@gmail.com");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.EmailAddress, Is.EqualTo(viewModel.Object.EmailAddress));
    }

    [Test]
    public void Constructor_IAddViewModel_DateOfBirthMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.DateOfBirth).Returns(new DateOnly(2000, 1, 1));

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.DateOfBirth, Is.EqualTo(viewModel.Object.DateOfBirth));
    }

    [Test]
    public void Constructor_IAddViewModel_DateOfBirthNull_Mapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.DateOfBirth).Returns((DateOnly?)null);

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.DateOfBirth, Is.Null);
    }

    [Test]
    public void Constructor_IAddViewModel_USBCIdMapped()
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.USBCId).Returns("123-4567");

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.USBCId, Is.EqualTo(viewModel.Object.USBCId));
    }

    [Test]
    public void Constructor_IAddViewModel_GenderMapped([Values] NewEnglandClassic.Models.Gender? gender)
    {
        var viewModel = new Mock<NewEnglandClassic.Bowlers.Add.IViewModel>();
        viewModel.SetupGet(v => v.Gender).Returns(gender);

        var model = new NewEnglandClassic.Models.Bowler(viewModel.Object);

        Assert.That(model.Gender, Is.EqualTo(viewModel.Object.Gender));
    }

    [Test]
    public void Age_BirthDayYesterday_Correct()
    {
        var bowler = new NewEnglandClassic.Models.Bowler()
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
        var bowler = new NewEnglandClassic.Models.Bowler()
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
        var bowler = new NewEnglandClassic.Models.Bowler()
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(1).AddYears(-20))
        };

        var expected = 19;
        var actual = bowler.Age;

        Assert.That(actual, Is.EqualTo(expected));
    }
}
