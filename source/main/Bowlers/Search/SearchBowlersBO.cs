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

    public async Task<IEnumerable<Models.Bowler>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken)
    {
        try
        {
            return (await _dataLayer.ExecuteAsync(searchCriteria, cancellationToken).ConfigureAwait(false)).ToList();
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

    Task<IEnumerable<Models.Bowler>> ExecuteAsync(Models.BowlerSearchCriteria searchCriteria, CancellationToken cancellationToken);
}