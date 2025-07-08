
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Scores.Retrieve;

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

    public async Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.ExecuteAsync(squadIds, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return [];
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<Models.SquadScore>> ExecuteAsync(IEnumerable<SquadId> squadIds, CancellationToken cancellationToken);
}