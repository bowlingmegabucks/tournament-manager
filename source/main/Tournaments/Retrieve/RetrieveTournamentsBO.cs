namespace NortheastMegabuck.Tournaments.Retrieve;
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

    async Task<IEnumerable<Models.Tournament>> IBusinessLogic.ExecuteAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return [];
        }
    }

    async Task<Models.Tournament?> IBusinessLogic.ExecuteAsync(TournamentId id, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

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
            Error = new Models.ErrorDetail(ex);

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
            Error = new Models.ErrorDetail(ex);

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
            Error = new Models.ErrorDetail(ex);

            return null;
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<Models.Tournament>> ExecuteAsync(CancellationToken cancellationToken);

    Task<Models.Tournament?> ExecuteAsync(TournamentId id, CancellationToken cancellationToken);

    Task<Models.Tournament?> ExecuteAsync(DivisionId id, CancellationToken cancellationToken);

    Task<Models.Tournament?> ExecuteAsync(SquadId id, CancellationToken cancellationToken);

    Task<Models.Tournament?> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}