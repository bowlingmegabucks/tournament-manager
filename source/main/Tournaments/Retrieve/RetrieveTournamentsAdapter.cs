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

    public IViewModel? Execute(TournamentId tournamentId)
    {
        var tournament = BusinessLogic.Execute(tournamentId);

        return tournament != null ? new ViewModel(tournament) : null;
    }

    public IViewModel? Execute(SquadId squadId)
    {
        var tournament = BusinessLogic.Execute(squadId);

        return tournament != null ? new ViewModel(tournament) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(CancellationToken cancellationToken);

    IViewModel? Execute(TournamentId tournamentId);

    IViewModel? Execute(SquadId squadId);
}