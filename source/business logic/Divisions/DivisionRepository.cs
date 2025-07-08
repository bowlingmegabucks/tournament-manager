using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NortheastMegabuck.Divisions;

internal class Repository : IRepository
{
    private readonly Database.IDataContext _dataContext;
    internal Repository(IConfiguration config)
    {
        _dataContext = new Database.DataContext(config);
    }

    /// <summary>
    /// Unit Test Constructor
    /// </summary>
    /// <param name="mockDataContext"></param>
    internal Repository(Database.IDataContext mockDataContext)
    {
        _dataContext = mockDataContext;
    }

    async Task<DivisionId> IRepository.AddAsync(Database.Entities.Division division, CancellationToken cancellationToken)
    {
        await _dataContext.Divisions.AddAsync(division, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return division.Id;
    }

    IQueryable<Database.Entities.Division> IRepository.Retrieve(TournamentId tournamentId)
        => _dataContext.Divisions.AsNoTracking().Where(division => division.TournamentId == tournamentId);

    async Task<Database.Entities.Division> IRepository.RetrieveAsync(DivisionId id, CancellationToken cancellationToken)
        => await _dataContext.Divisions.FirstAsync(division => division.Id == id, cancellationToken).ConfigureAwait(false);
}

internal interface IRepository
{
    Task<DivisionId> AddAsync(Database.Entities.Division division, CancellationToken cancellationToken);

    IQueryable<Database.Entities.Division> Retrieve(TournamentId tournamentId);

    Task<Database.Entities.Division> RetrieveAsync(DivisionId id, CancellationToken cancellationToken);
}