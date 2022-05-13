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
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.Tournament> Execute();
}