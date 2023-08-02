namespace NortheastMegabuck.Sweepers.Add;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = Enumerable.Empty<Models.ErrorDetail>();
    
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
    
    public SquadId? Execute(IViewModel sweeper)
    {
        var model = new Models.Sweeper(sweeper);

        var guid = _businessLogic.Execute(model);

        Errors = _businessLogic.Errors;

        return guid;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    SquadId? Execute(IViewModel sweeper);
}