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
}
