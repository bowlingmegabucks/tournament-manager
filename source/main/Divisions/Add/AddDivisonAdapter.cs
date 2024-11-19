namespace NortheastMegabuck.Divisions.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    internal Adapter(IConfiguration config)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => new BusinessLogic(config));
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockBusinessLogic"></param>
    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => mockBusinessLogic);
    }

    public async Task<DivisionId?> ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken)
    {
        var model = new Models.Division(viewModel);

        var id = await BusinessLogic.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        Errors = BusinessLogic.Errors;

        return id;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<DivisionId?> ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken);
}