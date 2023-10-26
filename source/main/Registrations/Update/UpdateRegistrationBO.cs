
namespace NortheastMegabuck.Registrations.Update;

internal sealed class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IDataLayer _dataLayer;

    private readonly Retrieve.IBusinessLogic _retrieveBusinessLogic;

    public BusinessLogic(IConfiguration config)
    {
        _dataLayer = new DataLayer(config);
        _retrieveBusinessLogic = new Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(IDataLayer mockDataLayer, Retrieve.IBusinessLogic mockRetrieveBusinessLogic)
    {
        _dataLayer = mockDataLayer;
        _retrieveBusinessLogic = mockRetrieveBusinessLogic;
    }

    public async Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
    {
        var registration = await _retrieveBusinessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);

        if (_retrieveBusinessLogic.Error is not null)
        {
            Error = _retrieveBusinessLogic.Error;

            return;
        }

        if (registration!.Sweepers.Any(sweeper => sweeper.Complete))
        { 
            Error = new Models.ErrorDetail("Cannot add super sweeper to registration with completed sweepers.");

            return;
        }

        if (registration.SuperSweeper)
        {
            Error = new Models.ErrorDetail("Bowler is already registered for the super sweeper.");

            return;
        }

        await _dataLayer.ExecuteAsync(id, true, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken);
}
