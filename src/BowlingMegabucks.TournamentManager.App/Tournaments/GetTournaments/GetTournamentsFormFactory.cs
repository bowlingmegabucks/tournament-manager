using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;
using Microsoft.Extensions.DependencyInjection;

namespace BowlingMegabucks.TournamentManager.App.Tournaments.GetTournaments;

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
