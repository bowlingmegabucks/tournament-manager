using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewEnglandClassic.Tournaments.Retrieve;
public partial class Form : System.Windows.Forms.Form, IView
{
    public Form(IConfiguration config)
    {
        InitializeComponent();

        var presenter = new Presenter(config, this);

        presenter.Execute();
    }

    public void BindTournaments(ICollection<IViewModel> viewModels) => throw new NotImplementedException();

    public void DisableOpenTournament() 
        => ButtonOpenTournament.Enabled = false;

    public void DisplayErrorMessage(string message) => throw new NotImplementedException();

}
