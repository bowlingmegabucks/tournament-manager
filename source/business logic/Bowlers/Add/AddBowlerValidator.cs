using FluentValidation;
using NortheastMegabuck.Models;

namespace NortheastMegabuck.Bowlers.Add;
internal class Validator 
    : Bowlers.Validator, IAddBowlerValidator
{
    public Validator()
    {
        RuleFor(bowler => bowler.Id).Must(id => id == BowlerId.Empty).WithMessage("Bowler id must be empty");
    }
}

internal interface IAddBowlerValidator
    : IValidator<Bowler>;