namespace BowlingMegabucks.TournamentManager.Squads.Retrieve;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var squads = await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        return squads.Select(squad => new ViewModel(squad));
    }

    public async Task<IViewModel?> ExecuteAsync(SquadId id, CancellationToken cancellationToken)
    {
        var squad = await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        return squad is not null ? new ViewModel(squad) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}
