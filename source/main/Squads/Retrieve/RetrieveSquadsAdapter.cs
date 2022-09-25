namespace NortheastMegabuck.Squads.Retrieve;
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
        var squads = _businessLogic.Execute(tournamentId);

        return squads.Select(squad => new ViewModel(squad));
    }

    public IViewModel? Execute(SquadId id)
    {
        var squad = _businessLogic.Execute(id);

        return squad is not null ? new ViewModel(squad) : null;
    }
}

internal interface IAdapter
{ 
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(TournamentId tournamentId);

    IViewModel? Execute(SquadId id);
}
