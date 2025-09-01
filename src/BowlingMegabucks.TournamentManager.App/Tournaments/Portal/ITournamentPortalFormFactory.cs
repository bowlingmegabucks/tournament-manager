using BowlingMegabucks.TournamentManager.Domain.Tournaments;

namespace BowlingMegabucks.TournamentManager.App.Tournaments.Portal;

internal interface ITournamentPortalFormFactory
{
    TournamentPortalForm Create(TournamentId id);
}
