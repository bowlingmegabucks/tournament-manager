﻿namespace BowlingMegabucks.TournamentManager.Controls.Grids;

internal sealed partial class TournamentsGrid
#if DEBUG   
    : TournamentMiddleGrid
#else
    : DataGrid<Tournaments.IViewModel>
#endif
{
    public TournamentsGrid()
    {
        InitializeComponent();
    }

    public Tournaments.IViewModel? SelectedTournament
        => SelectedRow;
}

#if DEBUG
internal class TournamentMiddleGrid : DataGrid<Tournaments.IViewModel>
{
    public TournamentMiddleGrid()
    {

    }
}

#endif
