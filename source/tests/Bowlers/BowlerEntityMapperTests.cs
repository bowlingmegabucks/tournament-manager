namespace NortheastMegabuck.Tests.Bowlers;
internal sealed class EntityMapper
{
    private NortheastMegabuck.Bowlers.EntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new NortheastMegabuck.Bowlers.EntityMapper();

    [Test]
    public void Execute_IdMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Id, Is.EqualTo(bowler.Id));
    }

    [Test]
    public void Execute_FirstNameMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { First = "firstName" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.FirstName, Is.EqualTo(bowler.Name.First));
    }

    [Test]
    public void Execute_MiddleInitialMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { First = "middleInitial" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.MiddleInitial, Is.EqualTo(bowler.Name.MiddleInitial));
    }

    [Test]
    public void Execute_LastNameMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { First = "lastName" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.LastName, Is.EqualTo(bowler.Name.Last));
    }

    [Test]
    public void Execute_SuffixMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Name = new NortheastMegabuck.Models.PersonName { First = "suffix" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Suffix, Is.EqualTo(bowler.Name.Suffix));
    }

    [Test]
    public void Execute_StreetAddressMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = "streetAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.StreetAddress, Is.EqualTo(bowler.StreetAddress));
    }

    [Test]
    public void Execute_StreetCityMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = "cityAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.CityAddress, Is.EqualTo(bowler.CityAddress));
    }

    [Test]
    public void Execute_StateAddressMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StateAddress = "stateAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.StateAddress, Is.EqualTo(bowler.StateAddress));
    }

    [Test]
    public void Execute_ZipCodeMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            ZipCode = "zipCode"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.ZipCode, Is.EqualTo(bowler.ZipCode));
    }

    [Test]
    public void Execute_EmailAddressMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            EmailAddress = "emailAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.EmailAddress, Is.EqualTo(bowler.EmailAddress));
    }

    [Test]
    public void Execute_DateOfBirthMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.DateOfBirth, Is.EqualTo(bowler.DateOfBirth));
    }

    [Test]
    public void Execute_GenderMapped_Male()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Gender = NortheastMegabuck.Models.Gender.Male
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Gender, Is.EqualTo(bowler.Gender));
    }

    [Test]
    public void Execute_GenderMapped_Female()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            Gender = NortheastMegabuck.Models.Gender.Female
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Gender, Is.EqualTo(bowler.Gender));
    }

    [Test]
    public void Execute_USBCIdMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = "usbcId"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.USBCId, Is.EqualTo(bowler.USBCId));
    }

    [Test]
    public void Execute_PhoneNumberMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            PhoneNumber = "phoneNumber"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.PhoneNumber, Is.EqualTo(bowler.PhoneNumber));
    }

    [Test]
    public void Execute_SocialSecurityNumberMapped()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            SocialSecurityNumber = "ssn"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.SocialSecurityNumber, Is.EqualTo(bowler.SocialSecurityNumber));
    }
}
