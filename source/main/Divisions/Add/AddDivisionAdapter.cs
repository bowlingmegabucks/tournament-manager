namespace BowlingMegabucks.TournamentManager.Divisions.Add;

internal class Adapter : IAdapter
{
    private readonly Lazy<IBusinessLogic> _businessLogic;
    private IBusinessLogic BusinessLogic => _businessLogic.Value;

    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = new Lazy<IBusinessLogic>(() => businessLogic);
    }

    public async Task<DivisionId?> ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken)
    {
        var model = viewModel.ToModel();

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