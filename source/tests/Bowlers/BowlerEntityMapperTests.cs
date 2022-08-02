using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tests.Bowlers;
internal class EntityMapper
{
    private NewEnglandClassic.Bowlers.IEntityMapper _mapper;

    [OneTimeSetUp]
    public void SetUp()
        => _mapper = new NewEnglandClassic.Bowlers.EntityMapper();

    [Test]
    public void Execute_IdMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            Id = new BowlerId(Guid.NewGuid())
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Id, Is.EqualTo(bowler.Id));
    }

    [Test]
    public void Execute_FirstNameMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            FirstName = "firstName"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.FirstName, Is.EqualTo(bowler.FirstName));
    }

    [Test]
    public void Execute_MiddleInitialMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            MiddleInitial = "middleInitial"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.MiddleInitial, Is.EqualTo(bowler.MiddleInitial));
    }

    [Test]
    public void Execute_LastNameMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            LastName = "lastName"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.LastName, Is.EqualTo(bowler.LastName));
    }

    [Test]
    public void Execute_SuffixMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            Suffix = "suffix"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Suffix, Is.EqualTo(bowler.Suffix));
    }

    [Test]
    public void Execute_StreetAddressMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            StreetAddress = "streetAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.StreetAddress, Is.EqualTo(bowler.StreetAddress));
    }

    [Test]
    public void Execute_StreetCityMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            CityAddress = "cityAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.CityAddress, Is.EqualTo(bowler.CityAddress));
    }

    [Test]
    public void Execute_StateAddressMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            StateAddress = "stateAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.StateAddress, Is.EqualTo(bowler.StateAddress));
    }

    [Test]
    public void Execute_ZipCodeMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            ZipCode = "zipCode"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.ZipCode, Is.EqualTo(bowler.ZipCode));
    }

    [Test]
    public void Execute_EmailAddressMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            EmailAddress = "emailAddress"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.EmailAddress, Is.EqualTo(bowler.EmailAddress));
    }

    [Test]
    public void Execute_DateOfBirthMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now)
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.DateOfBirth, Is.EqualTo(bowler.DateOfBirth));
    }

    [Test]
    public void Execute_GenderMapped([Values] NewEnglandClassic.Models.Gender gender)
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            Gender = gender
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.Gender, Is.EqualTo(bowler.Gender));
    }

    [Test]
    public void Execute_USBCIdMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            USBCId = "usbcId"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.USBCId, Is.EqualTo(bowler.USBCId));
    }

    [Test]
    public void Execute_PhoneNumberMapped()
    {
        var bowler = new NewEnglandClassic.Models.Bowler
        {
            PhoneNumber = "phoneNumber"
        };

        var entity = _mapper.Execute(bowler);

        Assert.That(entity.PhoneNumber, Is.EqualTo(bowler.PhoneNumber));
    }
}
