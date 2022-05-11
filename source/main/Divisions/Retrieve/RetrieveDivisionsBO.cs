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

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    public IEnumerable<Models.Division> ForTournament(Guid tournamentId)
    {
        try
        {
            return _dataLayer.ForTournament(tournamentId).ToList();
        }
        catch (Exception ex)
        {
            Errors = new[] { new Models.ErrorDetail(ex) };

            return Enumerable.Empty<Models.Division>();
        }
    }
}

internal interface IBusinessLogic
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    IEnumerable<Models.Division> ForTournament(Guid tournamentId);
}