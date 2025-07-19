
namespace BowlingMegabucks.TournamentManager.Squads.Complete;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}