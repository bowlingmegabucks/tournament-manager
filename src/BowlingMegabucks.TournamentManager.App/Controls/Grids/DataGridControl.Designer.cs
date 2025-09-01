#pragma warning disable CS1591

namespace BowlingMegabucks.TournamentManager.App.Controls.Grids;

partial class DataGridControl<TModel>
{
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        this.GridView = new BowlingMegabucks.TournamentManager.App.Controls.Grids.DataGridView();
        this.paginationPanel = new System.Windows.Forms.Panel();
        this.totalRecordsLabel = new System.Windows.Forms.Label();
        this.pageSizeComboBox = new System.Windows.Forms.ComboBox();
        this.pageSizeLabel = new System.Windows.Forms.Label();
        this.pageComboBox = new System.Windows.Forms.ComboBox();
        this.pageLabel = new System.Windows.Forms.Label();
        this.nextButton = new System.Windows.Forms.Button();
        this.previousButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
        this.paginationPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // GridView
        // 
        this.GridView.AllowUserToAddRows = false;
        this.GridView.AllowUserToDeleteRows = false;
        this.GridView.AllowUserToResizeColumns = false;
        this.GridView.AllowUserToResizeRows = false;
        this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        this.GridView.DefaultCellStyle = dataGridViewCellStyle1;
        this.GridView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.GridView.Location = new System.Drawing.Point(0, 0);
        this.GridView.MultiSelect = false;
        this.GridView.Name = "GridView";
        this.GridView.ReadOnly = true;
        this.GridView.RowTemplate.Height = 25;
        this.GridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.GridView.ShowEditingIcon = false;
        this.GridView.Size = new System.Drawing.Size(617, 337);
        this.GridView.TabIndex = 0;
        this.GridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDoubleClick);
        // 
        // paginationPanel
        // 
        this.paginationPanel.Controls.Add(this.totalRecordsLabel);
        this.paginationPanel.Controls.Add(this.pageSizeComboBox);
        this.paginationPanel.Controls.Add(this.pageSizeLabel);
        this.paginationPanel.Controls.Add(this.pageComboBox);
        this.paginationPanel.Controls.Add(this.pageLabel);
        this.paginationPanel.Controls.Add(this.nextButton);
        this.paginationPanel.Controls.Add(this.previousButton);
        this.paginationPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.paginationPanel.Location = new System.Drawing.Point(0, 337);
        this.paginationPanel.Name = "paginationPanel";
        this.paginationPanel.Padding = new System.Windows.Forms.Padding(10, 8, 10, 7);
        this.paginationPanel.Size = new System.Drawing.Size(617, 40);
        this.paginationPanel.TabIndex = 1;
        this.paginationPanel.Visible = false;
        // 
        // totalRecordsLabel
        // 
        this.totalRecordsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.totalRecordsLabel.AutoSize = true;
        this.totalRecordsLabel.Location = new System.Drawing.Point(480, 12);
        this.totalRecordsLabel.Name = "totalRecordsLabel";
        this.totalRecordsLabel.Size = new System.Drawing.Size(128, 15);
        this.totalRecordsLabel.TabIndex = 6;
        this.totalRecordsLabel.Text = "Showing 0-0 of 0 records";
        // 
        // pageSizeComboBox
        // 
        this.pageSizeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.pageSizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.pageSizeComboBox.FormattingEnabled = true;
        this.pageSizeComboBox.Location = new System.Drawing.Point(415, 9);
        this.pageSizeComboBox.Name = "pageSizeComboBox";
        this.pageSizeComboBox.Size = new System.Drawing.Size(60, 23);
        this.pageSizeComboBox.TabIndex = 5;
        // 
        // pageSizeLabel
        // 
        this.pageSizeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.pageSizeLabel.AutoSize = true;
        this.pageSizeLabel.Location = new System.Drawing.Point(350, 12);
        this.pageSizeLabel.Name = "pageSizeLabel";
        this.pageSizeLabel.Size = new System.Drawing.Size(59, 15);
        this.pageSizeLabel.TabIndex = 4;
        this.pageSizeLabel.Text = "Page Size:";
        // 
        // pageComboBox
        // 
        this.pageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.pageComboBox.FormattingEnabled = true;
        this.pageComboBox.Location = new System.Drawing.Point(191, 9);
        this.pageComboBox.Name = "pageComboBox";
        this.pageComboBox.Size = new System.Drawing.Size(70, 23);
        this.pageComboBox.TabIndex = 3;
        // 
        // pageLabel
        // 
        this.pageLabel.AutoSize = true;
        this.pageLabel.Location = new System.Drawing.Point(150, 12);
        this.pageLabel.Name = "pageLabel";
        this.pageLabel.Size = new System.Drawing.Size(35, 15);
        this.pageLabel.TabIndex = 2;
        this.pageLabel.Text = "Page:";
        // 
        // nextButton
        // 
        this.nextButton.Location = new System.Drawing.Point(81, 8);
        this.nextButton.Name = "nextButton";
        this.nextButton.Size = new System.Drawing.Size(60, 25);
        this.nextButton.TabIndex = 1;
        this.nextButton.Text = "Next";
        this.nextButton.UseVisualStyleBackColor = true;
        // 
        // previousButton
        // 
        this.previousButton.Location = new System.Drawing.Point(10, 8);
        this.previousButton.Name = "previousButton";
        this.previousButton.Size = new System.Drawing.Size(65, 25);
        this.previousButton.TabIndex = 0;
        this.previousButton.Text = "Previous";
        this.previousButton.UseVisualStyleBackColor = true;
        // 
        // DataGrid
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.GridView);
        this.Controls.Add(this.paginationPanel);
        this.Name = "DataGrid";
        this.Size = new System.Drawing.Size(617, 377);
        ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
        this.paginationPanel.ResumeLayout(false);
        this.paginationPanel.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    protected BowlingMegabucks.TournamentManager.App.Controls.Grids.DataGridView GridView;
    private System.Windows.Forms.Panel paginationPanel;
    private System.Windows.Forms.Button previousButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.ComboBox pageComboBox;
    private System.Windows.Forms.Label pageLabel;
    private System.Windows.Forms.ComboBox pageSizeComboBox;
    private System.Windows.Forms.Label pageSizeLabel;
    private System.Windows.Forms.Label totalRecordsLabel;
}
