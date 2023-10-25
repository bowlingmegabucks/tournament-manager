namespace NortheastMegabuck.Tournaments.Add;
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

    public async Task<TournamentId?> ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken)
    {
        var model = new Models.Tournament(viewModel);

        var id = await _businessLogic.ExecuteAsync(model, cancellationToken).ConfigureAwait(false);

        Errors = _businessLogic.Errors;

        return id;
    }
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task<TournamentId?> ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken);
}