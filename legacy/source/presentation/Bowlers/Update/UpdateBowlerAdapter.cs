
namespace BowlingMegabucks.TournamentManager.Bowlers.Update;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors
        => _businessLogic.Errors;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    async Task IAdapter.ExecuteAsync(BowlerId id, INameViewModel viewModel, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, viewModel.ToPersonName(), cancellationToken).ConfigureAwait(false);

    async Task IAdapter.ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(viewModel.ToModel(), cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task ExecuteAsync(BowlerId id, INameViewModel viewModel, CancellationToken cancellationToken);

    Task ExecuteAsync(IViewModel viewModel, CancellationToken cancellationToken);
}