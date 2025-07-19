
namespace BowlingMegabucks.TournamentManager.Squads.Add;

internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _mapper;
    private readonly IRepository _repository;

    public DataLayer(IEntityMapper mapper, IRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<SquadId> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(squad);

        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
}

internal interface IDataLayer
{
    Task<SquadId> ExecuteAsync(Models.Squad squad, CancellationToken cancellationToken);
}