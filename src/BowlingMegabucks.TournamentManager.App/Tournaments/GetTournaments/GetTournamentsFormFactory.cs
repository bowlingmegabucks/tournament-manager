using System.Diagnostics.CodeAnalysis;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.App.Tournaments.GetTournaments;

[SuppressMessage(
    "Performance",
    "CA1812:Avoid uninstantiated internal classes",
    Justification = "Instantiated by dependency injection container.")]
internal sealed class GetTournamentsFormFactory
    : IGetTournamentsFormFactory
{
    private readonly IServiceProvider _serviceProvider;

    public GetTournamentsFormFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public GetTournamentsForm Create()
    {
        GetTournamentsPresenter presenter = _serviceProvider.GetRequiredService<GetTournamentsPresenter>();

        return new GetTournamentsForm(presenter);
    }
}
