
namespace NortheastMegabuck.Sweepers.Complete;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

    private readonly IBusinessLogic _businessLogic;

    public Adapter(IConfiguration config)
    {
        _businessLogic = new BusinessLogic(config);
    }

    internal Adapter(IBusinessLogic mockBusinessLogic)
    {
        _businessLogic = mockBusinessLogic;
    }

    public async Task ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}