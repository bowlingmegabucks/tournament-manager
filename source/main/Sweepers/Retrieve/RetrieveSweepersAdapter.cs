namespace NewEnglandClassic.Sweepers.Retrieve;
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
    
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    public IEnumerable<IViewModel> ForTournament(Guid tournamentId)
    {
        var sweepers = _businessLogic.ForTournament(tournamentId);

        return sweepers.Select(sweeper => new ViewModel(sweeper));
    }
}

internal interface IAdapter
{ 
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> ForTournament(Guid tournamentId);
}
