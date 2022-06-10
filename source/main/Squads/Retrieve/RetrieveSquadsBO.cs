namespace NewEnglandClassic.Squads.Retrieve;
internal class BusinessLogic : IBusinessLogic
{
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

    public Models.ErrorDetail? Error { get; private set; }

    public IEnumerable<Models.Squad> ForTournament(Guid tournamentId)
    {
        try
        {
            return _dataLayer.ForTournament(tournamentId).ToList();
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.Squad>();
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.Squad> ForTournament(Guid tournamentId);
}