
namespace NortheastMegabuck.Registrations.Retrieve;
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

    async Task<IEnumerable<Models.Registration>> IBusinessLogic.ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        try
        {
            return (await _dataLayer.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false)).ToList();
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.Registration>();
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<Models.Registration>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}