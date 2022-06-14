namespace NewEnglandClassic.Tournaments.Retrieve;
internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public Models.ErrorDetail? Error
        => BusinessLogic.Error;

    internal Adapter(IConfiguration config)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => new BusinessLogic(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => mockBusinessLogic);
    }

    public IEnumerable<IViewModel> Execute()
    {
        var tournaments = BusinessLogic.Execute();

        return tournaments.Select(tournament => new ViewModel(tournament)).ToList();
    }

    public IViewModel? Execute(Guid tournamentId)
    {
        var tournament = BusinessLogic.Execute(tournamentId);

        return tournament != null ? new ViewModel(tournament) : null;
    }
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute();
    IViewModel? Execute(Guid tournamentId);
}