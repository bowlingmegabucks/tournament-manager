using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Divisions.Add;
internal class Validator : AbstractValidator<Models.Division>
{
    public Validator()
    {
        RuleFor(division => division.Number).Must(number => number > 0).WithMessage("Division number must be greater than 0");

        RuleFor(division => division.Name).NotEmpty().WithMessage("Division name is required");

        RuleFor(division => division.TournamentId).Must(tournamentId => tournamentId != TournamentId.Empty).WithMessage("Tournament Id is required");

        RuleFor(division => division.MinimumAge).Must(minimumAge => minimumAge > 0)
                                                .When(division => division.MinimumAge.HasValue)
                                                .WithMessage("Minimum age must be greater than 0");
        RuleFor(division => division.MinimumAge).Must((division, minimumAge) => minimumAge <= division.MaximumAge)
                                                .When(division => division.MinimumAge.HasValue && division.MaximumAge.HasValue)
                                                .WithMessage("Minimum age must be less than or equal to maximum age");

        RuleFor(division => division.MaximumAge).Must(maximumAge => maximumAge > 0)
                                                .When(division => division.MaximumAge.HasValue)
                                                .WithMessage("Maximum age must be greater than 0");
        RuleFor(division => division.MaximumAge).Must((division, maximumAge) => maximumAge >= division.MinimumAge)
                                                .When(division => division.MaximumAge.HasValue && division.MinimumAge.HasValue)
                                                .WithMessage("Maximum age must be greater than or equal to minimum age");

        RuleFor(division => division.MinimumAverage).GreaterThan(0)
                                                .When(division => division.MinimumAverage.HasValue)
                                                .WithMessage("Minimum average must be greater than 0");
        RuleFor(division => division.MinimumAverage).LessThan(300)
                                                   .When(division => division.MinimumAverage.HasValue)
                                                    .WithMessage("Minimum average must be less than 300");
        RuleFor(division => division.MinimumAverage).Must((division, minimumAverage) => minimumAverage <= division.MaximumAverage)
                                                    .When(division => division.MinimumAverage.HasValue && division.MaximumAverage.HasValue)
                                                    .WithMessage("Minimum average must be less than or equal to maximum average");

        RuleFor(division => division.MaximumAverage).GreaterThan(0)
                                                   .When(division => division.MaximumAverage.HasValue)
                                                   .WithMessage("Maximum average must be greater than 0");
        RuleFor(division => division.MaximumAverage).LessThan(300)
                                                  .When(division => division.MaximumAverage.HasValue)
                                                  .WithMessage("Maximum average must be less than 300");
        RuleFor(division => division.MaximumAverage).Must((division, maximumAverage) => maximumAverage >= division.MinimumAverage)
                                                    .When(division => division.MaximumAverage.HasValue && division.MinimumAverage.HasValue)
                                                    .WithMessage("Maximum average must be greater than or equal to minimum average");

        RuleFor(division => division.HandicapPercentage).GreaterThanOrEqualTo(0)
                                                       .When(division => division.HandicapPercentage.HasValue)
                                                       .WithMessage("Handicap percentage must be greater than or equal to 0");
        RuleFor(division => division.HandicapPercentage).Must(handicapPercentage => handicapPercentage.HasValue)
                                                       .When(division => division.HandicapBase.HasValue)
                                                       .WithMessage("Handicap percentage must be specified if handicap base is specified");

        RuleFor(division => division.HandicapBase).InclusiveBetween(0, 300)
                                                  .When(division => division.HandicapBase.HasValue)
                                                  .WithMessage("Handicap base must be between 0 and 300");
        RuleFor(division => division.HandicapBase).Must(handicapBase => handicapBase.HasValue)
                                                  .When(division => division.HandicapPercentage.HasValue)
                                                  .WithMessage("Handicap base must be specified if handicap percentage is specified");

        RuleFor(division => division.MaximumHandicapPerGame).GreaterThan(0)
                                                           .When(division => division.MaximumHandicapPerGame.HasValue)
                                                           .WithMessage("Maximum handicap per game must be greater than 0");
        RuleFor(division => division.MaximumHandicapPerGame).Must(maximumHandicapPerGame => !maximumHandicapPerGame.HasValue)
                                                           .When(division => !division.HandicapPercentage.HasValue || !division.HandicapBase.HasValue)
                                                           .WithMessage("Maximum handicap per game can only be specified if both Handicap base and percentage are specified");
    }
}
