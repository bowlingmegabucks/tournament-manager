
namespace NortheastMegabuck.LaneAssignments.Update;
internal class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IDataLayer _dataLayer;
    private readonly Retrieve.IBusinessLogic _retrieveLaneAssignment;

    public BusinessLogic(IConfiguration config)
    {
        _dataLayer = new DataLayer(config);
        _retrieveLaneAssignment = new Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataLayer"></param>
    /// <param name="mockRetrieveLaneAssignment"></param>
    internal BusinessLogic(IDataLayer mockDataLayer, Retrieve.IBusinessLogic mockRetrieveLaneAssignment)
    {
        _dataLayer = mockDataLayer;
        _retrieveLaneAssignment = mockRetrieveLaneAssignment;
    }

    async Task IBusinessLogic.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string originalPosition, string updatedPosition, CancellationToken cancellationToken)
    {
        var existingLaneAssignment = await _retrieveLaneAssignment.ExecuteAsync(squadId, bowlerId, cancellationToken).ConfigureAwait(false);

        if (existingLaneAssignment is null)
        {
            Error = new Models.ErrorDetail($"Bowler is not registered for squad");
            return;
        }

        if (existingLaneAssignment.Position != originalPosition)
        {
            Error = new Models.ErrorDetail($"Bowler has already been moved from {originalPosition}.  Please refresh and verify correct lane assignment");
            return;
        }

        try
        {
            await _dataLayer.ExecuteAsync(squadId, bowlerId, updatedPosition, cancellationToken).ConfigureAwait(false);
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

    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string originalPosition, string updatedPosition, CancellationToken cancellationToken);
}