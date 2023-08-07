using Microsoft.EntityFrameworkCore;

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

    Database.Entities.Division IRepository.Retrieve(DivisionId id)
        => _dataContext.Divisions.Single(division => division.Id == id);
}

internal interface IRepository
{
    Task<DivisionId> AddAsync(Database.Entities.Division division, CancellationToken cancellationToken);

    IQueryable<Database.Entities.Division> Retrieve(TournamentId tournamentId);

    Database.Entities.Division Retrieve(DivisionId id);
}