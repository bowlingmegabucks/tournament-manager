namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic
        => _businessLogic.Value;

    public Models.ErrorDetail? Error { get; private set; }

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => businessLogic);
    }

    public async Task<IViewModel?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        var tournament = await BusinessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        Error = BusinessLogic.ErrorDetail;

        return tournament != null ? new ViewModel(tournament) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IViewModel?> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}