
namespace NortheastMegabuck.Controls;
public partial class TournamentSeedingControl : UserControl
{
    internal TournamentSeedingControl(Tournaments.Seeding.IViewModel model)
    {
        InitializeComponent();

        seedLabel.Text = model.Seed.ToString();
        bowlerNameLabel.Text = model.BowlerName;
        scoreLabel.Text = model.Score.ToString();
        highGameLabel.Text = model.HighGame.ToString();
        cashingPictureBox.Visible = model.AtLargeCasher;
    }

    internal TournamentSeedingControl()
    {
        InitializeComponent();

        seedLabel.Text = string.Empty;
        bowlerNameLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        highGameLabel.Text = string.Empty;
        cashingPictureBox.Visible = false;

        Height = Convert.ToInt32(Math.Ceiling(Size.Height * 1.5));
    }
}
