using System.Text;

namespace NortheastMegabuck.Controls;
public partial class DivisionsGrid
#if DEBUG
    : DivisionsMiddleGrid
#else
    : Controls.DataGrid<Divisions.IViewModel>
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
                    handicap.Append($"{division.HandicapPercentage!.Value:N0}% of {division.HandicapBase!.Value}");
                    
                    if (division.MaximumHandicapPerGame.HasValue)
                    {
                        handicap.Append($" ({division.MaximumHandicapPerGame.Value} pins max)");
                    }

                    e.Value = handicap.ToString();
                }
                else
                {
                    e.Value = "Scratch";
                }

                e.FormattingApplied = true;

                break;
        }
    }
}

#if DEBUG
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class DivisionsMiddleGrid : Controls.DataGrid<Divisions.IViewModel>
{
    public DivisionsMiddleGrid()
    {

    }
}
#endif
