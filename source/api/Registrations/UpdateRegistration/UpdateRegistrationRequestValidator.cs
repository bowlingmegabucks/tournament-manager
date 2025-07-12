using FastEndpoints;
using FluentValidation;

namespace NortheastMegabuck.Api.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationRequestValidator
    : Validator<UpdateRegistrationRequest>
{
    public UpdateRegistrationRequestValidator()
    {
        RuleFor(request => request.Id)
            .NotNull()
            .WithMessage("Registration ID is required.")
            .WithErrorCode("Registration.IdRequired");

        RuleFor(request => request.Registration)
            .NotNull()
            .WithMessage("Registration details are required.")
            .WithErrorCode("Registration.Required");

        RuleFor(request => request.Id)
            .Must((request, id) => request.Id == request.Registration.RegistrationId)
            .WithMessage("Registration ID in the request does not match the ID in the registration details.")
            .WithErrorCode("Registration.IdMismatch");
    }
}