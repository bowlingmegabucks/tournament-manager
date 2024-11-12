namespace NortheastMegabuck.LaneAssignments.Retrieve;
internal class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public Models.ErrorDetail? Error
        => _businessLogic.Error;

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

    async Task<IEnumerable<IViewModel>> IAdapter.ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => (await _businessLogic.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false)).Select(laneAssignment => new ViewModel(laneAssignment));
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}
