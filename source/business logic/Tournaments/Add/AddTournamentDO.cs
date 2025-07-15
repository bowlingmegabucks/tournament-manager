
namespace NortheastMegabuck.Tournaments.Add;

internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _mapper;
    private readonly IRepository _repository;

    public DataLayer(IEntityMapper mapper, IRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    async Task<TournamentId> IDataLayer.ExecuteAsync(Models.Tournament tournament, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(tournament);

        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IDataLayer
{
    Task<TournamentId> ExecuteAsync(Models.Tournament tournament, CancellationToken cancellationToken);
}
