namespace NortheastMegabuck.Bowlers.Search;

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
            this.searchCriteriaGroupBox = new System.Windows.Forms.GroupBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.emailText = new System.Windows.Forms.TextBox();
            this.emailLabel = new NortheastMegabuck.Controls.LabelControl();
            this.firstNameText = new System.Windows.Forms.TextBox();
            this.firstNameLabel = new NortheastMegabuck.Controls.LabelControl();
            this.lastNameText = new System.Windows.Forms.TextBox();
            this.lastNameLabel = new NortheastMegabuck.Controls.LabelControl();
            this.searchResultsGrid = new NortheastMegabuck.Controls.Grids.SearchBowlersGrid();
            this.selectButton = new System.Windows.Forms.Button();
            this.newBowlerButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.searchCriteriaGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchCriteriaGroupBox
            // 
            this.searchCriteriaGroupBox.Controls.Add(this.searchButton);
            this.searchCriteriaGroupBox.Controls.Add(this.emailText);
            this.searchCriteriaGroupBox.Controls.Add(this.emailLabel);
            this.searchCriteriaGroupBox.Controls.Add(this.firstNameText);
            this.searchCriteriaGroupBox.Controls.Add(this.firstNameLabel);
            this.searchCriteriaGroupBox.Controls.Add(this.lastNameText);
            this.searchCriteriaGroupBox.Controls.Add(this.lastNameLabel);
            this.searchCriteriaGroupBox.Location = new System.Drawing.Point(12, 12);
            this.searchCriteriaGroupBox.Name = "searchCriteriaGroupBox";
            this.searchCriteriaGroupBox.Size = new System.Drawing.Size(832, 125);
            this.searchCriteriaGroupBox.TabIndex = 0;
            this.searchCriteriaGroupBox.TabStop = false;
            this.searchCriteriaGroupBox.Text = "Search Criteria";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(723, 87);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 110;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // emailText
            // 
            this.emailText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.emailText.Location = new System.Drawing.Point(499, 47);
            this.emailText.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.emailText.Name = "emailText";
            this.emailText.PlaceholderText = "joebowler123@usbc.com";
            this.emailText.Size = new System.Drawing.Size(299, 27);
            this.emailText.TabIndex = 2;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Bold = false;
            this.emailLabel.Location = new System.Drawing.Point(499, 22);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Required = false;
            this.emailLabel.Size = new System.Drawing.Size(117, 19);
            this.emailLabel.TabIndex = 109;
            this.emailLabel.TabStop = false;
            this.emailLabel.Text = "Email:";
            // 
            // firstNameText
            // 
            this.firstNameText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.firstNameText.Location = new System.Drawing.Point(324, 47);
            this.firstNameText.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.firstNameText.Name = "firstNameText";
            this.firstNameText.PlaceholderText = "Joe";
            this.firstNameText.Size = new System.Drawing.Size(162, 27);
            this.firstNameText.TabIndex = 1;
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Bold = false;
            this.firstNameLabel.Location = new System.Drawing.Point(324, 22);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Required = false;
            this.firstNameLabel.Size = new System.Drawing.Size(117, 19);
            this.firstNameLabel.TabIndex = 107;
            this.firstNameLabel.TabStop = false;
            this.firstNameLabel.Text = "First Name:";
            // 
            // lastNameText
            // 
            this.lastNameText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lastNameText.Location = new System.Drawing.Point(12, 47);
            this.lastNameText.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.lastNameText.Name = "lastNameText";
            this.lastNameText.PlaceholderText = "Bowler";
            this.lastNameText.Size = new System.Drawing.Size(299, 27);
            this.lastNameText.TabIndex = 0;
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Bold = false;
            this.lastNameLabel.Location = new System.Drawing.Point(12, 22);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Required = true;
            this.lastNameLabel.Size = new System.Drawing.Size(105, 19);
            this.lastNameLabel.TabIndex = 105;
            this.lastNameLabel.TabStop = false;
            this.lastNameLabel.Text = "Last Name:";
            // 
            // searchResultsGrid
            // 
            this.searchResultsGrid.AllowRowSelection = true;
            this.searchResultsGrid.AlternateRowColors = true;
            this.searchResultsGrid.Location = new System.Drawing.Point(12, 143);
            this.searchResultsGrid.Name = "searchResultsGrid";
            this.searchResultsGrid.SelectedRowContextMenu = null;
            this.searchResultsGrid.Size = new System.Drawing.Size(832, 290);
            this.searchResultsGrid.TabIndex = 1;
            this.searchResultsGrid.GridRowDoubleClicked += new System.EventHandler<NortheastMegabuck.Controls.Grids.GridRowDoubleClickEventArgs>(this.SearchResultsGrid_GridRowDoubleClicked);
            // 
            // selectButton
            // 
            this.selectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.selectButton.Location = new System.Drawing.Point(762, 439);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(75, 23);
            this.selectButton.TabIndex = 111;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.SelectButton_Click);
            // 
            // newBowlerButton
            // 
            this.newBowlerButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.newBowlerButton.Location = new System.Drawing.Point(12, 439);
            this.newBowlerButton.Name = "newBowlerButton";
            this.newBowlerButton.Size = new System.Drawing.Size(99, 23);
            this.newBowlerButton.TabIndex = 112;
            this.newBowlerButton.Text = "New Bowler";
            this.newBowlerButton.UseVisualStyleBackColor = true;
            this.newBowlerButton.Click += new System.EventHandler(this.NewButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(661, 439);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 113;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // Dialog
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(849, 473);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.newBowlerButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.searchResultsGrid);
            this.Controls.Add(this.searchCriteriaGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Bowler";
            this.TopMost = true;
            this.searchCriteriaGroupBox.ResumeLayout(false);
            this.searchCriteriaGroupBox.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private GroupBox searchCriteriaGroupBox;
    private TextBox emailText;
    private Controls.LabelControl emailLabel;
    private TextBox firstNameText;
    private Controls.LabelControl firstNameLabel;
    private TextBox lastNameText;
    private Controls.LabelControl lastNameLabel;
    private Button searchButton;
    private Controls.Grids.SearchBowlersGrid searchResultsGrid;
    private Button selectButton;
    private Button newBowlerButton;
    private Button cancelButton;
}