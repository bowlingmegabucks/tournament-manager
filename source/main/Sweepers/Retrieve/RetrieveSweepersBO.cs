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

    public IEnumerable<Models.Sweeper> Execute(TournamentId tournamentId)
    {
        try
        {
            return _dataLayer.Execute(tournamentId).ToList();
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

    public IEnumerable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId)
    {
        try
        {
            return _dataLayer.SuperSweeperBowlers(tournamentId).ToList();
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

    IEnumerable<Models.Sweeper> Execute(TournamentId tournamentId);

    Models.Sweeper? Execute(SquadId id);

    IEnumerable<BowlerId> SuperSweeperBowlers(TournamentId tournamentId);
}