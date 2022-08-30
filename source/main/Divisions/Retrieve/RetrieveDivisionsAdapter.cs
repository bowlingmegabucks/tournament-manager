namespace NortheastMegabuck.Divisions.Retrieve;
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
        var divisions = _businessLogic.Execute(tournamentId);

        return divisions.Select(division => new ViewModel(division));
    }
}

internal interface IAdapter
{ 
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(TournamentId tournamentId);
}
