﻿using System.Diagnostics.CodeAnalysis;
using System.Text;
using BowlingMegabucks.TournamentManager.Registrations.Retrieve;

namespace BowlingMegabucks.TournamentManager.Controls.Grids;
internal sealed partial class TournamentRegistrationGrid
#if DEBUG
    : TournamentRegistrationMiddleGrid
#else
    : DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
#endif
{
    private readonly Dictionary<SquadId, string> _squadDates;

    public TournamentRegistrationGrid()
    {
        InitializeComponent();

        _squadDates = [];
    }

    public void Remove(RegistrationId id)
    {
        var registration = Models.Single(model => model.Id == id);

        Remove(registration);
    }

    public void AddSquadDates([NotNull] IDictionary<SquadId, string> squadDates)
    {
        foreach (var squadDate in squadDates)
        {
            _squadDates.TryAdd(squadDate.Key, squadDate.Value);
        }
    }

    public ITournamentRegistrationViewModel SelectedRegistration
        => SelectedRow!;

    public void Filter(string nameFilter)
    {
        if (string.IsNullOrWhiteSpace(nameFilter))
        {
            ClearFilter();

            return;
        }

        var filterText = nameFilter.Trim();

        var nameParts = filterText.Split(' ');
        var firstNamePart = nameParts.Length > 0 ? nameParts[0] : string.Empty;
        var lastNamePart = nameParts.Length > 1 ? nameParts[1] : string.Empty;

        var filtered = Models.Where(model =>
            nameParts.Length == 1
                ? model.FirstName.Contains(firstNamePart, StringComparison.OrdinalIgnoreCase) ||
                       model.LastName.Contains(firstNamePart, StringComparison.OrdinalIgnoreCase)
                : model.FirstName.Contains(firstNamePart, StringComparison.OrdinalIgnoreCase) &&
                       model.LastName.Contains(lastNamePart, StringComparison.OrdinalIgnoreCase));

        Filter(filtered);
    }

    private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var row = GridView.Rows[e.RowIndex];
        var column = row.Cells[e.ColumnIndex];

        var registration = row.DataBoundItem as ITournamentRegistrationViewModel;

        if (row.Cells[nameof(squadsEnteredColumn)].ColumnIndex == column.ColumnIndex)
        {
            var squads = new StringBuilder();

            foreach (var squad in registration!.SquadsEntered)
            {
                squads.AppendLine(_squadDates[squad]);
            }

            column.ToolTipText = squads.ToString();
        }
        else if (row.Cells[nameof(sweepersEnteredColumn)].ColumnIndex == column.ColumnIndex)
        {
            var sweepers = new StringBuilder();

            foreach (var sweeper in registration!.SweepersEntered)
            {
                sweepers.AppendLine(_squadDates[sweeper]);
            }

            column.ToolTipText = sweepers.ToString();
        }
        else
        {
            column.ToolTipText = string.Empty;
        }
    }
}

#if DEBUG
internal class TournamentRegistrationMiddleGrid : DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
{
    public TournamentRegistrationMiddleGrid()
    {

    }
}
#endif
