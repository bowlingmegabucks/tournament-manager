﻿
namespace BowlingMegabucks.TournamentManager.Sweepers.Results;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    private readonly IBusinessLogic _businessLogic;
    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => Execute(await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false));

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => Execute(await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false));

    private static List<ViewModel> Execute(Models.SweeperResult? result)
    {
        if (result == null)
        {
            return [];
        }

        var scores = result.Scores.ToList();

        var placings = new List<ViewModel>();

        for (short i = 1; i <= scores.Count; i++)
        {
            placings.Add(new ViewModel(scores[i - 1], i, result.CasherCount));
        }

        return placings;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);

    Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}