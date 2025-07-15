
namespace NortheastMegabuck.Scores.Retrieve;

/// <summary>
/// 
/// </summary>
internal class BusinessLogic : IBusinessLogic
{
    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    private readonly IDataLayer _dataLayer;

    public BusinessLogic(IDataLayer dataLayer)
    {
        _dataLayer = dataLayer;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="squadIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(squadIds, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ErrorDetail = new Models.ErrorDetail(ex);

            return [];
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
    /// <param name="squadIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken);
}