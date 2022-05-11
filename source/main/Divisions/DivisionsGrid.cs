using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEnglandClassic.Divisions;
internal partial class DivisionsGrid
#if DEBUG
    : MiddleGrid
#else
    : Controls.DataGrid<IViewModel>
#endif
{
    public DivisionsGrid()
    {
        InitializeComponent();
    }

    public IViewModel? SelectedDivision
        => SelectedRow;

    private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        var division = GridView.Rows[e.RowIndex].DataBoundItem as IViewModel;
        
        switch (GridView.Columns[e.ColumnIndex].Name)
        {
            case "ColumnGender":
                if (division!.Gender != null)
                {
                    e.Value = division.Gender == NewEnglandClassic.Models.Gender.Male ? "Men" : "Women";
                    e.FormattingApplied = true;
                }
                
                break;
            case "ColumnHandicap":
                if (division!.HandicapBase != null)
                {
                    var handicap = new StringBuilder();
                    handicap.Append($"{division.HandicapPercentage!.Value:N0}% of {division.HandicapBase!.Value}");
                    
                    if (division.MaximumHandicapPerGame.HasValue)
                        handicap.Append($" ({division.MaximumHandicapPerGame.Value} pins max)");

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
internal class MiddleGrid : Controls.DataGrid<IViewModel>
{
    public MiddleGrid()
    {

    }
}
#endif
