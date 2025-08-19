using FastEndpoints;
using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Api.Registrations.CreateRegistration;

internal sealed class CreateRegistrationValidator
    : Validator<CreateRegistrationRequest>
{
    public CreateRegistrationValidator()
    {
        RuleFor(request => request.Registration)
            .NotNull()
            .WithMessage("Registration details are required.")
            .WithErrorCode("Registration.Required");

        RuleFor(request => request.Registration.TournamentId)
            .Must(tournamentId => Guid.TryParse(tournamentId.Value.ToString(), out var _))
            .WithMessage("Invalid Tournament ID")
            .WithErrorCode("Registration.TournamentIdInvalid");

        RuleFor(request => request.Registration.DivisionId)
            .Must(divisionId => Guid.TryParse(divisionId.Value.ToString(), out var _))
            .WithMessage("Invalid Division ID")
            .WithErrorCode("Registration.DivisionIdInvalid");

        RuleFor(request => request.Registration.Bowler.FirstName)
            .NotEmpty()
            .WithMessage("Bowler's first name is required.")
            .WithErrorCode("Registration.BowlerFirstNameRequired");

        RuleFor(request => request.Registration.Bowler.LastName)
            .NotEmpty()
            .WithMessage("Bowler's last name is required.")
            .WithErrorCode("Registration.BowlerLastNameRequired");

        RuleFor(request => request.Registration.Bowler.Address)
            .NotNull()
            .WithMessage("Bowler's address is required.")
            .WithErrorCode("Registration.BowlerAddressRequired");

        RuleFor(request => request.Registration.Bowler.Email)
            .EmailAddress()
            .WithMessage("A valid email address is required for the bowler.")
            .WithErrorCode("Registration.BowlerEmailInvalid");

        RuleFor(request => request.Registration.Bowler.UsbcId)
            .NotEmpty()
            .WithMessage("USBC ID is required for the bowler.")
            .WithErrorCode("Registration.BowlerUsbcIdRequired");

        RuleFor(request => request.Registration.Squads)
            .NotEmpty()
            .WithMessage("At least one squad must be entered.")
            .WithErrorCode("Registration.SquadsRequired");
    }
}