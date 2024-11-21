using Microsoft.EntityFrameworkCore;

namespace NortheastMegabuck.Scores;

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

    async Task IRepository.UpdateAsync(ICollection<Database.Entities.SquadScore> scores, CancellationToken cancellationToken)
    {
        var squadId = scores.First().SquadId;
        var existingSquadScores = await _dataContext.SquadScores.Where(score => score.SquadId == squadId).ToListAsync(cancellationToken).ConfigureAwait(false);

        foreach (var newSquadScore in scores)
        {
            var existingSquadScore = existingSquadScores.SingleOrDefault(score => score.BowlerId == newSquadScore.BowlerId && score.Game == newSquadScore.Game);

            if (existingSquadScore == null) //this is a new score
            {
                await _dataContext.SquadScores.AddAsync(newSquadScore, cancellationToken).ConfigureAwait(false);
            }
            else if (newSquadScore.Score != existingSquadScore.Score) //updated score for bowler in game
            {
                _dataContext.SquadScores.Attach(existingSquadScore);
                _dataContext.Entry(existingSquadScore).State = EntityState.Modified;

                existingSquadScore.Score = newSquadScore.Score;

                existingSquadScores.Remove(existingSquadScore);
            }
            else // no change
            {
                existingSquadScores.Remove(existingSquadScore);
            }
        }

        _dataContext.SquadScores.RemoveRange(existingSquadScores); //this removes scores that were cleared out in UI

        await _dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public IQueryable<Database.Entities.SquadScore> Retrieve(params SquadId[] squadIds)
    => _dataContext.SquadScores.AsNoTrackingWithIdentityResolution()
            .Include(squadScore => squadScore.Bowler)
                .ThenInclude(bowler => bowler.Registrations.Where(registration => registration.Squads.Any(squad => squadIds.Contains(squad.SquadId))))
                .ThenInclude(registration => registration.Division).ThenInclude(division => division.Sweepers)
            .Include(squadScore => squadScore.Squad)
        .Where(squadScore => squadIds.Contains(squadScore.SquadId));

    public async Task<bool> DoesBowlerHaveAnyScoresForTournamentAsync(RegistrationId registrationId, TournamentId tournamentId, CancellationToken cancellationToken)
    {
        var tournamentSquadIds = _dataContext.Squads.AsNoTracking()
            .Where(squad => squad.TournamentId == tournamentId)
            .Select(squad => squad.Id);

        var tournamentSweeperIds = _dataContext.Sweepers.AsNoTracking()
            .Where(sweeper => sweeper.TournamentId == tournamentId)
            .Select(sweeper => sweeper.Id);

        var squadIds = tournamentSquadIds.Union(tournamentSweeperIds);

        var bowlerId = await _dataContext.Registrations.AsNoTracking()
            .Where(registration => registration.Id == registrationId)
            .Select(registration => registration.BowlerId)
            .SingleAsync(cancellationToken).ConfigureAwait(false);

        var scores = _dataContext.SquadScores.AsNoTracking()
            .Where(score => score.BowlerId == bowlerId && squadIds.Contains(score.SquadId));

        return await scores.AnyAsync(cancellationToken).ConfigureAwait(false);
    }
}

internal interface IRepository
{
    Task UpdateAsync(ICollection<Database.Entities.SquadScore> scores, CancellationToken cancellationToken);

    IQueryable<Database.Entities.SquadScore> Retrieve(params SquadId[] squadIds);

    Task<bool> DoesBowlerHaveAnyScoresForTournamentAsync(RegistrationId registrationId, TournamentId tournamentId, CancellationToken cancellationToken);
}