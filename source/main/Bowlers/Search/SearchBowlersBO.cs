namespace NortheastMegabuck.Bowlers.Search;
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

    public IEnumerable<Models.Bowler> Execute(Models.BowlerSearchCriteria searchCriteria)
    {
        try
        {
            return _dataLayer.Execute(searchCriteria).ToList();
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.Bowler>();
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.Bowler> Execute(Models.BowlerSearchCriteria searchCriteria);
}