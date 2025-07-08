namespace NortheastMegabuck.Sweepers.Add;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

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

    public async Task<SquadId?> ExecuteAsync(IViewModel sweeper, CancellationToken cancellationToken)
    {
        var model = sweeper.ToModel();

        var guid = await _businessLogic.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        Errors = _businessLogic.Errors;

        return guid;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<SquadId?> ExecuteAsync(IViewModel sweeper, CancellationToken cancellationToken);
}