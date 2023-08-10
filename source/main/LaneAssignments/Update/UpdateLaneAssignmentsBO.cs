
namespace NortheastMegabuck.LaneAssignments.Update;
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

    async Task IBusinessLogic.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken)
    {
        try
        {
            await _dataLayer.ExecuteAsync(squadId, bowlerId, position, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string position, CancellationToken cancellationToken);
}