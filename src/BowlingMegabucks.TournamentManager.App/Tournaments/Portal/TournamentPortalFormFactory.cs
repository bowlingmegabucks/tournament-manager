using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Domain.Tournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournamentById;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.App.Tournaments.Portal;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class TournamentPortalFormFactory
    : ITournamentPortalFormFactory
{
    private readonly IServiceProvider _serviceProvider;

    public TournamentPortalFormFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TournamentPortalForm Create(TournamentId id)
    {
        GetTournamentByIdPresenter presenter = _serviceProvider.GetRequiredService<GetTournamentByIdPresenter>();

        return new TournamentPortalForm(presenter, id);
    }
}
