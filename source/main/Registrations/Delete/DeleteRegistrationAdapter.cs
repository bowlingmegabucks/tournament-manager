
namespace NortheastMegabuck.Registrations.Delete;
internal class Adapter : IAdapter
{
    public Models.ErrorDetail? Error
        => _businessLogic.Error;

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

    public async Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => await _businessLogic.ExecuteAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false);

    public void Execute(RegistrationId id)
        => _businessLogic.Execute(id);
}

internal interface IAdapter
{
    Models.ErrorDetail? Error { get; }

    Task ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);

    void Execute(RegistrationId id);
}