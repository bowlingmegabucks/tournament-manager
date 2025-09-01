using System.ComponentModel;

namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

/// <summary>
/// A custom data grid view control that extends the standard <see cref="System.Windows.Forms.DataGridView"/> with application-specific styling and behavior.
/// This control is designed to work seamlessly with the <see cref="DataGridControl{TModel}"/> base class for consistent grid presentation throughout the application.
/// </summary>
[Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
internal sealed partial class DataGridView
    : System.Windows.Forms.DataGridView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataGridView"/> class.
    /// Sets up the control with default configuration and initializes any custom styling or behavior.
    /// </summary>
    public DataGridView()
    {
        InitializeComponent();
    }
}
