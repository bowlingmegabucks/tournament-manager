using FluentValidation;

namespace NortheastMegabuck.Bowlers.Add;
internal class Validator : Bowlers.Validator
{
    public Validator()
    {
        RuleFor(bowler => bowler.Id).Must(id => id == BowlerId.Empty).WithMessage("Bowler id must be empty");
    }
}