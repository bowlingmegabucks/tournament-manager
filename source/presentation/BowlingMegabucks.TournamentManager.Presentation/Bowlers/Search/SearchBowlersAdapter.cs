﻿namespace BowlingMegabucks.TournamentManager.Bowlers.Search;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken)
    {
        var bowlers = await _businessLogic.ExecuteAsync(searchCriteria, cancellationToken).ConfigureAwait(false);

        return bowlers.Select(bowler => new ViewModel(bowler));
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken);
}
