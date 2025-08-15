
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Registrations.Retrieve;
internal class DataLayer : IDataLayer
{
    private readonly IRepository _repository;

    public DataLayer(IRepository repository)
    {
        _repository = repository;
    }

    async Task<IEnumerable<Models.Registration>> IDataLayer.ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken)
        => (await _repository.Retrieve(tournamentId).ToListAsync(cancellationToken).ConfigureAwait(false)).Select(registration => new Models.Registration(registration));

    async Task<Models.Registration> IDataLayer.ExecuteAsync(RegistrationId id, CancellationToken cancellationToken)
        => new(await _repository.RetrieveAsync(id, cancellationToken).ConfigureAwait(false) ?? throw new InvalidOperationException($"Registration with ID {id} not found."));
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Registration>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);

    Task<Models.Registration> ExecuteAsync(RegistrationId id, CancellationToken cancellationToken);
}