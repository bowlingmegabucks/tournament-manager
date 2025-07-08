using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.LaneAssignments.Retrieve;

/// <summary>
/// 
/// </summary>
public sealed class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly IDataLayer _dataLayer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
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
            ErrorDetail = new Models.ErrorDetail(ex);

            return [];
        }
    }

    async Task<Models.LaneAssignment?> IBusinessLogic.ExecuteAsync(SquadId squadId, BowlerId bowlerId, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(squadId, bowlerId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ErrorDetail = new Models.ErrorDetail(ex);
            return null;
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
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.LaneAssignment>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadId"></param>
    /// <param name="bowlerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.LaneAssignment?> ExecuteAsync(SquadId squadId, BowlerId bowlerId, CancellationToken cancellationToken);
}