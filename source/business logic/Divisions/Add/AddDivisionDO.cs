
namespace NortheastMegabuck.Divisions.Add;

internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _mapper;
    private readonly IRepository _repository;

    public DataLayer(IEntityMapper mapper, IRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    async Task<DivisionId> IDataLayer.ExecuteAsync(Models.Division division, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(division);

        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IDataLayer
{
    Task<DivisionId> ExecuteAsync(Models.Division division, CancellationToken cancellationToken);
}
