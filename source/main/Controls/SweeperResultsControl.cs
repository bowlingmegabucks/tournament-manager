
using System.Runtime.Versioning;

namespace NortheastMegabuck.Controls;

[SupportedOSPlatform("windows")]
internal partial class SweeperResultsControl : UserControl
{
    internal SweeperResultsControl(Sweepers.Results.IViewModel model)
    {
        InitializeComponent();

        ToSpreadsheetRow = $"{model.Place}\t{model.BowlerName}\t{model.Score}\t{model.HighGame}\t{model.Casher}";

        placeLabel.Text = model.Place.ToString();
        bowlerNameLabel.Text = model.BowlerName;
        scoreLabel.Text = model.Score.ToString();
        highGameLabel.Text = model.HighGame.ToString();
        cashingPictureBox.Visible = model.Casher;

        if (model.ScratchScore != model.Score)
        {
            scratchToolTip.SetToolTip(scoreLabel, $"{model.ScratchScore} Scratch");
            scratchToolTip.SetToolTip(highGameLabel, $"{model.HighGameScratch} Scratch");
        }
    }

    internal SweeperResultsControl()
    {
        InitializeComponent();

        ToSpreadsheetRow = string.Empty;

        placeLabel.Text = string.Empty;
        bowlerNameLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        highGameLabel.Text = string.Empty;
        cashingPictureBox.Visible = false;

        Height = Convert.ToInt32(Math.Ceiling(Size.Height * 1.5));
    }

    internal string ToSpreadsheetRow { get; }
}
