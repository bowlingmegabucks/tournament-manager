
namespace NortheastMegabuck.LaneAssignments.Update;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    public IBusinessLogic _businessLogic;

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

    async Task IAdapter.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken);
}