
namespace NortheastMegabuck.Controls;
public partial class AtLargeResultsControl : UserControl
{
    internal AtLargeResultsControl(Tournaments.Results.IAtLargeViewModel model)
    {
        InitializeComponent();

        ToSpreadsheetRow = $"{model.Place}\t{model.BowlerName}\t{model.Score}\t{model.HighGame}\t{model.SquadDate:MM-dd hh:mm tt}";

        placeLabel.Text = model.Place.ToString();
        bowlerNameLabel.Text = model.BowlerName;
        scoreLabel.Text = model.Score.ToString();
        highGameLabel.Text = model.HighGame.ToString();
        squadDateLabel.Text = model.SquadDate;
        cashingPictureBox.Visible = model.PreviousCasher;

        if (model.ScratchScore != model.Score)
        {
            scratchToolTip.SetToolTip(scoreLabel, $"{model.ScratchScore} Scratch");
            scratchToolTip.SetToolTip(highGameLabel, $"{model.HighGameScratch} Scratch");
        }
    }

    internal AtLargeResultsControl()
    {
        InitializeComponent();

        ToSpreadsheetRow = string.Empty;

        placeLabel.Text = string.Empty;
        bowlerNameLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        highGameLabel.Text = string.Empty;
        squadDateLabel.Text = string.Empty;
        cashingPictureBox.Visible = false;

        Height = Convert.ToInt32(Math.Ceiling(Size.Height * 1.5));
    }

    internal string ToSpreadsheetRow { get; }
}
