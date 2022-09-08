
namespace NortheastMegabuck.LaneAssignments.Update;
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

    void IBusinessLogic.Execute(NortheastMegabuck.SquadId squadId, NortheastMegabuck.BowlerId bowlerId, string position)
    {
        try
        {
            _dataLayer.Execute(squadId, bowlerId, position);
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

    void Execute(SquadId squadId, BowlerId bowlerId, string position);
}