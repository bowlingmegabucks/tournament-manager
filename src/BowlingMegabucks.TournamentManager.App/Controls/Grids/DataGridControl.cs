using System.ComponentModel;
using System.Globalization;

namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

/// <summary>
/// Base abstract class for data grid controls that provides common functionality for displaying and managing collections of data.
/// Supports row selection, context menus, filtering, double-click events, and pagination.
/// </summary>
/// <typeparam name="TModel">The type of model objects to be displayed in the grid. Must be a reference type.</typeparam>
internal abstract partial class DataGridControl<TModel> : UserControl where TModel : class
{
    /// <summary>
    /// Occurs when a grid row is double-clicked with the left mouse button.
    /// </summary>
    public event EventHandler<GridRowDoubleClickEventArgs>? GridRowDoubleClicked;

    /// <summary>
    /// Occurs when pagination settings change (page number or page size).
    /// </summary>
    public event EventHandler<PagingChangeEventArgs>? PagingChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="DataGridControl{TModel}"/> class.
    /// Sets up default grid properties including auto-generation settings, row colors, and event handlers.
    /// </summary>
    protected DataGridControl()
    {
        InitializeComponent();

        _models = [];
        _pageSizeOptions = [10];

        AlternateRowColors = true;
        AllowRowSelection = true;
        PaginationEnabled = false;
        CurrentPage = 1;
        PageSize = 10;
        TotalRecords = 0;

        GridView.AutoGenerateColumns = false;
        GridView.ShowCellToolTips = false;

        GridView.MouseDown += GridView_RightMouseDown!;

        // Wire up pagination control events
        WirePaginationEvents();

        // Handle resize events for responsive layout
        Resize += DataGridControl_Resize;
    }

    private BindingList<TModel> _models;
    private List<int> _pageSizeOptions;
    private bool _paginationEnabled;
    private int _currentPage;
    private int _pageSize;
    private int _totalRecords;

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
    /// Gets or sets a value indicating whether pagination is enabled for the grid.
    /// </summary>
    /// <value><see langword="true"/> if pagination is enabled; otherwise, <see langword="false"/>.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public bool PaginationEnabled
    {
        get => _paginationEnabled;
        set
        {
            _paginationEnabled = value;
            paginationPanel.Visible = value;
            if (value)
            {
                RepositionPaginationControls();
            }
        }
    }

