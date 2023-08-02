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
            
            return Enumerable.Empty<Models.Tournament>();
        }
    }

    Models.Tournament? IBusinessLogic.Execute(TournamentId id)
    {
        try
        {
            return _dataLayer.Execute(id);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return null;
        }
    }

    Models.Tournament? IBusinessLogic.Execute(DivisionId id)
    {
        try
        {
            return _dataLayer.Execute(id);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return null;
        }
    }

    Models.Tournament? IBusinessLogic.Execute(SquadId id)
    {
        try
        {
            return _dataLayer.Execute(id);
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

    Models.Tournament? Execute(TournamentId id);

    Models.Tournament? Execute(DivisionId id);

    Models.Tournament? Execute(SquadId id);
}