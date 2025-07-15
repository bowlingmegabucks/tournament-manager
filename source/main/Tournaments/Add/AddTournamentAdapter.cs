namespace NortheastMegabuck.Tournaments.Add;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IBusinessLogic _businessLogic;
    internal Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task<TournamentId?> ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken)
    {
        var model = viewModel.ToModel();

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