    /// <summary>
    /// Gets or sets the current page number (1-based).
    /// </summary>
    /// <value>The current page number.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (value < 1) value = 1;
            _currentPage = value;
            UpdatePaginationControls();
        }
    }

    /// <summary>
    /// Gets or sets the page size (number of records per page).
    /// </summary>
    /// <value>The page size.</value>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 1)
                value = 1;
            if (!_pageSizeOptions.Contains(value))
            {
                string errorMessage = string.Create(CultureInfo.CurrentCulture,$"PageSize must be one of the values in PageSizeOptions. Value: {value:N0}");
                throw new ArgumentOutOfRangeException(nameof(value), errorMessage);
            }

            _pageSize = value;
            UpdatePaginationControls();
        }
    }

    /// <summary>
    /// Gets or sets the total number of records available.
    /// </summary>
    /// <value>The total number of records.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int TotalRecords
    {
        get => _totalRecords;
        set
        {
            _totalRecords = Math.Max(0, value);
            UpdatePaginationControls();
        }
    }

    /// <summary>
    /// Gets or sets the available page size options.
    /// </summary>
    /// <value>A collection of integers representing available page sizes.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public List<int> PageSizeOptions
    {
        get => _pageSizeOptions;
        set
        {
            _pageSizeOptions = value ?? [10];
            if (_pageSizeOptions.Count == 0)
                _pageSizeOptions.Add(10);

            _pageSizeOptions.Sort();
            UpdatePageSizeDropdown();
        }
    }

    /// <summary>
    /// Gets the total number of pages based on the current page size and total records.
    /// </summary>
    public int TotalPages
        => TotalRecords > 0 ? (int)Math.Ceiling((double)TotalRecords / PageSize) : 1;

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
    /// This replaces any existing data in the grid and updates pagination.
    /// </summary>
    /// <param name="models">The collection of model objects to bind to the grid.</param>
    /// <param name="totalRecords">The total number of records available (for pagination).</param>
    public void Bind(IEnumerable<TModel> models, int totalRecords)
    {
        UseWaitCursor = true;

        _models = [.. models.ToList()];
        TotalRecords = totalRecords;
        Bind();
        // Ensure pagination controls are updated (dropdowns, labels, etc.)
        UpdatePaginationControls();

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

    /// <summary>
    /// Raises the <see cref="PagingChanged"/> event.
    /// </summary>
    /// <param name="e">The event arguments containing pagination information.</param>
    private void OnPagingChanged(PagingChangeEventArgs e)
        => PagingChanged?.Invoke(this, e);

    /// <summary>
    /// Handles the resize event of the control to reposition pagination controls.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event arguments.</param>
    private void DataGridControl_Resize(object? sender, EventArgs e)
    {
        if (PaginationEnabled && paginationPanel.Visible)
        {
            RepositionPaginationControls();
        }
    }

    #region Pagination Methods

    /// <summary>
    /// Wires up the pagination control events.
    /// </summary>
    private void WirePaginationEvents()
    {
        previousButton.Click += (s, e) => NavigateToPreviousPage();
        nextButton.Click += (s, e) => NavigateToNextPage();
        pageComboBox.SelectedIndexChanged += (s, e) => OnPageSelectionChanged();
        pageSizeComboBox.SelectedIndexChanged += (s, e) => OnPageSizeChanged();
    }

    /// <summary>
    /// Repositions the pagination controls to ensure proper spacing and prevent overlapping when the control is resized.
    /// </summary>
    private void RepositionPaginationControls()
    {
        if (!PaginationEnabled || !paginationPanel.Visible) return;

        // Suspend layout to prevent flickering
        paginationPanel.SuspendLayout();

        try
        {
            const int controlPadding = 5;
            const int verticalOffset = 8;
            int panelWidth = paginationPanel.ClientSize.Width - paginationPanel.Padding.Left - paginationPanel.Padding.Right;

            int currentX = PositionLeftAlignedControls(controlPadding, verticalOffset);
            PositionRightAlignedControls(currentX, panelWidth, controlPadding, verticalOffset);
        }
        finally
        {
            paginationPanel.ResumeLayout();
        }
    }

    /// <summary>
    /// Positions the left-aligned pagination controls (Previous, Next, Page controls).
    /// </summary>
    /// <param name="controlPadding">The padding between controls.</param>
    /// <param name="verticalOffset">The vertical offset from the top of the panel.</param>
    /// <returns>The X position where the next control should be placed.</returns>
    private int PositionLeftAlignedControls(int controlPadding, int verticalOffset)
    {
        int currentX = paginationPanel.Padding.Left;

        // Position Previous button
        previousButton.Location = new Point(currentX, verticalOffset);
        currentX += previousButton.Width + controlPadding;

        // Position Next button
        nextButton.Location = new Point(currentX, verticalOffset);
        currentX += nextButton.Width + controlPadding;

        // Position Page label and combo
        pageLabel.Location = new Point(currentX, verticalOffset + 4); // Slight offset for label alignment
        currentX += pageLabel.Width + controlPadding;

        pageComboBox.Location = new Point(currentX, verticalOffset);
        currentX += pageComboBox.Width + controlPadding;

        return currentX;
    }

    /// <summary>
    /// Positions the right-aligned pagination controls (Page Size and Total Records).
    /// </summary>
    /// <param name="leftControlsEndX">The X position where the left-aligned controls end.</param>
    /// <param name="panelWidth">The total width available in the panel.</param>
    /// <param name="controlPadding">The padding between controls.</param>
    /// <param name="verticalOffset">The vertical offset from the top of the panel.</param>
    private void PositionRightAlignedControls(int leftControlsEndX, int panelWidth, int controlPadding, int verticalOffset)
    {
        // Calculate space needed for right-aligned controls
        int totalRecordsWidth = totalRecordsLabel.Width;
        int pageSizeComboWidth = pageSizeComboBox.Width;
        int pageSizeLabelWidth = pageSizeLabel.Width;
        int rightControlsWidth = pageSizeLabelWidth + controlPadding + pageSizeComboWidth + controlPadding + totalRecordsWidth;
        int availableWidth = panelWidth - leftControlsEndX;

        // Only show page size controls and total records if there's enough space
        if (availableWidth >= rightControlsWidth + (controlPadding * 2))
        {
            PositionAllRightControls(panelWidth, totalRecordsWidth, pageSizeComboWidth, pageSizeLabelWidth, controlPadding, verticalOffset);
        }
        else
        {
            PositionLimitedRightControls(panelWidth, totalRecordsWidth, controlPadding, verticalOffset, availableWidth);
        }
    }

    /// <summary>
    /// Positions all right-aligned controls when there's sufficient space.
    /// </summary>
    private void PositionAllRightControls(int panelWidth, int totalRecordsWidth, int pageSizeComboWidth, int pageSizeLabelWidth, int controlPadding, int verticalOffset)
    {
        // Position from right to left
        int rightX = panelWidth;

        // Total records label (rightmost)
        totalRecordsLabel.Location = new Point(rightX - totalRecordsWidth, verticalOffset + 4);
        rightX -= totalRecordsWidth + controlPadding;

        // Page size combo
        pageSizeComboBox.Location = new Point(rightX - pageSizeComboWidth, verticalOffset);
        rightX -= pageSizeComboWidth + controlPadding;

        // Page size label
        pageSizeLabel.Location = new Point(rightX - pageSizeLabelWidth, verticalOffset + 4);

        // Ensure all right-aligned controls are visible
        pageSizeLabel.Visible = true;
        pageSizeComboBox.Visible = true;
        totalRecordsLabel.Visible = true;
    }

    /// <summary>
    /// Positions limited right-aligned controls when space is constrained.
    /// </summary>
    private void PositionLimitedRightControls(int panelWidth, int totalRecordsWidth, int controlPadding, int verticalOffset, int availableWidth)
    {
        // Hide right-aligned controls if not enough space
        pageSizeLabel.Visible = false;
        pageSizeComboBox.Visible = false;

        // Only hide total records if there's really no space
        if (availableWidth < totalRecordsWidth + controlPadding)
        {
            totalRecordsLabel.Visible = false;
        }
        else
        {
            totalRecordsLabel.Location = new Point(panelWidth - totalRecordsWidth, verticalOffset + 4);
            totalRecordsLabel.Visible = true;
        }
    }

    /// <summary>
    /// Navigates to the previous page if available.
    /// </summary>
    private void NavigateToPreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            OnPagingChanged(new PagingChangeEventArgs(CurrentPage, PageSize));
        }
    }

    /// <summary>
    /// Navigates to the next page if available.
    /// </summary>
    private void NavigateToNextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
            OnPagingChanged(new PagingChangeEventArgs(CurrentPage, PageSize));
        }
    }

    /// <summary>
    /// Handles page selection changes from the page dropdown.
    /// </summary>
    private void OnPageSelectionChanged()
    {
        if (pageComboBox.SelectedItem is int selectedPage && selectedPage != CurrentPage)
        {
            CurrentPage = selectedPage;
            OnPagingChanged(new PagingChangeEventArgs(CurrentPage, PageSize));
        }
    }

    /// <summary>
    /// Handles page size changes from the page size dropdown.
    /// </summary>
    private void OnPageSizeChanged()
    {
        if (pageSizeComboBox.SelectedItem is int selectedPageSize && selectedPageSize != PageSize)
        {
            PageSize = selectedPageSize;
            CurrentPage = 1; // Reset to first page when page size changes
            OnPagingChanged(new PagingChangeEventArgs(CurrentPage, PageSize));
        }
    }

    /// <summary>
    /// Updates the pagination controls based on current state.
    /// </summary>
    private void UpdatePaginationControls()
    {
        if (!PaginationEnabled) return;

        // Update previous/next button states
        previousButton.Enabled = CurrentPage > 1;
        nextButton.Enabled = CurrentPage < TotalPages;

        // Update page dropdown
        UpdatePageDropdown();

        // Update page size dropdown
        if (pageSizeComboBox.SelectedItem?.Equals(PageSize) != true)
        {
            pageSizeComboBox.SelectedItem = PageSize;
        }

        // Update total records label
        UpdateTotalRecordsLabel();

        // Reposition controls after updates
        RepositionPaginationControls();
    }

    /// <summary>
    /// Updates the page dropdown with available pages.
    /// </summary>
    private void UpdatePageDropdown()
    {
        pageComboBox.Items.Clear();

        for (int i = 1; i <= TotalPages; i++)
        {
            pageComboBox.Items.Add(i);
        }

        if (CurrentPage <= TotalPages && CurrentPage >= 1)
        {
            pageComboBox.SelectedItem = CurrentPage;
        }
    }

    /// <summary>
    /// Updates the page size dropdown with available options.
    /// </summary>
    private void UpdatePageSizeDropdown()
    {
        pageSizeComboBox.Items.Clear();

        foreach (int size in _pageSizeOptions.Order())
        {
            pageSizeComboBox.Items.Add(size);
        }

        if (_pageSizeOptions.Contains(PageSize))
        {
            pageSizeComboBox.SelectedItem = PageSize;
        }
        else if (pageSizeComboBox.Items.Count > 0)
        {
            pageSizeComboBox.SelectedIndex = 0;
            if (pageSizeComboBox.SelectedItem is int newPageSize)
            {
                PageSize = newPageSize;
            }
        }
    }

    /// <summary>
    /// Updates the total records label.
    /// </summary>
    private void UpdateTotalRecordsLabel()
    {
        int startRecord = TotalRecords > 0 ? ((CurrentPage - 1) * PageSize) + 1 : 0;
        int endRecord = Math.Min(CurrentPage * PageSize, TotalRecords);

        totalRecordsLabel.Text = string.Format(
            CultureInfo.CurrentCulture,
            "Showing {0:N0}-{1:N0} of {2:N0} records",
            startRecord,
            endRecord,
            TotalRecords);
    }

    #endregion
}
