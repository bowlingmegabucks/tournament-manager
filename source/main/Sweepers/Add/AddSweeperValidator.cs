using FluentValidation;

namespace NewEnglandClassic.Sweepers.Add;
internal class Validator : AbstractValidator<Models.Sweeper>
{
    public Validator()
    {
        RuleFor(sweeper => sweeper.TournamentId).Must(id => id != TournamentId.Empty).WithMessage("Tournament Id is required");
        RuleFor(sweeper => sweeper.Tournament).NotNull().WithMessage("Tournament is required");
        RuleFor(sweeper => sweeper.TournamentId).Equal(sweeper => sweeper.Tournament!.Id).When(sweeper => sweeper.Tournament != null).WithMessage("Tournament Id does not match");

        RuleFor(sweeper => sweeper.CashRatio).GreaterThan(1).WithMessage("Cash ratio must be greater than 1");

        RuleFor(sweeper => sweeper.Date).GreaterThanOrEqualTo(sweeper => sweeper.Tournament!.Start.ToDateTime(TimeOnly.MinValue)).When(sweeper => sweeper.Tournament != null).WithMessage("Sweeper date must be after tournament start");
        RuleFor(sweeper => sweeper.Date).LessThanOrEqualTo(sweeper => sweeper.Tournament!.End.ToDateTime(TimeOnly.MaxValue)).When(sweeper => sweeper.Tournament != null).WithMessage("Sweeper date must be before tournament end");

        RuleFor(sweeper => sweeper.MaxPerPair).Must(maxPerPair => maxPerPair > 0).WithMessage("Max per pair must be greater than 0");

        RuleFor(sweeper => sweeper.NumberOfLanes).Must(numberOfLanes => numberOfLanes > 0).WithMessage("Number of lanes must be greater than 0");

        RuleFor(sweeper => sweeper.StartingLane).Must(startingLanes => startingLanes > 0).WithMessage("Starting lane must be greater than 0");

        RuleFor(sweeper => sweeper.Complete).Must(complete => !complete).WithMessage("Cannot add a completed sweeper");

        RuleFor(sweeper => sweeper.Games).Must(games => games > 0).WithMessage("Games must be greater than 0");

        RuleFor(sweeper => sweeper.EntryFee).GreaterThan(0).WithMessage("Entry fee must be greater than $0");
    }
}
