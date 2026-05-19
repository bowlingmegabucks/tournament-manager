using FluentValidation;

namespace BowlingMegabucks.TournamentManager.Scores.Update;

internal class Validator : AbstractValidator<IEnumerable<Models.SquadScore>>
{
    public Validator()
    {
        RuleForEach(squadScores => squadScores).SetValidator(new SquadScoreValidator());

        RuleFor(squadScores => squadScores)
            .Must(squadScores => squadScores.Count() == squadScores.Max(squadScore => squadScore.GameNumber))
            .WithMessage("Missing game for bowler");
        RuleFor(squadScores => squadScores)
            .Must(squadScores => squadScores.Select(squadScore => squadScore.Bowler.Id).Distinct().Count() == 1)
            .When(squadScores => squadScores.All(squadScore => squadScore.Bowler.Id != BowlerId.Empty))
            .WithMessage("Scores must be for the same bowler");
        RuleFor(squadScores => squadScores)
            .Must(squadScores => squadScores.Select(squadScore => squadScore.SquadId).Distinct().Count() == 1)
            .When(squadScores => squadScores.All(squadScore => squadScore.Bowler.Id != BowlerId.Empty))
            .WithMessage("Scores must be for the same squad");
        RuleFor(squadScores => squadScores)
            .Must(squadScores => squadScores.Select(squadScore => squadScore.GameNumber).Distinct().Count() == squadScores.Select(squadScore => squadScore.GameNumber).Count())
            .WithMessage("Duplicate game for bowler");
    }

    private sealed class SquadScoreValidator : AbstractValidator<Models.SquadScore>
    {
        public SquadScoreValidator()
        {
            RuleFor(squadScore => squadScore.Bowler.Id)
                .Must(bowlerId => bowlerId != BowlerId.Empty)
                .WithMessage("Bowler Id is required");

            RuleFor(squadScore => squadScore.SquadId)
                .Must(squadId => squadId != SquadId.Empty)
                .WithMessage("Squad Id is required");

            RuleFor(squadScore => squadScore.GameNumber)
                .Must(gameNumber => gameNumber > 0)
                .WithMessage("Game number must be greater than zero");

            RuleFor(squadScore => squadScore.Score)
                .InclusiveBetween(1, 300);
        }
    }
}