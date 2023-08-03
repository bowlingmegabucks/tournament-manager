namespace NortheastMegabuck.Tournaments.Retrieve;
internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public Models.ErrorDetail? Error
        => BusinessLogic.Error;

    internal Adapter(IConfiguration config)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => new BusinessLogic(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => mockBusinessLogic);
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(CancellationToken cancellationToken)
    {
        var tournaments = await BusinessLogic.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        return tournaments.Select(tournament => new ViewModel(tournament)).ToList();
    }

    public async Task<IViewModel?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournament = await BusinessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        return tournament != null ? new ViewModel(tournament) : null;
    }

    public async Task<IViewModel?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var tournament = await BusinessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        return tournament != null ? new ViewModel(tournament) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}