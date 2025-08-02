
using System.Globalization;
using System.Runtime.Versioning;

namespace BowlingMegabucks.TournamentManager.Controls;

[SupportedOSPlatform("windows")]
internal sealed partial class TournamentSeedingControl
    : UserControl
{
    internal TournamentSeedingControl(Tournaments.Seeding.IViewModel model)
    {
        InitializeComponent();

        ToSpreadsheetRow = $"{model.Seed}\t{model.BowlerName}\t{model.Score}\t{model.HighGame}\t{model.AtLargeCasher}";

        seedLabel.Text = model.Seed.ToString(CultureInfo.InvariantCulture);
        bowlerNameLabel.Text = model.BowlerName;
        scoreLabel.Text = model.Score.ToString(CultureInfo.InvariantCulture);
        highGameLabel.Text = model.HighGame.ToString(CultureInfo.InvariantCulture);
        cashingPictureBox.Visible = model.AtLargeCasher;
    }

    internal TournamentSeedingControl()
    {
        InitializeComponent();

        ToSpreadsheetRow = string.Empty;

        seedLabel.Text = string.Empty;
        bowlerNameLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        highGameLabel.Text = string.Empty;
        cashingPictureBox.Visible = false;

        Height = Convert.ToInt32(Math.Ceiling(Size.Height * 1.5));
    }

    internal string ToSpreadsheetRow { get; }
}
