
namespace NortheastMegabuck.Sweepers.Complete;
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

    public async Task ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => await _repository.CompleteAsync(id, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(SquadId id, CancellationToken cancellationToken);
}