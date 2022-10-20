
namespace NortheastMegabuck.Sweepers.Results;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    public IEnumerable<IViewModel> Execute(SquadId squadId)
        => Execute(_businessLogic.Execute(squadId));

    public IEnumerable<IViewModel> Execute(TournamentId tournamentId)
        => Execute(_businessLogic.Execute(tournamentId));

    private IEnumerable<IViewModel> Execute(Models.SweeperCut? result)
    {
        if (result == null)
        {
            return Enumerable.Empty<IViewModel>();
        }

        var scores = result.Scores.ToList();

        var placings = new List<ViewModel>();

        for (short i = 1; i <= scores.Count; i++)
        {
            placings.Add(new ViewModel(scores[i - 1], i, result.CasherCount));
        }

        return placings;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(SquadId squadId);

    IEnumerable<IViewModel> Execute(TournamentId tournamentId);
}