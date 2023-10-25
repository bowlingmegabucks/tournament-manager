
namespace NortheastMegabuck.Bowlers.Update;

internal sealed class DataLayer : IDataLayer
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

    async Task IDataLayer.ExecuteAsync(BowlerId id, Models.PersonName name, CancellationToken cancellationToken)
        => await _repository.UpdateAsync(id, name.First, name.MiddleInitial, name.Last, name.Suffix, cancellationToken).ConfigureAwait(false);
}

internal interface IDataLayer
{
    Task ExecuteAsync(BowlerId id, Models.PersonName name, CancellationToken cancellationToken);
}