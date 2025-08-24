
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
}

internal interface IDataLayer
{
    Task<IEnumerable<Models.Registration>> ExecuteAsync(TournamentId tournamentId, CancellationToken cancellationToken);
}