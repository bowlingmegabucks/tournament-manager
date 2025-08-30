namespace BowlingMegabucks.TournamentManager.Presentation.Services;

internal abstract class TournamentManagerAdapter
{
    protected readonly ITournamentManagerApi _tournamentManagerApi;

    protected TournamentManagerAdapter(ITournamentManagerApi tournamentManagerApi)
    {
        _tournamentManagerApi = tournamentManagerApi;
    }
}
