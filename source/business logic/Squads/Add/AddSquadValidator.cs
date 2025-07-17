using FluentValidation;

namespace NortheastMegabuck.Squads.Add;

internal class Validator : AbstractValidator<Models.Squad>
{
    public Validator()
    {
        RuleFor(squad => squad.TournamentId).Must(id => id != TournamentId.Empty).WithMessage("Tournament Id is required");
        RuleFor(squad => squad.Tournament).NotNull().WithMessage("Tournament is required");
        RuleFor(squad => squad.TournamentId).Equal(squad => squad.Tournament!.Id).When(squad => squad.Tournament != null).WithMessage("Tournament Id does not match");

        RuleFor(squad => squad.FinalsRatio).GreaterThan(1).When(squad => squad.FinalsRatio != null).WithMessage("Finals ratio must be greater than 1");

        RuleFor(squad => squad.CashRatio).GreaterThan(1).When(squad => squad.CashRatio != null).WithMessage("Cash ratio must be greater than 1");

        RuleFor(squad => squad.Date).GreaterThanOrEqualTo(squad => squad.Tournament!.Start.ToDateTime(TimeOnly.MinValue)).When(squad => squad.Tournament != null).WithMessage("Squad date must be after tournament start");
        RuleFor(squad => squad.Date).LessThanOrEqualTo(squad => squad.Tournament!.End.ToDateTime(TimeOnly.MaxValue)).When(squad => squad.Tournament != null).WithMessage("Squad date must be before tournament end");

        RuleFor(squad => squad.MaxPerPair).Must(maxPerPair => maxPerPair > 0).WithMessage("Max per pair must be greater than 0");

        RuleFor(squad => squad.NumberOfLanes).Must(numberOfLanes => numberOfLanes > 0).WithMessage("Number of lanes must be greater than 0");
        RuleFor(squad => squad.NumberOfLanes).Must(numberOfLanes => numberOfLanes % 2 == 0).WithMessage("Number of lanes must be even");

        RuleFor(squad => squad.StartingLane).Must(startingLanes => startingLanes > 0).WithMessage("Starting lane must be greater than 0");
        RuleFor(squad => squad.StartingLane).Must(startingLane => startingLane % 2 == 1).WithMessage("Starting lane must be odd");

        RuleFor(squad => squad.Complete).Must(complete => !complete).WithMessage("Cannot add a completed squad");
    }
}
