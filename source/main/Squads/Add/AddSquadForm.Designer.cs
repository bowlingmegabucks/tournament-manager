namespace NortheastMegabuck.Squads.Add;

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
        cancelButton = new Button();
        saveButton = new Button();
        newSquad = new Controls.SquadControl();
        disclaimerLabel = new Label();
        tournamentFinalsRatioLabel = new Label();
        tournamentCashRatioLabel = new Label();
        tournamentFinalsRatioValue = new TextBox();
        tournamentCashRatioValue = new TextBox();
        tournamentEntryFeeValue = new TextBox();
        tournamentEntryFeeLabel = new Label();
        SuspendLayout();
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 352);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 2;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // saveButton
        // 
        saveButton.DialogResult = DialogResult.OK;
        saveButton.Location = new Point(430, 352);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 1;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // newSquad
        // 
        newSquad.CashRatio = null;
        newSquad.Complete = false;
        newSquad.Date = new DateTime(2022, 6, 8, 10, 30, 39, 618);
        newSquad.FinalsRatio = null;
        newSquad.Location = new Point(12, 139);
        newSquad.MaxPerPair = 0;
        newSquad.Name = "newSquad";
        newSquad.NumberOfLanes = 1;
        newSquad.Size = new Size(519, 207);
        newSquad.StartingLane = 1;
        newSquad.TabIndex = 0;
        // 
        // disclaimerLabel
        // 
        disclaimerLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
        disclaimerLabel.Location = new Point(12, 9);
        disclaimerLabel.Name = "disclaimerLabel";
        disclaimerLabel.Padding = new Padding(0, 0, 0, 10);
        disclaimerLabel.Size = new Size(519, 77);
        disclaimerLabel.TabIndex = 8;
        disclaimerLabel.Text = "If the desire is to keep the Finals, Cash Ratio, and/or Entry Fee for this squad the same as the tournament default, set the vaules for these fields to zero.";
        // 
        // tournamentFinalsRatioLabel
        // 
        tournamentFinalsRatioLabel.AutoSize = true;
        tournamentFinalsRatioLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        tournamentFinalsRatioLabel.Location = new Point(176, 86);
        tournamentFinalsRatioLabel.Name = "tournamentFinalsRatioLabel";
        tournamentFinalsRatioLabel.Size = new Size(163, 17);
        tournamentFinalsRatioLabel.TabIndex = 9;
        tournamentFinalsRatioLabel.Text = "Tournament Finals Ratio:";
        // 
        // tournamentCashRatioLabel
        // 
        tournamentCashRatioLabel.AutoSize = true;
        tournamentCashRatioLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        tournamentCashRatioLabel.Location = new Point(349, 86);
        tournamentCashRatioLabel.Name = "tournamentCashRatioLabel";
        tournamentCashRatioLabel.Size = new Size(156, 17);
        tournamentCashRatioLabel.TabIndex = 10;
        tournamentCashRatioLabel.Text = "Tournament Cash Ratio:";
        // 
        // tournamentFinalsRatioValue
        // 
        tournamentFinalsRatioValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        tournamentFinalsRatioValue.Location = new Point(176, 106);
        tournamentFinalsRatioValue.Name = "tournamentFinalsRatioValue";
        tournamentFinalsRatioValue.ReadOnly = true;
        tournamentFinalsRatioValue.Size = new Size(163, 27);
        tournamentFinalsRatioValue.TabIndex = 11;
        tournamentFinalsRatioValue.TabStop = false;
        tournamentFinalsRatioValue.TextAlign = HorizontalAlignment.Right;
        // 
        // tournamentCashRatioValue
        // 
        tournamentCashRatioValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        tournamentCashRatioValue.Location = new Point(349, 106);
        tournamentCashRatioValue.Name = "tournamentCashRatioValue";
        tournamentCashRatioValue.ReadOnly = true;
        tournamentCashRatioValue.Size = new Size(156, 27);
        tournamentCashRatioValue.TabIndex = 12;
        tournamentCashRatioValue.TabStop = false;
        tournamentCashRatioValue.TextAlign = HorizontalAlignment.Right;
        // 
        // tournamentEntryFeeValue
        // 
        tournamentEntryFeeValue.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
        tournamentEntryFeeValue.Location = new Point(12, 106);
        tournamentEntryFeeValue.Name = "tournamentEntryFeeValue";
        tournamentEntryFeeValue.ReadOnly = true;
        tournamentEntryFeeValue.Size = new Size(156, 27);
        tournamentEntryFeeValue.TabIndex = 14;
        tournamentEntryFeeValue.TabStop = false;
        tournamentEntryFeeValue.TextAlign = HorizontalAlignment.Right;
        // 
        // tournamentEntryFeeLabel
        // 
        tournamentEntryFeeLabel.AutoSize = true;
        tournamentEntryFeeLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        tournamentEntryFeeLabel.Location = new Point(12, 86);
        tournamentEntryFeeLabel.Name = "tournamentEntryFeeLabel";
        tournamentEntryFeeLabel.Size = new Size(149, 17);
        tournamentEntryFeeLabel.TabIndex = 13;
        tournamentEntryFeeLabel.Text = "Tournament Entry Fee:";
        // 
        // Form
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(533, 390);
        Controls.Add(tournamentEntryFeeValue);
        Controls.Add(tournamentEntryFeeLabel);
        Controls.Add(tournamentCashRatioValue);
        Controls.Add(tournamentFinalsRatioValue);
        Controls.Add(tournamentCashRatioLabel);
        Controls.Add(tournamentFinalsRatioLabel);
        Controls.Add(disclaimerLabel);
        Controls.Add(newSquad);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Add Squad";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Button cancelButton;
    private Button saveButton;
    private Controls.SquadControl newSquad;
    private Label disclaimerLabel;
    private Label tournamentFinalsRatioLabel;
    private Label tournamentCashRatioLabel;
    private TextBox tournamentFinalsRatioValue;
    private TextBox tournamentCashRatioValue;
    private TextBox tournamentEntryFeeValue;
    private Label tournamentEntryFeeLabel;
}