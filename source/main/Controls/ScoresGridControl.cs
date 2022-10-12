
namespace NortheastMegabuck.Controls;

public partial class ScoresGrid
#if DEBUG
    : ScoresMiddleGrid
#else
    : Controls.DataGrid<Scores.IViewModel>
#endif
{
    public ScoresGrid()
    {
        InitializeComponent();
    }

    public void GenerateGameColumns(short games)
    {
        for (var i = 1; i <= games; i++)
        {
            var column = new DataGridViewColumn()
            {
                Name = $"game{i}Column",
                ReadOnly = false,
                Visible = true,
                Width = 55,
                HeaderText = $"Game {i}",
                CellTemplate = new DataGridViewTextBoxCell()
            };

            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            GridView.Columns.Add(column);
        }
    }
}

#if DEBUG
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public class ScoresMiddleGrid : DataGrid<Scores.IViewModel>
{
    public ScoresMiddleGrid()
    {

    }
}
#endif
