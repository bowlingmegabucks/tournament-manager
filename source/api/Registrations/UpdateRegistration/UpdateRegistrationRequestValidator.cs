using FastEndpoints;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.UpdateRegistration;

internal sealed class UpdateRegistrationRequestValidator
    : Validator<UpdateRegistrationRequest>
{
    public UpdateRegistrationRequestValidator()
    {
        RuleFor(request => request.RegistrationId)
            .NotNull()
            .WithMessage("Registration ID is required.")
            .WithErrorCode("Registration.IdRequired");

        RuleFor(request => request.Registration)
            .NotNull()
            .WithMessage("Registration details are required.")
            .WithErrorCode("Registration.Required");

        RuleFor(request => request.RegistrationId)
            .Must((request, id) => request.RegistrationId == request.Registration.RegistrationId)
            .WithMessage("Registration ID in the request does not match the ID in the registration details.")
            .WithErrorCode("Registration.IdMismatch");
    }
}