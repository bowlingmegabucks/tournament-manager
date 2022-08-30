namespace NortheastMegabuck.Bowlers.Search;
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

    public IEnumerable<IViewModel> Execute(Models.BowlerSearchCriteria searchCriteria)
    {
        var bowlers = _businessLogic.Execute(searchCriteria);

        return bowlers.Select(bowler => new ViewModel(bowler));
    }
}

internal interface IAdapter
{ 
    Models.ErrorDetail? Error { get; }

    IEnumerable<IViewModel> Execute(Models.BowlerSearchCriteria searchCriteria);
}
