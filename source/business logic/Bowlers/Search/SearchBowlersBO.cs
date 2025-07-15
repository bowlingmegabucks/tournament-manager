
namespace NortheastMegabuck.Bowlers.Search;

/// <summary>
/// 
/// </summary>
internal class BusinessLogic : IBusinessLogic
{
    private readonly IDataLayer _dataLayer;

    public BusinessLogic(IDataLayer dataLayer)
    {
        _dataLayer = dataLayer;
    }

    /// <summary>
    /// 
    /// </summary>
    public Models.ErrorDetail? ErrorDetail { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Models.Bowler>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken)
    {
        try
        {
            return (await _dataLayer.ExecuteAsync(searchCriteria, cancellationToken).ConfigureAwait(false)).ToList();
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
    /// <param name="searchCriteria"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<Models.Bowler>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken);
}