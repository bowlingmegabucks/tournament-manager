
using ErrorOr;

namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;

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

    public BusinessLogic(IDataLayer dataLayer)
    {
        _dataLayer = dataLayer;
    }

    async Task<Models.Tournament?> IBusinessLogic.ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
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

    async Task<Models.Tournament?> IBusinessLogic.ExecuteAsync(DivisionId id, CancellationToken cancellationToken)
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

    async Task<Models.Tournament?> IBusinessLogic.ExecuteAsync(SquadId id, CancellationToken cancellationToken)
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

    async Task<Models.Tournament?> IBusinessLogic.ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
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
    Task<Models.Tournament?> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.Tournament?> ExecuteAsync(DivisionId id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.Tournament?> ExecuteAsync(SquadId id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Models.Tournament?> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}