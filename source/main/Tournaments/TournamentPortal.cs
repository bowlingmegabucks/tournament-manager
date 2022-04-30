using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewEnglandClassic.Tournaments;
public partial class Portal : Form
{
    private readonly IConfiguration _config;
    private readonly Guid _id;

    
    public Portal(IConfiguration config, Guid id)
    {
        InitializeComponent();

        _config = config;
        _id = id;
    }
}
