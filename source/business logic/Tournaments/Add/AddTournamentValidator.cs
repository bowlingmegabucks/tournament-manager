using FluentValidation;

namespace NortheastMegabuck.Tournaments.Add;

internal class Validator : AbstractValidator<Models.Tournament>
{
    public Validator()
    {
        RuleFor(tournament => tournament.Name).NotEmpty().WithMessage("Name is required");

        RuleFor(tournament => tournament.Start).Must((tournament, start) => start <= tournament.End).WithMessage("Start date must be before end date");
        RuleFor(tournament => tournament.End).Must((tournament, end) => end >= tournament.Start).WithMessage("End date must be after start date");

        RuleFor(tournament => tournament.EntryFee).GreaterThan(0).WithMessage("Entry fee must be greater than $0");

        RuleFor(tournament => tournament.Games).Must(games => games > 0).WithMessage("Must have at least one game");

        RuleFor(tournament => tournament.FinalsRatio).GreaterThan(1).WithMessage("Finals ratio must be greater than 1");

        RuleFor(tournament => tournament.CashRatio).GreaterThan(1).WithMessage("Cash ratio must be greater than 1");

        RuleFor(tournament => tournament.BowlingCenter).NotEmpty().WithMessage("Bowling center is required");

        RuleFor(tournament => tournament.Completed).Must(complete => !complete).WithMessage("Tournament is already completed");
    }
}
