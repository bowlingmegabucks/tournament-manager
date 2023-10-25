using System.ComponentModel;

namespace NortheastMegabuck.Controls.Grids;

/// <summary>
///
/// </summary>
/// <typeparam name="TModel"></typeparam>
public abstract partial class DataGrid<TModel> : UserControl where TModel : class
{
    /// <summary>
    ///
    /// </summary>
    public event System.EventHandler<GridRowDoubleClickEventArgs>? GridRowDoubleClicked;

    /// <summary>
    ///
    /// </summary>
    protected DataGrid()
    {
        InitializeComponent();

        _models = new List<TModel>();

        AlternateRowColors = true;
        AllowRowSelection = true;

        GridView.AutoGenerateColumns = false;
        GridView.ShowCellToolTips = false;

        GridView.MouseDown += GridView_RightMouseDown!;
    }

    private List<TModel> _models;
    /// <summary>
    ///
    /// </summary>
    protected IEnumerable<TModel> Models 
        => _models;

    /// <summary>
    ///
    /// </summary>
    protected TModel? SelectedRow
        => GridView.CurrentRow?.DataBoundItem as TModel ?? null;

    protected void Remove(TModel model)
    {
        _models.Remove(model);

        Bind();
    }

    /// <summary>
    ///
    /// </summary>
    public ContextMenuStrip? SelectedRowContextMenu
    {
        get => GridView.RowTemplate.ContextMenuStrip;
        set => GridView.RowTemplate.ContextMenuStrip = value;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="models"></param>
    public void Bind(IEnumerable<TModel> models)
    {
        UseWaitCursor = true;

        _models = new List<TModel>(models.ToList());

        Bind();

        UseWaitCursor = false;
    }

    private void Bind()
        => GridView.DataSource = new BindingList<TModel>(_models);

    /// <summary>
    ///
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
    ///
    /// </summary>
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
    ///
    /// </summary>
    public bool AllowRowSelection { get; set; }

    private void GridView_RightMouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right)
        {
            return;
        }

        var grid = (DataGridView)sender;

        var row = grid.HitTest(e.X, e.Y).RowIndex;

        if (row == -1)
        {
            return;
        }

        grid.ClearSelection();

        grid.CurrentCell = grid.Rows[row].Cells.OfType<DataGridViewCell>().First(cell => cell.Visible);

        grid.Rows[row].Selected = true;
    }

    private void GridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            OnGridRowDoubleClicked(new GridRowDoubleClickEventArgs());
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="e"></param>
    private void OnGridRowDoubleClicked(GridRowDoubleClickEventArgs e)
        => GridRowDoubleClicked?.Invoke(this, e);
}

/// <summary>
///
/// </summary>
public class GridRowDoubleClickEventArgs : System.EventArgs
{

    /// <summary>
    ///
    /// </summary>
    public GridRowDoubleClickEventArgs()
    { }
}
