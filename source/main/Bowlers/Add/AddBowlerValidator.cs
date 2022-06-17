using FluentValidation;
using NewEnglandClassic.Validators;

namespace NewEnglandClassic.Bowlers.Add;
internal class Validator : AbstractValidator<Models.Bowler>
{
    public Validator()
    {
        RuleFor(bowler => bowler.FirstName).NotEmpty().WithMessage("First Name is Required");
        RuleFor(bowler => bowler.MiddleInitial).Must(initial => string.IsNullOrEmpty(initial) || initial.Length == 1).WithMessage("Middle Initial must only be 1 character");
        RuleFor(bowler => bowler.LastName).NotEmpty().WithMessage("Last Name is Required");

        RuleFor(bowler => bowler.CityAddress).NotEmpty().When(StreetIsGiven).WithMessage("City is Required when Street is given");
        RuleFor(bowler => bowler.StateAddress).Length(2).When(StreetIsGiven).WithMessage("State must be Postal Abbreviation");
        RuleFor(bowler => bowler.StateAddress)
            .NotEmpty()
            .When(StreetIsGiven)
            .WithMessage("State is required when Street is given");
        RuleFor(bowler => bowler.ZipCode).ZipCode()
            .When(StreetIsGiven)
            .WithMessage("Valid Zip Code required when Street is given");
        RuleFor(bowler => bowler.StateAddress)
            .NotEmpty()
            .When(CityIsGiven)
            .WithMessage("State required when City is given");

        RuleFor(bowler => bowler.EmailAddress).EmailAddress().When(bowler => !string.IsNullOrEmpty(bowler.EmailAddress)).WithMessage("Invalid Email");

        RuleFor(bowler => bowler.PhoneNumber).PhoneNumber().When(bowler => !string.IsNullOrWhiteSpace(bowler.PhoneNumber)).WithMessage("Invalid Phone Number");

        RuleFor(bowler => bowler.DateOfBirth).PastDate().When(bowler => bowler.DateOfBirth.HasValue).WithMessage("Date of Birth must be in the past");

        RuleFor(bowler => bowler.USBCId).Matches(@"^\d+-\d+$").WithMessage("Invalid USBC Id");
    }

    private bool StreetIsGiven(Models.Bowler bowler) => !string.IsNullOrWhiteSpace(bowler.StreetAddress);

    private bool CityIsGiven(Models.Bowler bowler) => !string.IsNullOrWhiteSpace(bowler.CityAddress);
}
