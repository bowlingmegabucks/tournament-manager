using BowlingMegabucks.TournamentManager.App.Tournaments.GetTournaments;
using BowlingMegabucks.TournamentManager.Presentation.Tournaments.GetTournaments;

namespace BowlingMegabucks.TournamentManager.App.Tournaments;

internal sealed partial class TournamentsGrid
#if DEBUG   
    : TournamentMiddleGrid
#else
    : DataGridControl<TournamentSummaryViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();

        PageSizeOptions = [2, 10, 25, 50, 100];
    }

    public TournamentSummaryViewModel? SelectedTournament
        => SelectedRow;
}
