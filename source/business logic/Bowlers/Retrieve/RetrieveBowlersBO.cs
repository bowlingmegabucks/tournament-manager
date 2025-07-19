
namespace BowlingMegabucks.TournamentManager.Bowlers.Retrieve;

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
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Models.Bowler?> ExecuteAsync(BowlerId id, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            ErrorDetail = new Models.ErrorDetail(ex);

            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrationId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Models.Bowler?> ExecuteAsync(RegistrationId registrationId, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(registrationId, cancellationToken).ConfigureAwait(false);
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
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.Bowler?> ExecuteAsync(BowlerId id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="registrationId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.Bowler?> ExecuteAsync(RegistrationId registrationId, CancellationToken cancellationToken);
}