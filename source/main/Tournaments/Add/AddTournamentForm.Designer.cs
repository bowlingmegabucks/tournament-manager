namespace BowlingMegabucks.TournamentManager.Tournaments.Add;

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
        newTournament = new Controls.TournamentControl();
        saveButton = new Button();
        cancelButton = new Button();
        SuspendLayout();
        // 
        // newTournament
        // 
        newTournament.BowlingCenter = "";
        newTournament.CashRatio = new decimal(new int[] { 0, 0, 0, 0 });
        newTournament.Completed = false;
        newTournament.End = new DateOnly(2023, 8, 21);
        newTournament.EntryFee = new decimal(new int[] { 0, 0, 0, 0 });
        newTournament.FinalsRatio = new decimal(new int[] { 0, 0, 0, 0 });
        newTournament.Games = 0;
        newTournament.Location = new Point(12, 12);
        newTournament.Name = "newTournament";
        newTournament.Size = new Size(354, 388);
        newTournament.Start = new DateOnly(2023, 8, 21);
        newTournament.SuperSweeperCashRatio = new decimal(new int[] { 0, 0, 0, 0 });
        newTournament.TabIndex = 0;
        newTournament.TournamentName = "";
        // 
        // saveButton
        // 
        saveButton.Location = new Point(282, 406);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(75, 23);
        saveButton.TabIndex = 1;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += SaveButton_Click;
        // 
        // cancelButton
        // 
        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(12, 406);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new Size(75, 23);
        cancelButton.TabIndex = 2;
        cancelButton.Text = "Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        // 
        // Form
        // 
        AcceptButton = saveButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        AutoValidate = AutoValidate.EnableAllowFocusChange;
        CancelButton = cancelButton;
        ClientSize = new Size(369, 441);
        Controls.Add(cancelButton);
        Controls.Add(saveButton);
        Controls.Add(newTournament);
        MaximizeBox = false;
        Name = "Form";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "New Tournament";
        ResumeLayout(false);
    }

    #endregion

    private Controls.TournamentControl newTournament;
    private Button saveButton;
    private Button cancelButton;
}
