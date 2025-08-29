using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Application.Tournaments;
using BowlingMegabucks.TournamentManager.Application.Tournaments.GetAllTournaments;
using BowlingMegabucks.TournamentManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BowlingMegabucks.TournamentManager.Infrastructure.Queries;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class TournamentQueries
    : ITournamentQueries
{
    private readonly ApplicationDbContext _applicationDbContext;

    public TournamentQueries(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IReadOnlyCollection<TournamentSummaryDto>> GetAllTournamentsAsync(CancellationToken cancellationToken)
        => await _applicationDbContext.Tournaments
            .AsNoTracking()
            .Select(tournament => new TournamentSummaryDto
            {
                Id = tournament.Id,
                Name = tournament.Name,
                StartDate = tournament.TournamentDates.StartDate,
                EndDate = tournament.TournamentDates.EndDate,
                BowlingCenter = tournament.BowlingCenter,
                EntryFee = tournament.EntryFee,
                Completed = tournament.Completed,
            }).ToListAsync(cancellationToken);
}
