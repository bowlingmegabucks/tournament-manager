using System.Security.Cryptography.X509Certificates;
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

    void IRepository.Update(IEnumerable<Database.Entities.SquadScore> newSquadScores)
    {
        var squadId = newSquadScores.First().SquadId;
        var existingSquadScores = _dataContext.SquadScores.Where(score => score.SquadId == squadId).ToList();

        foreach (var newSquadScore in newSquadScores)
        {
            var existingSquadScore = existingSquadScores.SingleOrDefault(score => score.BowlerId == newSquadScore.BowlerId && score.Game == newSquadScore.Game);

            if (existingSquadScore == null) //this is a new score
            {
                _dataContext.SquadScores.Add(newSquadScore);
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

        _dataContext.SaveChanges();
    }

    IEnumerable<Database.Entities.SquadScore> IRepository.Retrieve(SquadId squadId)
        => Retrieve(new[] { squadId });

    public IEnumerable<Database.Entities.SquadScore> Retrieve(IEnumerable<SquadId> squadIds)
    => _dataContext.SquadScores.AsNoTrackingWithIdentityResolution()
            .Include(squadScore => squadScore.Bowler)
                .ThenInclude(bowler => bowler.Registrations.Where(registration => registration.Squads.Any(squad => squadIds.Contains(squad.SquadId))))
                .ThenInclude(registration => registration.Division).ThenInclude(division => division.Sweepers)
            .Include(squadScore => squadScore.Squad)
        .Where(squadScore => squadIds.Contains(squadScore.SquadId));

    IEnumerable<Database.Entities.SquadScore> IRepository.SuperSweeper(TournamentId tournamentId)
    {
        var tournament = _dataContext.Tournaments.AsNoTrackingWithIdentityResolution()
                            .Include(tournament=> tournament.Sweepers)
                        .Single(tournament => tournament.Id == tournamentId);

        return Retrieve(tournament.Sweepers.Select(sweeper => sweeper.Id));
    }
}

internal interface IRepository
{
    void Update(IEnumerable<Database.Entities.SquadScore> scores);

    IEnumerable<Database.Entities.SquadScore> Retrieve(SquadId sqauadId);

    IEnumerable<Database.Entities.SquadScore> Retrieve(IEnumerable<SquadId> squadIds);

    IEnumerable<Database.Entities.SquadScore> SuperSweeper(TournamentId tournamnetId);
}