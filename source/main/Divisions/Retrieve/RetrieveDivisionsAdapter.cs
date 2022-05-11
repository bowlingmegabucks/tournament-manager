namespace NewEnglandClassic.Divisions.Retrieve;
internal class Adapter : IAdapter
{
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

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();

    public IEnumerable<IViewModel> ForTournament(Guid tournamentId)
    {
        var divisions = _businessLogic.ForTournament(tournamentId);

        Errors = _businessLogic.Errors;

        return divisions.Select(division => new ViewModel(division));
    }
}

internal interface IAdapter
{ 
    IEnumerable<Models.ErrorDetail> Errors { get; }

    IEnumerable<IViewModel> ForTournament(Guid tournamentId);
}
