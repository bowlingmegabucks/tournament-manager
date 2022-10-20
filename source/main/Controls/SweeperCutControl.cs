
namespace NortheastMegabuck.Controls;
public partial class SweeperCutControl : UserControl
{
    internal SweeperCutControl(Sweepers.Cut.IViewModel model)
    {
        InitializeComponent();

        placeLabel.Text = model.Place.ToString();
        bowlerNameLabel.Text = model.BowlerName;
        scoreLabel.Text = model.Score.ToString();
        highGameLabel.Text = model.HighGame.ToString();
        cashingPictureBox.Visible = model.Casher;
    }

    internal SweeperCutControl()
    {
        InitializeComponent();

        placeLabel.Text = string.Empty;
        bowlerNameLabel.Text = string.Empty;
        scoreLabel.Text = string.Empty;
        highGameLabel.Text = string.Empty;
        cashingPictureBox.Visible = false;

        Height = Convert.ToInt32(Math.Ceiling(Size.Height * 1.5));
    }
}
