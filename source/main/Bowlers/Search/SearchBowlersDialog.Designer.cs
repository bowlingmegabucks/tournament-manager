namespace NewEnglandClassic.Bowlers.Search;

partial class Dialog
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.GroupboxSearchCriteria = new System.Windows.Forms.GroupBox();
            this.ButtonSearch = new System.Windows.Forms.Button();
            this.TextboxEmail = new System.Windows.Forms.TextBox();
            this.labelControl2 = new NewEnglandClassic.Controls.LabelControl();
            this.TextboxFirstName = new System.Windows.Forms.TextBox();
            this.LabelFirstName = new NewEnglandClassic.Controls.LabelControl();
            this.TextboxLastName = new System.Windows.Forms.TextBox();
            this.LabelLastName = new NewEnglandClassic.Controls.LabelControl();
            this.SearchResultsGrid = new NewEnglandClassic.Contols.SearchBowlersGrid();
            this.ButtonSelect = new System.Windows.Forms.Button();
            this.ButtonNew = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.GroupboxSearchCriteria.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupboxSearchCriteria
            // 
            this.GroupboxSearchCriteria.Controls.Add(this.ButtonSearch);
            this.GroupboxSearchCriteria.Controls.Add(this.TextboxEmail);
            this.GroupboxSearchCriteria.Controls.Add(this.labelControl2);
            this.GroupboxSearchCriteria.Controls.Add(this.TextboxFirstName);
            this.GroupboxSearchCriteria.Controls.Add(this.LabelFirstName);
            this.GroupboxSearchCriteria.Controls.Add(this.TextboxLastName);
            this.GroupboxSearchCriteria.Controls.Add(this.LabelLastName);
            this.GroupboxSearchCriteria.Location = new System.Drawing.Point(12, 12);
            this.GroupboxSearchCriteria.Name = "GroupboxSearchCriteria";
            this.GroupboxSearchCriteria.Size = new System.Drawing.Size(832, 125);
            this.GroupboxSearchCriteria.TabIndex = 0;
            this.GroupboxSearchCriteria.TabStop = false;
            this.GroupboxSearchCriteria.Text = "Search Criteria";
            // 
            // ButtonSearch
            // 
            this.ButtonSearch.Location = new System.Drawing.Point(723, 87);
            this.ButtonSearch.Name = "ButtonSearch";
            this.ButtonSearch.Size = new System.Drawing.Size(75, 23);
            this.ButtonSearch.TabIndex = 110;
            this.ButtonSearch.Text = "Search";
            this.ButtonSearch.UseVisualStyleBackColor = true;
            // 
            // TextboxEmail
            // 
            this.TextboxEmail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxEmail.Location = new System.Drawing.Point(499, 47);
            this.TextboxEmail.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.TextboxEmail.Name = "TextboxEmail";
            this.TextboxEmail.PlaceholderText = "joebowler123@usbc.com";
            this.TextboxEmail.Size = new System.Drawing.Size(299, 27);
            this.TextboxEmail.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.AutoSize = true;
            this.labelControl2.Bold = false;
            this.labelControl2.Location = new System.Drawing.Point(499, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Required = false;
            this.labelControl2.Size = new System.Drawing.Size(117, 19);
            this.labelControl2.TabIndex = 109;
            this.labelControl2.TabStop = false;
            this.labelControl2.Text = "Email:";
            // 
            // TextboxFirstName
            // 
            this.TextboxFirstName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxFirstName.Location = new System.Drawing.Point(324, 47);
            this.TextboxFirstName.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.TextboxFirstName.Name = "TextboxFirstName";
            this.TextboxFirstName.PlaceholderText = "Joe";
            this.TextboxFirstName.Size = new System.Drawing.Size(162, 27);
            this.TextboxFirstName.TabIndex = 1;
            // 
            // LabelFirstName
            // 
            this.LabelFirstName.AutoSize = true;
            this.LabelFirstName.Bold = false;
            this.LabelFirstName.Location = new System.Drawing.Point(324, 22);
            this.LabelFirstName.Name = "LabelFirstName";
            this.LabelFirstName.Required = false;
            this.LabelFirstName.Size = new System.Drawing.Size(117, 19);
            this.LabelFirstName.TabIndex = 107;
            this.LabelFirstName.TabStop = false;
            this.LabelFirstName.Text = "First Name:";
            // 
            // TextboxLastName
            // 
            this.TextboxLastName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxLastName.Location = new System.Drawing.Point(12, 47);
            this.TextboxLastName.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.TextboxLastName.Name = "TextboxLastName";
            this.TextboxLastName.PlaceholderText = "Bowler";
            this.TextboxLastName.Size = new System.Drawing.Size(299, 27);
            this.TextboxLastName.TabIndex = 0;
            // 
            // LabelLastName
            // 
            this.LabelLastName.AutoSize = true;
            this.LabelLastName.Bold = false;
            this.LabelLastName.Location = new System.Drawing.Point(12, 22);
            this.LabelLastName.Name = "LabelLastName";
            this.LabelLastName.Required = true;
            this.LabelLastName.Size = new System.Drawing.Size(105, 19);
            this.LabelLastName.TabIndex = 105;
            this.LabelLastName.TabStop = false;
            this.LabelLastName.Text = "Last Name:";
            // 
            // SearchResultsGrid
            // 
            this.SearchResultsGrid.AllowRowSelection = true;
            this.SearchResultsGrid.AlternateRowColors = true;
            this.SearchResultsGrid.Location = new System.Drawing.Point(12, 143);
            this.SearchResultsGrid.Name = "SearchResultsGrid";
            this.SearchResultsGrid.SelectedRowContextMenu = null;
            this.SearchResultsGrid.Size = new System.Drawing.Size(832, 290);
            this.SearchResultsGrid.TabIndex = 1;
            this.SearchResultsGrid.GridRowDoubleClicked += new System.EventHandler<NewEnglandClassic.Controls.GridRowDoubleClickEventArgs>(this.SearchResultsGrid_GridRowDoubleClicked);
            // 
            // ButtonSelect
            // 
            this.ButtonSelect.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonSelect.Location = new System.Drawing.Point(762, 439);
            this.ButtonSelect.Name = "ButtonSelect";
            this.ButtonSelect.Size = new System.Drawing.Size(75, 23);
            this.ButtonSelect.TabIndex = 111;
            this.ButtonSelect.Text = "Select";
            this.ButtonSelect.UseVisualStyleBackColor = true;
            this.ButtonSelect.Click += new System.EventHandler(this.ButtonSelect_Click);
            // 
            // ButtonNew
            // 
            this.ButtonNew.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ButtonNew.Location = new System.Drawing.Point(12, 439);
            this.ButtonNew.Name = "ButtonNew";
            this.ButtonNew.Size = new System.Drawing.Size(99, 23);
            this.ButtonNew.TabIndex = 112;
            this.ButtonNew.Text = "New Bowler";
            this.ButtonNew.UseVisualStyleBackColor = true;
            this.ButtonNew.Click += new System.EventHandler(this.ButtonNew_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(661, 439);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 113;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // Dialog
            // 
            this.AcceptButton = this.ButtonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(849, 473);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonNew);
            this.Controls.Add(this.ButtonSelect);
            this.Controls.Add(this.SearchResultsGrid);
            this.Controls.Add(this.GroupboxSearchCriteria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Bowler";
            this.TopMost = true;
            this.GroupboxSearchCriteria.ResumeLayout(false);
            this.GroupboxSearchCriteria.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private GroupBox GroupboxSearchCriteria;
    private TextBox TextboxEmail;
    private Controls.LabelControl labelControl2;
    private TextBox TextboxFirstName;
    private Controls.LabelControl LabelFirstName;
    private TextBox TextboxLastName;
    private Controls.LabelControl LabelLastName;
    private Button ButtonSearch;
    private Contols.SearchBowlersGrid SearchResultsGrid;
    private Button ButtonSelect;
    private Button ButtonNew;
    private Button ButtonCancel;
}