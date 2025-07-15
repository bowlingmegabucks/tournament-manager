
namespace NortheastMegabuck.Sweepers.Add;

internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _mapper;
    private readonly IRepository _repository;

    internal DataLayer(IEntityMapper mapper, IRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<SquadId> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(sweeper);

        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IDataLayer
{
    Task<SquadId> ExecuteAsync(Models.Sweeper sweeper, CancellationToken cancellationToken);
}