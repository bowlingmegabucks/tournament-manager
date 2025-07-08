
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Registrations.Add;
internal class DataLayer : IDataLayer
{
    private readonly IEntityMapper _mapper;
    private readonly IRepository _repository;

    internal DataLayer(IConfiguration config)
    {
        _mapper = new EntityMapper();
        _repository = new Repository(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockMapper"></param>
    /// <param name="mockRepository"></param>
    internal DataLayer(IEntityMapper mockMapper, IRepository mockRepository)
    {
        _mapper = mockMapper;
        _repository = mockRepository;
    }

    public async Task<RegistrationId> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken)
    {
        var entity = _mapper.Execute(registration);

        if (entity.BowlerId != BowlerId.Empty)
        {
            entity.Bowler = null!;
        }

        return await _repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }
    public async Task<Models.Registration> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken)
        => new(await _repository.AddSquadAsync(bowlerId, squadId, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<RegistrationId> ExecuteAsync(Models.Registration registration, CancellationToken cancellationToken);

    Task<Models.Registration> ExecuteAsync(BowlerId bowlerId, SquadId squadId, CancellationToken cancellationToken);
}