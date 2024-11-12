using System.Diagnostics.CodeAnalysis;
using System.Text;
using NortheastMegabuck.Registrations.Retrieve;

namespace NortheastMegabuck.Controls.Grids;
public partial class TournamentRegistrationGrid
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

        _squadDates = new Dictionary<SquadId, string>();
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
            _squadDates.Add(squadDate.Key, squadDate.Value);
        }
    }

    public Registrations.Retrieve.ITournamentRegistrationViewModel SelectedRegistration
        => SelectedRow!;

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
public class TournamentRegistrationMiddleGrid : DataGrid<Registrations.Retrieve.ITournamentRegistrationViewModel>
{
    public TournamentRegistrationMiddleGrid()
    {

    }
}
#endif
