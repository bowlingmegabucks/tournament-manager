using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tournaments.Add;
internal partial class Form : System.Windows.Forms.Form, IView
{
    private readonly IConfiguration _config;
    
    public Form(IConfiguration config)
    {
        InitializeComponent();

        _config = config;
    }

    public bool IsValid()
        => ValidateChildren();

    public IViewModel Tournament
        => TournamentControlNew;

    public void KeepOpen()
        => DialogResult = DialogResult.None;

    public void DisplayErrors(IEnumerable<string> errorMessages)
        => MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

    public void DisplayMessage(string message)
        => MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    private void ButtonSave_Click(object sender, EventArgs e)
        => new Presenter(_config, this).Execute();
}
