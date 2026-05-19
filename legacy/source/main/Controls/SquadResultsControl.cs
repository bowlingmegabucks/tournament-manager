
using System.Globalization;
using System.Runtime.Versioning;

namespace BowlingMegabucks.TournamentManager.Controls;

[SupportedOSPlatform("windows")]
internal sealed partial class SquadResultsControl 
    : UserControl
{
    internal SquadResultsControl(Squads.Results.IViewModel model)
    {
        InitializeComponent();

        ToSpreadsheetRow = $"{model.Place}\t{model.BowlerName}\t{model.Score}\t{model.HighGame}\t{model.Advancer}\t{model.Casher}";

        placeLabel.Text = model.Place.ToString(CultureInfo.InvariantCulture);
        bowlerNameLabel.Text = model.BowlerName;
        scoreLabel.Text = model.Score.ToString(CultureInfo.InvariantCulture);
        highGameLabel.Text = model.HighGame.ToString(CultureInfo.InvariantCulture);
        advancerPictureBox.Visible = model.Advancer;
        cashingPictureBox.Visible = model.Casher;

        if (model.ScratchScore != model.Score)
        {
            scratchToolTip.SetToolTip(scoreLabel, $"{model.ScratchScore} Scratch");
            scratchToolTip.SetToolTip(highGameLabel, $"{model.HighGameScratch} Scratch");
        }
    }

    internal SquadResultsControl()
    {
        InitializeComponent();

        ToSpreadsheetRow = string.Empty;

        placeLabel.Text = string.Empty;
        bowlerNameLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        highGameLabel.Text = string.Empty;
        advancerPictureBox.Visible = false;
        cashingPictureBox.Visible = false;

        Height = Convert.ToInt32(Math.Ceiling(Size.Height * 1.5));
    }

    internal string ToSpreadsheetRow { get; }
}
