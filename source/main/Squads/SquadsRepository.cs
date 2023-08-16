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

    public SquadId Add(Database.Entities.TournamentSquad squad)
    {
        _dataContext.Squads.Add(squad);
        _dataContext.SaveChanges();

        return squad.Id;
    }

    public IQueryable<Database.Entities.TournamentSquad> Retrieve(TournamentId tournamentId)
        => _dataContext.Squads.AsNoTracking().Where(squad => squad.TournamentId == tournamentId);

    public async Task<Database.Entities.TournamentSquad> RetrieveAsync(SquadId id, CancellationToken cancellationToken)
        => await _dataContext.Squads.AsNoTracking().FirstAsync(squad => squad.Id == id, cancellationToken).ConfigureAwait(false);

    public void Complete(SquadId id)
    {
        var squad = _dataContext.Squads.Single(sweeper => sweeper.Id == id);
        squad.Complete = true;

        var squadScores = _dataContext.SquadScores.AsNoTracking().Where(score => score.SquadId == id).ToList();
        var registrations = _dataContext.Registrations.AsNoTrackingWithIdentityResolution().Include(registration => registration.Squads).Where(registration => registration.Squads.Select(squad => squad.SquadId).Contains(id)).ToList();
        var squadBowlerIds = registrations.Select(registration => registration.BowlerId).ToList();

        var noBowl = squadBowlerIds.Where(bowlerId => !squadScores.Select(score => score.BowlerId).Contains(bowlerId)).ToList();

        if (noBowl.Count > 0)
        {
            var tournament = _dataContext.Tournaments.AsNoTrackingWithIdentityResolution().Include(tournament => tournament.Squads).Single(tournament => tournament.Squads.Select(squad => squad.Id).Contains(id));
            var games = tournament.Games;

            foreach (var noShow in noBowl)
            {
                for (short game = 1; game <= games; game++)
                {
                    _dataContext.SquadScores.Add(new Database.Entities.SquadScore { BowlerId = noShow, SquadId = id, Score = -1, Game = game });
                }
            }
        }

        _dataContext.SaveChanges();
    }
}

internal interface IRepository
{
    SquadId Add(Database.Entities.TournamentSquad squad);

    IQueryable<Database.Entities.TournamentSquad> Retrieve(TournamentId tournamentId);

    Task<Database.Entities.TournamentSquad> RetrieveAsync(SquadId id, CancellationToken cancellationToken);

    void Complete(SquadId id);
}
