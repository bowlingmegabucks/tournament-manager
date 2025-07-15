
namespace NortheastMegabuck.Scores.Retrieve;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.ErrorDetail;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken)
        => (await _businessLogic.ExecuteAsync([squadId], cancellationToken).ConfigureAwait(false)).Select(squadScore => new ViewModel(squadScore));
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task<IEnumerable<IViewModel>> ExecuteAsync(SquadId squadId, CancellationToken cancellationToken);
}