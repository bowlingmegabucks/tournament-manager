using System;
using System.Windows.Forms;

namespace NewEnglandClassic.Controls;

/// <summary>
///
/// </summary>
public partial class NumericControl : NumericUpDown
{
    /// <summary>
    ///
    /// </summary>
    public NumericControl()
    {
        InitializeComponent();
        Enter += new EventHandler(SelectAll!);
    }

    private void SelectAll(object sender, EventArgs e)
        => Select(0, Value.ToString().Length);
}
