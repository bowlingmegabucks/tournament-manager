namespace NortheastMegabuck.Squads.Add;
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
    
    public async Task<SquadId?> ExecuteAsync(IViewModel squad, CancellationToken cancellationToken)
    {
        var model = new Models.Squad(squad);

        var id = await _businessLogic.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        Errors = _businessLogic.Errors;

        return id;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<SquadId?> ExecuteAsync(IViewModel squad, CancellationToken cancellationToken);
}