
namespace BowlingMegabucks.TournamentManager.LaneAssignments.Update;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    public IBusinessLogic _businessLogic;
    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    async Task IAdapter.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string originalPosition, string updatedPosition, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(squadId, bowlerId, originalPosition, updatedPosition, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string originalPosition, string updatedPosition, CancellationToken cancellationToken);
}