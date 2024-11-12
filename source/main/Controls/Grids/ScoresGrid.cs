
using System.Diagnostics.CodeAnalysis;
using NortheastMegabuck.Scores;

namespace NortheastMegabuck.Controls.Grids;

public partial class ScoresGrid
#if DEBUG
    : ScoresMiddleGrid
#else
    : DataGrid<IGridViewModel>
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
    public void SetSquadId(SquadId squadId)
        => _squadId = squadId;

    public void FillScores([NotNull] IEnumerable<IGridViewModel> bowlerScores)
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

#pragma warning disable S3267 // Loops should be simplified with "LINQ" expressions
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
#pragma warning restore S3267 // Loops should be simplified with "LINQ" expressions

        return scores;
    }

    internal void FillScores(IEnumerable<IViewModel> squadScores)
    {
        var scoresByBowler = squadScores.GroupBy(squadScore => squadScore.BowlerId);

        foreach (var scoreByBowler in scoresByBowler)
        {
            var dataRow = GridView.Rows.OfType<DataGridViewRow>().Single(row => row.Cells["bowlerIdColumn"].Value.ToString() == scoreByBowler.Key.ToString());

            foreach (var score in scoreByBowler)
            {
                dataRow.Cells[$"game{score.GameNumber}Column"].Value = score.Score;
            }
        }
    }
}

#if DEBUG
public class ScoresMiddleGrid : DataGrid<IGridViewModel>
{
    public ScoresMiddleGrid()
    {

    }
}
#endif
