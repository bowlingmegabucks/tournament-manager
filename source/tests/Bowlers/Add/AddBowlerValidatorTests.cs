using FluentValidation;
using FluentValidation.TestHelper;

namespace NortheastMegabuck.Tests.Bowlers.Add;

[TestFixture]
internal class Validator
{
    private IValidator<NortheastMegabuck.Models.Bowler> _validator;

    [OneTimeSetUp]
    public void SetUp()
        => _validator = new NortheastMegabuck.Bowlers.Add.Validator();

    [Test]
    public void Name_HasPersonNameValidator()
        => _validator.ShouldHaveChildValidator(bowler => bowler.Name, typeof(NortheastMegabuck.Bowlers.PersonNameValidator));

    [Test]
    public void CityAddress_NullOrEmpty_With_Street_Has_Error([Values(null, "", " ")] string city)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = city,
            StreetAddress = "123 Main St",
            StateAddress = "MA",
            ZipCode = "01234"
        };
        

        var result = _validator.TestValidate(bowler);

        result.ShouldHaveValidationErrorFor(a => a.CityAddress).WithErrorMessage("City is Required when Street is given");
    }

    [Test]
    public void City_NullOrEmpty_With_No_Street_Has_No_Error([Values(null, "", " ")] string city, [Values(null, "", " ")] string state)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = city,
            StreetAddress = null,
            StateAddress = state,
            ZipCode = "01234"
        };

        var result = _validator.TestValidate(bowler);

        result.ShouldNotHaveValidationErrorFor(a => a.CityAddress);
    }

    [Test]
    public void State_Length_3_To_100_Has_Error([Range(3, 100)] int length) => State_Length_Not_2_Has_Error(length);

    [Test]
    public void State_Length_0_To_1_Has_Error([Range(0, 1)] int length) => State_Length_Not_2_Has_Error(length);

    private void State_Length_Not_2_Has_Error(int length)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = "Boston",
            StreetAddress = "123 Main St",
            StateAddress = new string('J', length),
            ZipCode = "01234"
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.StateAddress).WithErrorMessage("State must be Postal Abbreviation");
    }

    [Test]
    public void State_Length_2_Has_No_Error()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = "Boston",
            StreetAddress = "123 Main St",
            StateAddress = "MA",
            ZipCode = "01234"
        };

        var result = _validator.TestValidate(bowler);

        result.ShouldNotHaveValidationErrorFor(a => a.StateAddress);
    }

    [Test]
    public void State_NullOrEmtpy_Street_Given_Has_Error([Values(null, "", " ")] string state)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = "123 Main St",
            StateAddress = state,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.StateAddress).WithErrorMessage("State is required when Street is given");
    }

    [Test]
    public void Zip_NullOrEmpty_Street_Given_Has_Error([Values(null, "", " ")] string zip)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = "123 Main St",
            StateAddress = "MA",
            ZipCode = zip
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.ZipCode).WithErrorMessage("Valid Zip Code required when Street is given");
    }

    [Test]
    public void Zip_Length_0_to_4_Street_Given_Has_Error([Range(0, 4)] int length)
        => Zip_Length_Not_5or9_Street_Given_Has_Error(length);

    [Test]
    public void Zip_Length_6_to_8_Street_Given_Has_Error([Range(6, 8)] int length)
        => Zip_Length_Not_5or9_Street_Given_Has_Error(length);

    [Test]
    public void Zip_Length_10_to_100_Street_Given_Has_Error([Range(10, 100)] int length)
        => Zip_Length_Not_5or9_Street_Given_Has_Error(length);

    private void Zip_Length_Not_5or9_Street_Given_Has_Error(int length)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = "123 Main St",
            ZipCode = new string('J', length)
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.ZipCode).WithErrorMessage("Valid Zip Code required when Street is given");
    }

    [Test]
    public void Zip_Length_5or9_NotNumeric_Country_USA_Street_Given_Has_Error([Values("aaaaa", "bbbbbbbbb")] string zip)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = "123 Main St",
            ZipCode = zip,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.ZipCode).WithErrorMessage("Valid Zip Code required when Street is given");
    }

    [Test]
    public void Zip_Length_5or9_Numeric_Street_Given_Or_Not_Given_Has_No_Error([Values("12345", "123456789")] string zip)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = "123 Main St",
            ZipCode = zip,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(a => a.ZipCode);
    }

    [Test]
    public void Zip_Any_Street_Not_Given_Has_No_Error(
        [Values(null, "", " ", "a", "bb", "ccc", "ddddd", "eeeeeeeee", "12345", "123456789")] string zip,
        [Values(null, "", " ")] string street)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            StreetAddress = street,
            ZipCode = zip,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(a => a.ZipCode);
    }

    [Test]
    public void State_NullOrEmtpy_City_Given_Has_Error([Values(null, "", " ")] string state)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            CityAddress = "Boston",
            StateAddress = state,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.StateAddress).WithErrorMessage("State required when City is given");
    }

    [Test]
    public void EmailAddress_Valid_Has_No_validator_Error([Values("email@domain1.com",
                                                                       "firstname.lastname@domain2.com",
                                                                       "email@subdomain.domain3.com",
                                                                       "firstname+lastname@domain4.com",
                                                                       "1234567890@domain5.com",
                                                                       "email@domain-one6.com",
                                                                       "_______@domain7.com",
                                                                       "email@domain8.name",
                                                                       "email@domain9.co.jp",
                                                                       "firstname-lastname@domain10.com",
                                                                       "Joe Smith <email@domain.com>",
                                                                       ".email@domain.com",
                                                                       "email.@domain.com",
                                                                       "email..email@domain.com",
                                                                       "email@domain.com (Joe Smith)",
                                                                       "email@domain",
                                                                       "email@-domain.com",
                                                                       "email@111.222.333.44444",
                                                                       "email@domain..com")]string emailAddress)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            EmailAddress = emailAddress,
        };
        
        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(a => a.EmailAddress);
    }

    [TestCase(" ")]
    [TestCase("plainaddress")]
    [TestCase("#@%^%#$@#$@#.com")]
    [TestCase("@domain.com")]
    [TestCase("email.domain.com")]
    [TestCase("email@domain@domain.com")]
    public void EmailAddress_Invalid_NotVerified_validator_Error(string emailAddress)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            EmailAddress = emailAddress,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(a => a.EmailAddress).WithErrorMessage("Invalid Email");
    }

    [Test]
    public void PhoneNumber_NullEmptyWhitespace_NoValidatorError([Values(null, "", "          ")] string phoneNumber)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            PhoneNumber = phoneNumber,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(b => b.PhoneNumber);
    }

    [TestCase("1234567890")]
    [TestCase("123-456-7890")]
    public void PhoneNumber_Valid_NoValidatorError(string phoneNumber)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            PhoneNumber = phoneNumber,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(b => b.PhoneNumber);
    }

    [TestCase("1234567")]
    [TestCase("123-4567")]
    [TestCase("puzzled")]
    public void PhoneNumber_Invalid_HasValidatorError(string phoneNumber)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            PhoneNumber = phoneNumber,
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(b => b.PhoneNumber).WithErrorMessage("Invalid Phone Number");
    }

    [Test]
    public void DateOfBirth_Today_HasValidatorError()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today)
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(b => b.DateOfBirth).WithErrorMessage("Date of Birth must be in the past");
    }

    [Test]
    public void DateOfBirth_Future_HasValidatorError()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(1))
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(b => b.DateOfBirth).WithErrorMessage("Date of Birth must be in the past");
    }

    [Test]
    public void DateOfBirth_Yesterrday_NoValidatorError()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddDays(-1))
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(b => b.DateOfBirth);
    }

    [TestCase(null)]
    public void DateOfBirth_Null_NoValidatorError(DateOnly? dob)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            DateOfBirth = dob
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(b => b.DateOfBirth);
    }

    [TestCase("123-456")]
    [TestCase("123-4567")]
    [TestCase("12-49209498")]
    [TestCase("4920-94090")]
    [TestCase("12345-583")]
    public void USBCId_Valid_NoValidatorError(string usbcId)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = usbcId
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(bowler => bowler.USBCId);
    }

    [TestCase("12345")]
    [TestCase("a39-3840")]
    [TestCase("392-490s0")]
    [TestCase("123-456-789")]
    [TestCase("123-")]
    [TestCase("-123")]
    [TestCase("123-456-")]
    [TestCase("-123-465")]
    public void USBCId_Invalid_HasValidatorError(string usbcId)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = usbcId
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(bowler => bowler.USBCId).WithErrorMessage("Invalid USBC Id");
    }

    [Test]
    public void USBCId_EmptyNull_NoValidationError([Values(null, "")] string usbcId)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            USBCId = usbcId
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(bowler => bowler.USBCId);
    }

    [TestCase("12345678")]
    [TestCase("18754628a")]
    public void SSN_Invalid_HasValidatorError(string ssn)
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            SocialSecurityNumber = ssn.Encrypt()
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(b => b.SocialSecurityNumber).WithErrorMessage("Invalid Social Security Number");
    }

    [Test]
    public void SSN_Valid_NoValidatorError()
    {
        var bowler = new NortheastMegabuck.Models.Bowler
        {
            SocialSecurityNumber = "123456789".Encrypt()
        };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(b => b.SocialSecurityNumber);
    }

    [Test]
    public void SSN_Whitespace_HasValidatorError([Values(" ", "  ", "         ")] string ssn)
    {
        var bowler = new NortheastMegabuck.Models.Bowler { SocialSecurityNumber = ssn.Encrypt() };

        var result = _validator.TestValidate(bowler);
        result.ShouldHaveValidationErrorFor(b => b.SocialSecurityNumber).WithErrorMessage("Invalid Social Security Number");
    }

    [Test]
    public void SSN_NullOrEmpty_NoValidatorError([Values(null, "")] string ssn)
    {
        var bowler = new NortheastMegabuck.Models.Bowler { SocialSecurityNumber = ssn.Encrypt() };

        var result = _validator.TestValidate(bowler);
        result.ShouldNotHaveValidationErrorFor(b => b.SocialSecurityNumber);
    }
}
