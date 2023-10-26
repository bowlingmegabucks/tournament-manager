
namespace NortheastMegabuck.Registrations.Update;

internal sealed class BusinessLogic : IBusinessLogic
{
    public Models.ErrorDetail? Error { get; private set; }

    private readonly IDataLayer _dataLayer;

    private readonly Retrieve.IBusinessLogic _retrieveBusinessLogic;
    private readonly Tournaments.Retrieve.IBusinessLogic _retrieveTournamentBusinessLogic;

    public BusinessLogic(IConfiguration config)
    {
        _dataLayer = new DataLayer(config);
        _retrieveBusinessLogic = new Retrieve.BusinessLogic(config);
        _retrieveTournamentBusinessLogic = new Tournaments.Retrieve.BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataLayer"></param>
    internal BusinessLogic(IDataLayer mockDataLayer, Retrieve.IBusinessLogic mockRetrieveBusinessLogic, Tournaments.Retrieve.IBusinessLogic mockTournamentBusinessLogic)
    {
        _dataLayer = mockDataLayer;
        _retrieveBusinessLogic = mockRetrieveBusinessLogic;
        _retrieveTournamentBusinessLogic = mockTournamentBusinessLogic;
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

        var tournament = await _retrieveTournamentBusinessLogic.ExecuteAsync(registration.Id, cancellationToken).ConfigureAwait(false);

        if (_retrieveBusinessLogic.Error is not null)
        {
            Error = _retrieveTournamentBusinessLogic.Error;

            return;
        }

        if (!tournament!.Sweepers.Any())
        { 
            Error = new Models.ErrorDetail("Cannot add super sweeper to tournament without sweepers.");

            return;
        }
        
        if (tournament!.Sweepers.Count() != tournament.Sweepers.Count())
        {
            Error = new Models.ErrorDetail("Bowler is not registered for all sweepers.");

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
