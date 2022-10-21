namespace NortheastMegabuck.Tournaments.Portal;
internal partial class Form : System.Windows.Forms.Form
{
    private readonly IConfiguration _config;
    private readonly TournamentId _id;
    private readonly short _gamesPerSquad;

    
    public Form(IConfiguration config, TournamentId id, string tournamentName, short gamesPerSquad)
    {
        InitializeComponent();

        _config = config;
        _id = id;
        _gamesPerSquad = gamesPerSquad;

        Text = tournamentName;
    }

    private void AddDivisionMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Divisions.Add.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void ViewDivisionsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Divisions.Retrieve.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void AddSquadMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Squads.Add.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void OpenSquadMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Squads.Retrieve.Form(_config, _id, _gamesPerSquad);

        form.ShowDialog(this);
    }

    private void AddSweeperMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Sweepers.Add.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void OpenSweeperMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Sweepers.Retrieve.Form(_config, _id);

        form.ShowDialog(this);
    }

    private void AddRegistrationMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Registrations.Add.Form(_config, _id);

        if (!form.IsDisposed)
        {
            form.ShowDialog(this);
        }
    }

    private void ViewTournamentRegistrationsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Registrations.Retrieve.RetrieveTournamentRegistrationsForm(_config, _id);

        form.ShowDialog(this);
    }

    private void SuperSweeperResultsMenuItem_Click(object sender, EventArgs e)
    {
        using var form = new Sweepers.Results.Form(_config, _id);

        form.ShowDialog();
    }
}
