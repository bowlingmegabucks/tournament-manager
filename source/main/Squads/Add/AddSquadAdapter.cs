namespace NortheastMegabuck.Squads.Add;
internal class Adapter : IAdapter
{
    public IEnumerable<Models.ErrorDetail> Errors { get; private set; } = [];

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IBusinessLogic businessLogic)
    {
        _businessLogic = businessLogic;
    }

    public async Task<SquadId?> ExecuteAsync(IViewModel squad, CancellationToken cancellationToken)
    {
        var model = squad.ToModel();

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