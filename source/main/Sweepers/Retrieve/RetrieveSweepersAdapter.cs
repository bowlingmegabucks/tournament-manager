namespace NortheastMegabuck.Sweepers.Retrieve;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var sweepers = await _businessLogic.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false);

        return sweepers.Select(sweeper => new ViewModel(sweeper));
    }

    public async Task<IViewModel?> ExecuteAsync(SquadId id, CancellationToken cancellationToken)
    {
        var sweeper = await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        return sweeper is not null ? new ViewModel(sweeper) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<IViewModel?> ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}
