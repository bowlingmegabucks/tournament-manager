
namespace NortheastMegabuck.Bowlers.Update;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors
        => _businessLogic.Errors;

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

    async Task IAdapter.ExecuteAsync(BowlerId id, INameViewModel viewModel, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, new Models.PersonName(viewModel), cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task ExecuteAsync(BowlerId id, INameViewModel viewModel, CancellationToken cancellationToken);
}