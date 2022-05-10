using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic.Tournaments.Portal;
public partial class Form : System.Windows.Forms.Form
{
    private readonly IConfiguration _config;
    private readonly Guid _id;

    
    public Form(IConfiguration config, Guid id)
    {
        InitializeComponent();

        _config = config;
        _id = id;
    }

    private void MenuItemDivisionAdd_Click(object sender, EventArgs e)
    {
        using var form = new Divisions.Add.Form(_config, _id);

        form.ShowDialog(this);
    }
}
