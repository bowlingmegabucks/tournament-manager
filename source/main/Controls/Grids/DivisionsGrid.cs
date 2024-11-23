using System.Globalization;
using System.Text;

namespace NortheastMegabuck.Controls.Grids;
internal partial class DivisionsGrid
#if DEBUG
    : DivisionsMiddleGrid
#else
    : DataGrid<Divisions.IViewModel>
#endif
{
    public DivisionsGrid()
    {
        InitializeComponent();
    }

    public Divisions.IViewModel? SelectedDivision
        => SelectedRow;

    private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var division = GridView.Rows[e.RowIndex].DataBoundItem as Divisions.IViewModel;

        switch (GridView.Columns[e.ColumnIndex].Name)
        {
            case nameof(genderColumn):
                if (division!.Gender != null)
                {
                    e.Value = division.Gender == NortheastMegabuck.Models.Gender.Male ? "Men" : "Women";
                    e.FormattingApplied = true;
                }

                break;
            case nameof(handicapColumn):
                if (division!.HandicapBase != null)
                {
                    var handicap = new StringBuilder();
                    handicap.AppendFormat(CultureInfo.CurrentCulture, "{0:N0}% of {1}", division.HandicapPercentage!.Value, division.HandicapBase!.Value);

                    if (division.MaximumHandicapPerGame.HasValue)
                    {
                        handicap.AppendFormat(CultureInfo.CurrentCulture, " ({0} pins max)", division.MaximumHandicapPerGame.Value);
                    }

                    e.Value = handicap.ToString();
                }
                else
                {
                    e.Value = "Scratch";
                }

                e.FormattingApplied = true;

                break;
            default:
                break;
        }
    }
}

#if DEBUG
internal class DivisionsMiddleGrid : DataGrid<Divisions.IViewModel>
{
    public DivisionsMiddleGrid()
    {

    }
}
#endif
