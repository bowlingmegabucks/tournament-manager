
namespace NortheastMegabuck.LaneAssignments.Update;

/// <summary>
/// 
/// </summary>
internal sealed class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly IDataLayer _dataLayer;
    private readonly Retrieve.IBusinessLogic _retrieveLaneAssignment;

    public BusinessLogic(IDataLayer dataLayer, Retrieve.IBusinessLogic retrieveLaneAssignment)
    {
        _dataLayer = dataLayer;
        _retrieveLaneAssignment = retrieveLaneAssignment;
    }

    async Task IBusinessLogic.ExecuteAsync(SquadId squadId, BowlerId bowlerId, string originalPosition, string updatedPosition, CancellationToken cancellationToken)
    {
        var bowlerExistingAssignment = await _retrieveLaneAssignment.ExecuteAsync(squadId, bowlerId, cancellationToken).ConfigureAwait(false);

        if (bowlerExistingAssignment is null)
        {
            ErrorDetail = new Models.ErrorDetail($"Bowler is not registered for squad");
            return;
        }

        if (bowlerExistingAssignment.Position == updatedPosition)
        {
            return;
        }

        if (bowlerExistingAssignment.Position != originalPosition)
        {
            ErrorDetail = new Models.ErrorDetail($"Bowler has already been moved from {originalPosition}.  Please refresh and verify correct lane assignment");
            return;
        }

        var existingLaneAssignments = await _retrieveLaneAssignment.ExecuteAsync(squadId, cancellationToken).ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(updatedPosition) && existingLaneAssignments.Any(laneAssignment => laneAssignment.Position == updatedPosition))
        {
            ErrorDetail = new Models.ErrorDetail($"Lane {updatedPosition} is already assigned.  Please refresh for updated lane assignments");
            return;
        }

        try
        {
            await _dataLayer.ExecuteAsync(squadId, bowlerId, updatedPosition, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ErrorDetail = new Models.ErrorDetail(ex);
        }
    }
}

/// <summary>
/// 
/// </summary>
public interface IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    Models.ErrorDetail? ErrorDetail { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadId"></param>
    /// <param name="bowlerId"></param>
    /// <param name="originalPosition"></param>
    /// <param name="updatedPosition"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task ExecuteAsync(SquadId squadId, BowlerId bowlerId, string originalPosition, string updatedPosition, CancellationToken cancellationToken);
}