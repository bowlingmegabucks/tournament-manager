namespace NortheastMegabuck.Registrations.Update;

internal sealed class Adapter : IAdapter
{
    private readonly IBusinessLogic _businessLogic;

    public IEnumerable<Models.ErrorDetail> Errors
        => _businessLogic.Errors;

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

    async Task IAdapter.AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken)
        => await _businessLogic.AddSuperSweeperAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IAdapter
{
    IEnumerable<Models.ErrorDetail> Errors { get; }

    Task AddSuperSweeperAsync(RegistrationId id, CancellationToken cancellationToken);
}