using Microsoft.Extensions.Configuration;

namespace NewEnglandClassic;

public partial class Form1 : Form
{
    private readonly IConfiguration _config;

    public Form1(IConfiguration config)
    {
        InitializeComponent();

        _config = config;
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        var result = new PingBusiness(_config).Database();

        MessageBox.Show(result ? "Database Connected" : "Database Not Connected");
    }
}
