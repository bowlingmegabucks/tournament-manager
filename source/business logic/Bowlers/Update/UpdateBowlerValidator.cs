using FluentValidation;

namespace NortheastMegabuck.Bowlers.Update;
internal class Validator 
    : Bowlers.Validator, IUpdateBowlerValidator
{
    public Validator()
    {
        RuleFor(bowler => bowler.Id).Must(id => id != BowlerId.Empty).WithMessage("Bowler id must not be empty");
    }
}

internal interface IUpdateBowlerValidator 
    : IValidator<Models.Bowler>;
