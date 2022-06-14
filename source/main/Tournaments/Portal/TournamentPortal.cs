namespace NewEnglandClassic.Tournaments.Portal;
public partial class Form : System.Windows.Forms.Form
{
    private readonly IConfiguration _config;
    private readonly Guid _id;

    
    public Form(IConfiguration config, Guid id, string tournamentName)
    {
        InitializeComponent();

        _config = config;
        _id = id;

        Text = tournamentName;
    }

    private void MenuItemDivisionAdd_Click(object sender, EventArgs e)
    {
        using var form = new Divisions.Add.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void MenuItemDivisionsView_Click(object sender, EventArgs e)
    {
        using var form = new Divisions.Retrieve.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void MenuItemSquadsAdd_Click(object sender, EventArgs e)
    {
        using var form = new Squads.Add.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void MenuItemSquadsOpen_Click(object sender, EventArgs e)
    {
        using var form = new Squads.Retrieve.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void MenuItemSweepersAdd_Click(object sender, EventArgs e)
    {
        using var form = new Sweepers.Add.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void MenuItemOpenSweeper_Click(object sender, EventArgs e)
    {
        using var form = new Sweepers.Retrieve.Form(_config, _id);

        form.ShowDialog(this);
    }
}
