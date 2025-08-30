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
        ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
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
        this.GridView.Size = new System.Drawing.Size(617, 377);
        this.GridView.TabIndex = 0;
        this.GridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridView_CellMouseDoubleClick);
        // 
        // DataGrid
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.GridView);
        this.Name = "DataGrid";
        this.Size = new System.Drawing.Size(617, 377);
        ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    protected BowlingMegabucks.TournamentManager.App.Controls.Grids.DataGridView GridView;
}
