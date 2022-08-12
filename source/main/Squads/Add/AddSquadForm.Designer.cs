namespace NewEnglandClassic.Squads.Add;

partial class Form
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.newSquad = new NewEnglandClassic.Contols.SquadControl();
            this.disclaimerLabel = new System.Windows.Forms.Label();
            this.tournamentFinalsRatioLabel = new System.Windows.Forms.Label();
            this.tournamentCashRatioLabel = new System.Windows.Forms.Label();
            this.tournamentFinalsRatioValue = new System.Windows.Forms.TextBox();
            this.tournamentCashRatioValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 352);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.saveButton.Location = new System.Drawing.Point(430, 352);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // newSquad
            // 
            this.newSquad.CashRatio = null;
            this.newSquad.Complete = false;
            this.newSquad.Date = new System.DateTime(2022, 6, 8, 10, 30, 39, 618);
            this.newSquad.FinalsRatio = null;
            this.newSquad.Location = new System.Drawing.Point(12, 139);
            this.newSquad.MaxPerPair = ((short)(0));
            this.newSquad.Name = "newSquad";
            this.newSquad.NumberOfLanes = ((short)(1));
            this.newSquad.Size = new System.Drawing.Size(519, 207);
            this.newSquad.StartingLane = ((short)(1));
            this.newSquad.TabIndex = 0;
            // 
            // disclaimerLabel
            // 
            this.disclaimerLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.disclaimerLabel.Location = new System.Drawing.Point(12, 9);
            this.disclaimerLabel.Name = "disclaimerLabel";
            this.disclaimerLabel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.disclaimerLabel.Size = new System.Drawing.Size(519, 77);
            this.disclaimerLabel.TabIndex = 8;
            this.disclaimerLabel.Text = "If the desire is to keep the Finals and/or Cash Ratio for this squad the same as " +
    "the tournament default, set the vaules for these fields to zero.";
            // 
            // tournamentFinalsRatioLabel
            // 
            this.tournamentFinalsRatioLabel.AutoSize = true;
            this.tournamentFinalsRatioLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tournamentFinalsRatioLabel.Location = new System.Drawing.Point(12, 86);
            this.tournamentFinalsRatioLabel.Name = "tournamentFinalsRatioLabel";
            this.tournamentFinalsRatioLabel.Size = new System.Drawing.Size(163, 17);
            this.tournamentFinalsRatioLabel.TabIndex = 9;
            this.tournamentFinalsRatioLabel.Text = "Tournament Finals Ratio:";
            // 
            // tournamentCashRatioLabel
            // 
            this.tournamentCashRatioLabel.AutoSize = true;
            this.tournamentCashRatioLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tournamentCashRatioLabel.Location = new System.Drawing.Point(349, 86);
            this.tournamentCashRatioLabel.Name = "tournamentCashRatioLabel";
            this.tournamentCashRatioLabel.Size = new System.Drawing.Size(156, 17);
            this.tournamentCashRatioLabel.TabIndex = 10;
            this.tournamentCashRatioLabel.Text = "Tournament Cash Ratio:";
            // 
            // tournamentFinalsRatioValue
            // 
            this.tournamentFinalsRatioValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tournamentFinalsRatioValue.Location = new System.Drawing.Point(12, 106);
            this.tournamentFinalsRatioValue.Name = "tournamentFinalsRatioValue";
            this.tournamentFinalsRatioValue.ReadOnly = true;
            this.tournamentFinalsRatioValue.Size = new System.Drawing.Size(163, 27);
            this.tournamentFinalsRatioValue.TabIndex = 11;
            this.tournamentFinalsRatioValue.TabStop = false;
            this.tournamentFinalsRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tournamentCashRatioValue
            // 
            this.tournamentCashRatioValue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tournamentCashRatioValue.Location = new System.Drawing.Point(349, 106);
            this.tournamentCashRatioValue.Name = "tournamentCashRatioValue";
            this.tournamentCashRatioValue.ReadOnly = true;
            this.tournamentCashRatioValue.Size = new System.Drawing.Size(156, 27);
            this.tournamentCashRatioValue.TabIndex = 12;
            this.tournamentCashRatioValue.TabStop = false;
            this.tournamentCashRatioValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form
            // 
            this.AcceptButton = this.saveButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(533, 390);
            this.Controls.Add(this.tournamentCashRatioValue);
            this.Controls.Add(this.tournamentFinalsRatioValue);
            this.Controls.Add(this.tournamentCashRatioLabel);
            this.Controls.Add(this.tournamentFinalsRatioLabel);
            this.Controls.Add(this.disclaimerLabel);
            this.Controls.Add(this.newSquad);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Squad";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Button cancelButton;
    private Button saveButton;
    private Contols.SquadControl newSquad;
    private Label disclaimerLabel;
    private Label tournamentFinalsRatioLabel;
    private Label tournamentCashRatioLabel;
    private TextBox tournamentFinalsRatioValue;
    private TextBox tournamentCashRatioValue;
}