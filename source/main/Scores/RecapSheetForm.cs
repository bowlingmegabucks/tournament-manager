
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

    private readonly IList<Image> _recapSheets = new List<Image>();
    private int _counter;

    private void PrintButton_Click(object sender, EventArgs e)
    {
        if (recapsPrintDialog.ShowDialog() == DialogResult.Cancel)
        {
            return;
        }

        if (recapsTrackBar.Value != 0)
        {
            recapsTrackBar.Value = 0;
            recapsTrackBar.Scroll -= RecapsTrackBar_Scroll!;
            RecapsTrackBar_Scroll(sender, e);
        }

        buttonsPanel.Visible = false;
        FormBorderStyle = FormBorderStyle.None;

        scrollRecapsTimer.Enabled = true;
    }

    private void ScrollRecapsTimer_Tick(object sender, EventArgs e)
    {
        _recapSheets.Add(CaptureScreen());

        if (_counter == _recaps.Count)
        {
            scrollRecapsTimer.Enabled = false;
            _counter = 0;

            recapPrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Half Sheet", 850, 550);
            recapPrintDocument.Print();
        }
        else
        {
            PopulateSheet(_recaps[_counter++]);
        }
    }

    private Image CaptureScreen()
    {
        var myGraphics = CreateGraphics();
        var s = Size;

        var image = new Bitmap(s.Width, s.Height, myGraphics);

        var memoryGraphics = Graphics.FromImage(image);
        memoryGraphics.CopyFromScreen(Location.X, Location.Y, 0, 0, s);

        return image;
    }

    private void RecapPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
    {
        e.Graphics!.DrawImage(_recapSheets[_counter], 8, 0);
        _counter++;
        e.HasMorePages = _counter != _recapSheets.Count;
    }

    private void RecapPrintDocument_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        _recapSheets.Clear();
        Dispose();
    }
}
