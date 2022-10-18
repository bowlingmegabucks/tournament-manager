
using System.ComponentModel;

namespace NortheastMegabuck.Scores;
public partial class RecapSheetForm : System.Windows.Forms.Form
{
    private readonly IDictionary<short, Controls.RecapSheetGameRowControl> _games;
    private readonly DateTime _squadDate;
    private readonly Pen _pen;

    public RecapSheetForm(DateTime squadDate)
    {
        InitializeComponent();

        _squadDate = squadDate;
        _games = new Dictionary<short, Controls.RecapSheetGameRowControl>();
        _recaps = new List<IRecapSheetViewModel>();
        _pen = new Pen(Color.Black, 2);
    }

    private IList<IRecapSheetViewModel> _recaps;

    internal void Preview(IEnumerable<IRecapSheetViewModel> recaps, short games)
    {
        dateLabel.Text = _squadDate.ToString("MM/dd/yyyy");
        timeLabel.Text = _squadDate.ToString("hh:mm tt");

        Show();

        _recaps = recaps.ToList();

        recapsTrackBar.Maximum = _recaps.Count - 1;
        recapsTrackBar.LargeChange = _recaps.Count / 10;

        gamesFlowPanelLayout.Controls.Add(new Controls.RecapSheetGameHeaderControl());

        GenerateGameRows(games);

        PopulateSheet(_recaps[0]);
    }

    private void GenerateGameRows(short games)
    {
        for (short i = 1; i <= games; i++)
        {
            _games.Add(i, new Controls.RecapSheetGameRowControl());
        }

        gamesFlowPanelLayout.Controls.AddRange(_games.Values.ToArray());
        gamesFlowPanelLayout.Controls.Add(new Controls.RecapSheetGameTotalControl());

#pragma warning disable CA1303 // Do not pass literals as localized parameters
        var officeCheck = new Label
        {
            Font = new Font("Calibri", 11F),
            Name = "labelOfficeCheck",
            Size = new System.Drawing.Size(416, 48),
            TabIndex = 18,
            Text = "Office Check: __________",
            TextAlign = ContentAlignment.BottomRight
        };
#pragma warning restore CA1303 // Do not pass literals as localized parameters

        gamesFlowPanelLayout.Controls.Add(officeCheck);

    }

    private void PopulateSheet(IRecapSheetViewModel recapSheet)
    {
        nameLabel.Text = recapSheet.BowlerName;
        divisionLabel.Text = recapSheet.DivisionName;
        bowlerSignatureLabel.Text = recapSheet.BowlerName;

        foreach (var assignment in recapSheet.Cross)
        {
            _games[assignment.Key].LaneAssignment = assignment.Value;
            _games[assignment.Key].Handicap = recapSheet.Handicap;
        }

        gamesFlowPanelLayout.Controls.OfType<Controls.RecapSheetGameTotalControl>().Single().Handicap = recapSheet.Handicap * _games.Count;
    }

    private void RecapsTrackBar_Scroll(object sender, EventArgs e)
        => PopulateSheet(_recaps[recapsTrackBar.Value]);

    private void RecapSheetForm_Paint(object sender, PaintEventArgs e)
        => new List<Label> { bowlerSignatureLabel, opposingSignatureLabel }.ForEach(label => DrawSignatureLine(label, e));

    private void DrawSignatureLine(Label label, PaintEventArgs e)
        => e.Graphics.DrawLine(_pen, label.Location.X, label.Location.Y, label.Size.Width + label.Location.X, label.Location.Y - 1);

    private void CancelButton_Click(object sender, EventArgs e)
        => Dispose();
}
