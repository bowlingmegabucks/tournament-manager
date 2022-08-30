namespace NortheastMegabuck.Sweepers.Retrieve;
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

    public IEnumerable<IViewModel> Execute(TournamentId tournamentId)
    {
        var sweepers = _businessLogic.Execute(tournamentId);

        return sweepers.Select(sweeper => new ViewModel(sweeper));
    }
}

internal interface IAdapter
{ 
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(TournamentId tournamentId);
}
