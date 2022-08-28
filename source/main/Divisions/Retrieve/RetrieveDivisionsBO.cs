namespace NewEnglandClassic.Divisions.Retrieve;
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

    public IEnumerable<Models.Division> Execute(TournamentId tournamentId)
    {
        try
        {
            return _dataLayer.Execute(tournamentId).ToList();
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.Division>();
        }
    }

    public Models.Division? Execute(NewEnglandClassic.Divisions.Id id)
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

    IEnumerable<Models.Division> Execute(TournamentId tournamentId);

    Models.Division? Execute(NewEnglandClassic.Divisions.Id id);
}