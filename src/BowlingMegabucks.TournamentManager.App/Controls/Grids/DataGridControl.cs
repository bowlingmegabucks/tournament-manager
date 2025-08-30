using System.ComponentModel;

namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

/// <summary>
/// Base abstract class for data grid controls that provides common functionality for displaying and managing collections of data.
/// Supports row selection, context menus, filtering, and double-click events.
/// </summary>
/// <typeparam name="TModel">The type of model objects to be displayed in the grid. Must be a reference type.</typeparam>
internal abstract partial class DataGridControl<TModel> : UserControl where TModel : class
{
    /// <summary>
    /// Occurs when a grid row is double-clicked with the left mouse button.
    /// </summary>
    public event System.EventHandler<GridRowDoubleClickEventArgs>? GridRowDoubleClicked;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGridControl{TModel}"/> class.
    /// Sets up default grid properties including auto-generation settings, row colors, and event handlers.
    /// </summary>
    protected DataGridControl()
    {
        InitializeComponent();

        _models = [];

        AlternateRowColors = true;
        AllowRowSelection = true;

        GridView.AutoGenerateColumns = false;
        GridView.ShowCellToolTips = false;

        GridView.MouseDown += GridView_RightMouseDown!;
    }

    private BindingList<TModel> _models;

    /// <summary>
    /// Gets the collection of models currently bound to the grid.
    /// </summary>
    /// <value>An enumerable collection of model objects of type <typeparamref name="TModel"/>.</value>
    protected IEnumerable<TModel> Models
        => _models;

    /// <summary>
    /// Gets the currently selected row's data model.
    /// </summary>
    /// <value>The model object bound to the currently selected row, or <see langword="null"/> if no row is selected.</value>
    protected TModel? SelectedRow
        => GridView.CurrentRow?.DataBoundItem as TModel ?? null;

    /// <summary>
    /// Removes the specified model from the grid and refreshes the display.
    /// </summary>
    /// <param name="model">The model object to remove from the grid.</param>
    protected void Remove(TModel model)
    {
        _models.Remove(model);

        Bind();
    }

    /// <summary>
    /// Gets or sets the context menu that appears when right-clicking on a grid row.
    /// </summary>
    /// <value>A <see cref="ContextMenuStrip"/> to display for row context menus, or <see langword="null"/> if no context menu is used.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ContextMenuStrip? SelectedRowContextMenu
    {
        get => GridView.RowTemplate.ContextMenuStrip;
        set => GridView.RowTemplate.ContextMenuStrip = value;
    }

    /// <summary>
    /// Binds a collection of models to the grid and displays a wait cursor during the operation.
    /// This replaces any existing data in the grid.
    /// </summary>
    /// <param name="models">The collection of model objects to bind to the grid.</param>
    public void Bind(IEnumerable<TModel> models)
    {
        UseWaitCursor = true;

        _models = [.. models.ToList()];

        Bind();

        UseWaitCursor = false;
    }

    /// <summary>
    /// Internal method that binds the current models collection to the grid view.
    /// </summary>
    private void Bind()
        => GridView.DataSource = _models;

    /// <summary>
    /// Applies a filter to the grid by displaying only the specified subset of models.
    /// </summary>
    /// <param name="filteredModel">The filtered collection of models to display.</param>
    protected void Filter(IEnumerable<TModel> filteredModel)
        => GridView.DataSource = new BindingList<TModel>([.. filteredModel]);

    /// <summary>
    /// Clears any applied filters and displays all models in the grid.
    /// </summary>
    protected void ClearFilter()
        => Bind();

    /// <summary>
    /// Refreshes the grid display by updating and redrawing the control.
    /// Shows a wait cursor during the refresh operation.
    /// </summary>
    public void RefreshData()
    {
        UseWaitCursor = true;

        GridView.Update();
        GridView.Refresh();

        UseWaitCursor = false;
    }

    private bool _alternateRowColors;

    /// <summary>
    /// Gets or sets a value indicating whether alternating row colors are enabled.
    /// When enabled, alternating rows display with a light gray background color.
    /// </summary>
    /// <value><see langword="true"/> if alternating row colors are enabled; otherwise, <see langword="false"/>.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AlternateRowColors
    {
        get => _alternateRowColors;
        set
        {
            _alternateRowColors = value;

            GridView.AlternatingRowsDefaultCellStyle.BackColor = AlternateRowColors ? Color.FromArgb(224, 224, 224) : GridView.RowsDefaultCellStyle.BackColor;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether row selection is allowed in the grid.
    /// </summary>
    /// <value><see langword="true"/> if row selection is allowed; otherwise, <see langword="false"/>.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool AllowRowSelection { get; set; }

    /// <summary>
    /// Handles right mouse button clicks on the grid view.
    /// Selects the clicked row and prepares it for context menu display.
    /// </summary>
    /// <param name="sender">The source of the event (DataGridView).</param>
    /// <param name="e">The mouse event arguments containing click information.</param>
    private void GridView_RightMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
        {
            return;
        }

        var grid = (DataGridView)sender;

        int row = grid.HitTest(e.X, e.Y).RowIndex;

        if (row == -1)
        {
            return;
        }

        grid.ClearSelection();

        grid.CurrentCell = grid.Rows[row].Cells.OfType<DataGridViewCell>().First(cell => cell.Visible);

        grid.Rows[row].Selected = true;
    }

    /// <summary>
    /// Handles double-click events on grid cells.
    /// Raises the <see cref="GridRowDoubleClicked"/> event when a left mouse button double-click occurs.
    /// </summary>
    /// <param name="sender">The source of the event (DataGridView).</param>
    /// <param name="e">The cell mouse event arguments containing click information.</param>
    private void GridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            OnGridRowDoubleClicked(new GridRowDoubleClickEventArgs());
        }
    }

    /// <summary>
    /// Raises the <see cref="GridRowDoubleClicked"/> event.
    /// </summary>
    /// <param name="e">The event arguments containing information about the double-click event.</param>
    private void OnGridRowDoubleClicked(GridRowDoubleClickEventArgs e)
        => GridRowDoubleClicked?.Invoke(this, e);
}
