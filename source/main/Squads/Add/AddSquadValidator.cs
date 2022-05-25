using FluentValidation;

namespace NewEnglandClassic.Squads.Add;

internal class Validator : AbstractValidator<Models.Squad>
{
    internal Validator()
    {
        RuleFor(squad => squad.TournamentId).Must(id => id != Guid.Empty).WithMessage("Tournament Id is required");
        RuleFor(squad => squad.TournamentId).Equal(squad => squad.Tournament.Id).WithMessage("Tournament Id does not match");

        RuleFor(squad => squad.FinalsRatio).GreaterThan(1).When(squad=> squad.FinalsRatio != null).WithMessage("Finals ratio must be greater than 1");

        RuleFor(squad => squad.CashRatio).GreaterThan(1).When(squad=> squad.CashRatio != null).WithMessage("Cash ratio must be greater than 1");

        RuleFor(squad => squad.Date).GreaterThanOrEqualTo(squad => squad.Tournament.Start.ToDateTime(TimeOnly.MinValue)).WithMessage("Squad date must be after tournament start");
        RuleFor(squad => squad.Date).LessThan(squad => squad.Tournament.End.ToDateTime(TimeOnly.MaxValue)).WithMessage("Squad date must be before tournament end");

        RuleFor(squad => squad.MaxPerPair).GreaterThan(0).WithMessage("Max per pair must be greater than 0");

        RuleFor(squad => squad.Complete).Must(complete => !complete).WithMessage("Cannot add a completed squad");
    }
}
