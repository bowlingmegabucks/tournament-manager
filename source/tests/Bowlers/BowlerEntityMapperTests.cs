namespace BowlingMegabucks.TournamentManager.Tests.Bowlers;
internal sealed class EntityMapper
{
    private BowlingMegabucks.TournamentManager.Bowlers.EntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new BowlingMegabucks.TournamentManager.Bowlers.EntityMapper();

    [Test]
    public void Execute_IdMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Id = BowlerId.New()
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Id, Is.EqualTo(bowler.Id));
    }

    [Test]
    public void Execute_FirstNameMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "firstName" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.FirstName, Is.EqualTo(bowler.Name.First));
    }

    [Test]
    public void Execute_MiddleInitialMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "middleInitial" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.MiddleInitial, Is.EqualTo(bowler.Name.MiddleInitial));
    }

    [Test]
    public void Execute_LastNameMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "lastName" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.LastName, Is.EqualTo(bowler.Name.Last));
    }

    [Test]
    public void Execute_SuffixMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "suffix" }
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Suffix, Is.EqualTo(bowler.Name.Suffix));
    }

    [Test]
    public void Execute_StreetAddressMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            StreetAddress = "streetAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.StreetAddress, Is.EqualTo(bowler.StreetAddress));
    }

    [Test]
    public void Execute_StreetCityMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            CityAddress = "cityAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.CityAddress, Is.EqualTo(bowler.CityAddress));
    }

    [Test]
    public void Execute_StateAddressMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            StateAddress = "stateAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.StateAddress, Is.EqualTo(bowler.StateAddress));
    }

    [Test]
    public void Execute_ZipCodeMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            ZipCode = "zipCode"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.ZipCode, Is.EqualTo(bowler.ZipCode));
    }

    [Test]
    public void Execute_EmailAddressMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            EmailAddress = "emailAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.EmailAddress, Is.EqualTo(bowler.EmailAddress));
    }

    [Test]
    public void Execute_DateOfBirthMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.DateOfBirth, Is.EqualTo(bowler.DateOfBirth));
    }

    [Test]
    public void Execute_GenderMapped_Male()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Male
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Gender, Is.EqualTo(bowler.Gender));
    }

    [Test]
    public void Execute_GenderMapped_Female()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            Gender = BowlingMegabucks.TournamentManager.Models.Gender.Female
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Gender, Is.EqualTo(bowler.Gender));
    }

    [Test]
    public void Execute_USBCIdMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            USBCId = "usbcId"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.USBCId, Is.EqualTo(bowler.USBCId));
    }

    [Test]
    public void Execute_PhoneNumberMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            PhoneNumber = "phoneNumber"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.PhoneNumber, Is.EqualTo(bowler.PhoneNumber));
    }

    [Test]
    public void Execute_SocialSecurityNumberMapped()
    {
        var bowler = new BowlingMegabucks.TournamentManager.Models.Bowler
        {
            SocialSecurityNumber = "ssn"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.SocialSecurityNumber, Is.EqualTo(bowler.SocialSecurityNumber));
    }
}
