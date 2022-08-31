
namespace NortheastMegabuck.Squads.Portal;
public partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    private readonly SquadId _id;
    public Form(IConfiguration config, SquadId id)
    {
        InitializeComponent();

        _config = config;
        _id = id;

        new Presenter(config, this).Load();
    }

    public void SetPortalTitle(string title)
        => Text = title;

    SquadId IView.Id
        => _id;

    public void DisplayError(string message)
        => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}
