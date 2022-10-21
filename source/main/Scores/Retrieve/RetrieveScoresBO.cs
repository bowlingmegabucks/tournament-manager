
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

    public IEnumerable<Models.SquadScore> Execute(SquadId squadId)
    {
        try
        {
            return _dataLayer.Execute(squadId);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.SquadScore>();
        }
    }

    public IEnumerable<Models.SquadScore> SuperSweeper(TournamentId tournamentId)
    {
        try
        {
            return _dataLayer.SuperSweeper(tournamentId);
        }
        catch (Exception ex)
        {
            Error = new Models.ErrorDetail(ex);

            return Enumerable.Empty<Models.SquadScore>();
        }
    }
}

internal interface IBusinessLogic
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<Models.SquadScore> Execute(SquadId squadId);

    IEnumerable<Models.SquadScore> SuperSweeper(TournamentId tournamentId);
}