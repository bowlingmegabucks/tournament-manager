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
}

internal interface IRepository
{
    Task UpdateAsync(ICollection<Database.Entities.SquadScore> scores, CancellationToken cancellationToken);

    IQueryable<Database.Entities.SquadScore> Retrieve(params SquadId[] squadIds);
}