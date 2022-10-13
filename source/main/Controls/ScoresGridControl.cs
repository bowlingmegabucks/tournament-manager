
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

    public void LoadScores(IEnumerable<Scores.IViewModel> bowlerScores)
    {
        foreach (var bowlerScore in bowlerScores)
        {
            var dataRow = GridView.Rows.OfType<DataGridViewRow>().Single(row => row.Cells["bowlerIdColumn"].Value.ToString() == bowlerScore.BowlerId.Value.ToString());

            foreach (var score in bowlerScore.Scores)
            {
                dataRow.Cells[$"game{score.Key}Column"].Value = score.Value;
            }
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
