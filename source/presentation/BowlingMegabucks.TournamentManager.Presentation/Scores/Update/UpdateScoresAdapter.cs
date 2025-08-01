﻿namespace BowlingMegabucks.TournamentManager.Scores.Update;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public IEnumerable<Models.ErrorDetail> Errors
        => _businessLogic.Errors;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(IEnumerable<IViewModel> scores, CancellationToken cancellationToken)
    {
        var models = scores.Select(squadScore => squadScore.ToModel()).ToList();

        var invalidScores = await _businessLogic.ExecuteAsync(models, cancellationToken).ConfigureAwait(false);

        return invalidScores.Select(score => new ViewModel(score));
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(IEnumerable<IViewModel> scores, CancellationToken cancellationToken);
}