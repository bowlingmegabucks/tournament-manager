using BowlingMegabucks.TournamentManager.App.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;

internal sealed partial class TournamentsGrid
#if DEBUG   
    : TournamentMiddleGrid
#else
    : DataGridControl<Presentation.Tournaments.GetTournaments.TournamentSummaryViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();
    }

    public Presentation.Tournaments.GetTournaments.TournamentSummaryViewModel? SelectedTournament
        => SelectedRow;
}
