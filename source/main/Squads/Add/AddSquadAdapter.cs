namespace NewEnglandClassic.Squads.Add;
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
    
    public Guid? Execute(IViewModel viewModel)
    {
        var model = new Models.Squad(viewModel);

        var guid = _businessLogic.Execute(model);

        Errors = _businessLogic.Errors;

        return guid;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Guid? Execute(IViewModel squad);
}