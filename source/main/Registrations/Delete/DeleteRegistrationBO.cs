
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

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
    {
        try
        {
            await _dataLayer.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);
        }
    }

    public void Execute(RegistrationId id)
    {
        try
        {
            _dataLayer.Execute(id);
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

    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    void Execute(RegistrationId id);
}