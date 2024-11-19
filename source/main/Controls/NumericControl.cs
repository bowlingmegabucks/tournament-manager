using System.Runtime.Versioning;

namespace NortheastMegabuck.Controls;

/// <summary>
///
/// </summary>
[SupportedOSPlatform("windows")]
internal partial class NumericControl : NumericUpDown
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
        => Select(0, Text.Length);
}
