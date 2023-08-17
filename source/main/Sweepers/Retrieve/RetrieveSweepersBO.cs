using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Sweepers.Retrieve;
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

    public async Task<IEnumerable<Models.Sweeper>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        try
        {
            return (await _dataLayer.ExecuteAsync(tournamentId, cancellationToken).ConfigureAwait(false)).ToList();
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.Sweeper>();
        }
    }

    public Models.Sweeper? Execute(SquadId id)
    {
        try
        {
            return _dataLayer.Execute(id);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return null;
        }
    }

    public async Task<IEnumerable<BowlerId>> SuperSweeperBowlersAsync(TournamentId tournamentId, CancellationToken cancellationToken)
    {
        try
        {
            return await _dataLayer.SuperSweeperBowlers(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<BowlerId>();
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<Models.Sweeper>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Models.Sweeper? Execute(SquadId id);

    Task<IEnumerable<BowlerId>> SuperSweeperBowlersAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}