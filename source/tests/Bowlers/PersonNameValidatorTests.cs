using FluentValidation;
using FluentValidation.TestHelper;

namespace BowlingMegabucks.TournamentManager.Tests.names;

[TestFixture]
internal sealed class PersonNameValidator
{

    private IValidator<BowlingMegabucks.TournamentManager.Models.PersonName> _validator;

    [OneTimeSetUp]
    public void SetUp()
        => _validator = new BowlingMegabucks.TournamentManager.Bowlers.PersonNameValidator();

    [Test]
    public void First_NullWhitespace_HasValidatorError([Values(null, "", " ")] string First)
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = First };

        var result = _validator.TestValidate(name);
        result.ShouldHaveValidationErrorFor(b => b.First).WithErrorMessage("First Name is Required");
    }

    [Test]
    public void First_NotNullOrWhitespace_NoValidatorError()
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { First = "John" };

        var result = _validator.TestValidate(name);
        result.ShouldNotHaveValidationErrorFor(b => b.First);
    }

    [Test]
    public void MiddleInitial_NullEmpty_HasNoValidatorError([Values(null, "")] string middleInitial)
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { MiddleInitial = middleInitial };

        var result = _validator.TestValidate(name);
        result.ShouldNotHaveValidationErrorFor(b => b.MiddleInitial);
    }

    [Test]
    public void MiddleInitial_Length1_HasNoValidatorError()
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { MiddleInitial = "J" };

        var result = _validator.TestValidate(name);
        result.ShouldNotHaveValidationErrorFor(b => b.MiddleInitial);
    }

    [Test]
    public void MiddleInitial_LengthGreaterThan1_HasValidatorError([Range(2, 10)] int length)
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { MiddleInitial = new string('J', length) };

        var result = _validator.TestValidate(name);
        result.ShouldHaveValidationErrorFor(b => b.MiddleInitial).WithErrorMessage("Middle Initial must only be 1 character");
    }

    [Test]
    public void Last_NullWhitespace_HasValidatorError([Values(null, "", " ")] string Last)
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { Last = Last };

        var result = _validator.TestValidate(name);
        result.ShouldHaveValidationErrorFor(b => b.Last).WithErrorMessage("Last Name is Required");
    }

    [Test]
    public void Last_NotNullWhitespace_NoValidatorError()
    {
        var name = new BowlingMegabucks.TournamentManager.Models.PersonName { Last = "Doe" };

        var result = _validator.TestValidate(name);
        result.ShouldNotHaveValidationErrorFor(b => b.Last);
    }
}
