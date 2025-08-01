using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Tournaments.Retrieve;

internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    async Task<Models.Tournament> IDataLayer.ExecuteAsync(DivisionId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));

    async Task<Models.Tournament> IDataLayer.ExecuteAsync(SquadId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));

    async Task<Models.Tournament> IDataLayer.ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false));
}

internal interface IDataLayer
{
    Task<Models.Tournament> ExecuteAsync(DivisionId id, CancellationToken cancellationToken);

    Task<Models.Tournament> ExecuteAsync(SquadId id, CancellationToken cancellationToken);

    Task<Models.Tournament> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}