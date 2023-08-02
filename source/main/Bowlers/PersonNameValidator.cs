using FluentValidation;

namespace NortheastMegabuck.Bowlers;

internal class PersonNameValidator : AbstractValidator<Models.PersonName>
{
    public PersonNameValidator()
    {
        RuleFor(name => name.First).NotEmpty().WithMessage("First Name is Required");
        RuleFor(name => name.MiddleInitial).Must(initial => string.IsNullOrEmpty(initial) || initial.Length == 1).WithMessage("Middle Initial must only be 1 character");
        RuleFor(name => name.Last).NotEmpty().WithMessage("Last Name is Required");
    }
}
