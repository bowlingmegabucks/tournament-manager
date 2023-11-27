
namespace NortheastMegabuck.Registrations.Update;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IConfiguration config)
    {
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockRepository"></param>
    internal DataLayer(IRepository mockRepository)
    {
        _repository = mockRepository;
    }

    public async Task ExecuteAsync(RegistrationId id, bool superSweeper, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, superSweeper, cancellationToken).ConfigureAwait(false);

    public async Task ExecuteAsync(RegistrationId id, DivisionId divisionId, int? average, DateOnly? dateOfBirth, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, divisionId, average, dateOfBirth, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(RegistrationId id, bool superSweeper, CancellationToken cancellationToken);

    Task ExecuteAsync(RegistrationId id, DivisionId divisionId, int? average, DateOnly? dateOfBirth, CancellationToken cancellationToken);
}