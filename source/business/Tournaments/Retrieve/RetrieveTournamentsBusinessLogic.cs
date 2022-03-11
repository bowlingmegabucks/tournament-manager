using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic.Tournaments.Retrieve;
public class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? ErrorDetail { get; private set; }

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
            ErrorDetail = new Models.ErrorDetail(ex);
            
            return Enumerable.Empty<Models.Tournament>();
        }
    }
}

public interface IBusinessLogic
{
    Models.ErrorDetail? ErrorDetail { get; }

    IEnumerable<Models.Tournament> Execute();
}