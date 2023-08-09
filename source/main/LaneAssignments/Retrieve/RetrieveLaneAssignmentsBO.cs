namespace NortheastMegabuck.LaneAssignments.Retrieve;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IDataLayer _dataLayer;

    public BusinessLogic(IConfiguration config)
    {
        _dataLayer = new DataLayer(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(IDataLayer mockDataLayer)
    {
        _dataLayer = mockDataLayer;
    }

    async Task<IEnumerable<Models.LaneAssignment>> IBusinessLogic.ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
    {
        try
        {
            return (await _dataLayer.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false)).ToList();
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.LaneAssignment>();
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<Models.LaneAssignment>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}