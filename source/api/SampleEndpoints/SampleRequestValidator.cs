using FastEndpoints;
using FluentValidation;

namespace NortheastMegabuck.Api.SampleEndpoints;

internal sealed class SampleRequestValidator
    : Validator<SampleRequest>
{
    public SampleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Age)
            .GreaterThan(0)
            .WithMessage("Age must be greater than 0.")
            .LessThan(120)
            .WithMessage("Age must be less than 120.");
    }
}