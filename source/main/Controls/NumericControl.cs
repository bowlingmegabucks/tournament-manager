using System.Runtime.Versioning;

namespace BowlingMegabucks.TournamentManager.Controls;

/// <summary>
///
/// </summary>
[SupportedOSPlatform("windows")]
internal sealed partial class NumericControl 
    : NumericUpDown
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
