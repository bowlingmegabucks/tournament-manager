
using NortheastMegabuck.Scores;

namespace NortheastMegabuck.Controls;

public partial class ScoresGrid
#if DEBUG
    : ScoresMiddleGrid
#else
    : Controls.DataGrid<Scores.IGridViewModel>
#endif
{
    private short _games;

    public ScoresGrid()
    {
        InitializeComponent();
    }

    public void GenerateGameColumns(short games)
    {
        _games = games;

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

    private SquadId _squadId;
    public SquadId SquadId
    {
        set => _squadId = value;
    }

    public void LoadScores(IEnumerable<Scores.IGridViewModel> bowlerScores)
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

    public IEnumerable<IViewModel> GetScores()
    {
        var scores = new List<IViewModel>();

        var bowlerScores = GridView.Rows.OfType<DataGridViewRow>().Where(row => !string.IsNullOrEmpty(row.Cells["game1Column"].Value?.ToString())).ToList();

        foreach (var bowlerScore in bowlerScores)
        {
            var bowlerId = bowlerScore.Cells["bowlerIdColumn"].Value.ToString();

            for (short i = 1; i <= _games; i++)
            {
                if (int.TryParse(bowlerScore.Cells[$"game{i}Column"].Value?.ToString(), out var score))
                {
                    scores.Add(new ViewModel
                    {
                        SquadId = _squadId,
                        BowlerId = new BowlerId(new Guid(bowlerId!)),
                        GameNumber = i,
                        Score = score
                    });
                }  
            }
        }

        return scores;
    }
}

#if DEBUG
public class ScoresMiddleGrid : DataGrid<Scores.IGridViewModel>
{
    public ScoresMiddleGrid()
    {

    }
}
#endif
