using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;

internal sealed partial class TournamentsGrid
#if DEBUG   
    : Tournaments.GetTournaments.TournamentMiddleGrid
#else
    : Controls.Grids.DataGridControl<TournamentSummaryViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();

        PageSizeOptions = PageSizeOptions.Count == 0
            ? [10, 25]
            : PageSizeOptions;
    }

    public TournamentSummaryViewModel? SelectedTournament
        => SelectedRow;
}
