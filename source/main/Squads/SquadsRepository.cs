using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Squads;

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

    public async Task<SquadId> AddAsync(Database.Entities.TournamentSquad squad, CancellationToken cancellationToken)
    {
        await _dataContext.Squads.AddAsync(squad, cancellationToken).ConfigureAwait(false);
        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return squad.Id;
    }

    public IQueryable<Database.Entities.TournamentSquad> Retrieve(TournamentId tournamentId)
        => _dataContext.Squads.AsNoTracking().Where(squad => squad.TournamentId == tournamentId);

    public async Task<Database.Entities.TournamentSquad> RetrieveAsync(SquadId id, CancellationToken cancellationToken)
        => await _dataContext.Squads.AsNoTracking().FirstAsync(squad => squad.Id == id, cancellationToken).ConfigureAwait(false);

    public async Task CompleteAsync(SquadId id, CancellationToken cancellationToken)
    {
        var squad = await _dataContext.Squads.FirstAsync(sweeper => sweeper.Id == id, cancellationToken).ConfigureAwait(false);
        squad.Complete = true;

        var squadScores = await _dataContext.SquadScores.AsNoTracking().Where(score => score.SquadId == id).ToListAsync(cancellationToken).ConfigureAwait(false);
        var noBowl = await _dataContext.Registrations.AsNoTrackingWithIdentityResolution().Include(registration => registration.Squads)
                                .Where(registration => registration.Squads.Select(squad => squad.SquadId).Contains(id))
                                .Select(registration => registration.BowlerId)
                                .Where(bowlerId => !squadScores.Select(score => score.BowlerId).Contains(bowlerId)).ToListAsync(cancellationToken).ConfigureAwait(false);

        if (noBowl.Count > 0)
        {
            var tournament = await _dataContext.Tournaments.AsNoTrackingWithIdentityResolution().Include(tournament => tournament.Squads).FirstAsync(tournament => tournament.Squads.Select(squad => squad.Id).Contains(id), cancellationToken).ConfigureAwait(false);
            var games = tournament.Games;

            foreach (var noShow in noBowl)
            {
                for (short game = 1; game <= games; game++)
                {
                    await _dataContext.SquadScores.AddAsync(new Database.Entities.SquadScore { BowlerId = noShow, SquadId = id, Score = 0, Game = game }, cancellationToken).ConfigureAwait(false);
                }
            }
        }

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}

internal interface IRepository
{
    Task<SquadId> AddAsync(Database.Entities.TournamentSquad squad, CancellationToken cancellationToken);

    IQueryable<Database.Entities.TournamentSquad> Retrieve(TournamentId tournamentId);

    Task<Database.Entities.TournamentSquad> RetrieveAsync(SquadId id, CancellationToken cancellationToken);

    Task CompleteAsync(SquadId id, CancellationToken cancellationToken);
}
