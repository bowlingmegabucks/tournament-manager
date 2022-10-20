
namespace NortheastMegabuck.Controls.Grids;
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
}
