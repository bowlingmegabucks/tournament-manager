namespace NewEnglandClassic.Tournaments.Retrieve;
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

    IEnumerable<Models.Tournament> IBusinessLogic.Execute()
    {
        try
        {
            return _dataLayer.Execute();
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
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.Tournament> Execute();

    Models.Tournament? Execute(TournamentId id);

    Models.Tournament? Execute(DivisionId id);
}