
namespace NortheastMegabuck.Registrations.Delete;
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

    public void Execute(BowlerId bowlerId, SquadId squadId)
    {
        try
        {
            _dataLayer.Execute(bowlerId, squadId);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    void Execute(BowlerId bowlerId, SquadId squadId);
}