using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Presentation.Services;

namespace BowlingMegabucks.TournamentManager.Presentation.Tournaments;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class TournamentsAdapter
    : TournamentManagerAdapter, ITournamentsAdapter
{
    public TournamentsAdapter(ITournamentManagerApi tournamentManagerApi)
        : base(tournamentManagerApi)
    { }
}
