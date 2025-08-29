using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Application.Abstractions.Messaging;
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

    // in the query handler, we can have properties that we want to sort on.
    // Sorting: https://github.com/kippermand/training/blob/1bdfc98631e2fa555b30306146f0fc52ded29dc8/restApi/pragmatic/auth0/DevHabit/DevHabit.Api/Services/Sorting/QueryableExtensions.cs#L7
    // SortMapping: https://github.com/kippermand/training/blob/main/restApi/pragmatic/auth0/DevHabit/DevHabit.Api/Services/Sorting/SortMapping.cs#L3
    public async Task<IReadOnlyCollection<TournamentSummaryDto>> GetAllTournamentsAsync(IOffsetPaginationQuery pagination, CancellationToken cancellationToken)
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
            })
            .ApplyPagination(pagination)
            .ToListAsync(cancellationToken);
}
