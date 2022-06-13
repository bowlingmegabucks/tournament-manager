namespace NewEnglandClassic.Contols;

partial class SweeperDivisionControl
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
            this.components = new System.ComponentModel.Container();
            this.TextboxDivisionName = new System.Windows.Forms.TextBox();
            this.LabelName = new NewEnglandClassic.Controls.LabelControl();
            this.LabelMaximumHandicapPerGame = new NewEnglandClassic.Controls.LabelControl();
            this.NumericBonusPinsPerGame = new NewEnglandClassic.Controls.NumericControl();
            this.ErrorProviderSweeperDivision = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.NumericBonusPinsPerGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSweeperDivision)).BeginInit();
            this.SuspendLayout();
            // 
            // TextboxDivisionName
            // 
            this.TextboxDivisionName.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TextboxDivisionName.Location = new System.Drawing.Point(3, 28);
            this.TextboxDivisionName.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.TextboxDivisionName.Name = "TextboxDivisionName";
            this.TextboxDivisionName.PlaceholderText = "Division Description";
            this.TextboxDivisionName.ReadOnly = true;
            this.TextboxDivisionName.Size = new System.Drawing.Size(299, 27);
            this.TextboxDivisionName.TabIndex = 104;
            this.TextboxDivisionName.TabStop = false;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Bold = false;
            this.LabelName.Location = new System.Drawing.Point(3, 3);
            this.LabelName.Name = "LabelName";
            this.LabelName.Required = false;
            this.LabelName.Size = new System.Drawing.Size(117, 19);
            this.LabelName.TabIndex = 105;
            this.LabelName.TabStop = false;
            this.LabelName.Text = "Name:";
            // 
            // LabelMaximumHandicapPerGame
            // 
            this.LabelMaximumHandicapPerGame.AutoSize = true;
            this.LabelMaximumHandicapPerGame.Bold = false;
            this.LabelMaximumHandicapPerGame.Location = new System.Drawing.Point(315, 3);
            this.LabelMaximumHandicapPerGame.Name = "LabelMaximumHandicapPerGame";
            this.LabelMaximumHandicapPerGame.Required = false;
            this.LabelMaximumHandicapPerGame.Size = new System.Drawing.Size(189, 19);
            this.LabelMaximumHandicapPerGame.TabIndex = 115;
            this.LabelMaximumHandicapPerGame.TabStop = false;
            this.LabelMaximumHandicapPerGame.Text = "Bonus Pins Per Game:";
            // 
            // NumericBonusPinsPerGame
            // 
            this.NumericBonusPinsPerGame.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.NumericBonusPinsPerGame.Location = new System.Drawing.Point(357, 28);
            this.NumericBonusPinsPerGame.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.NumericBonusPinsPerGame.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.NumericBonusPinsPerGame.Name = "NumericBonusPinsPerGame";
            this.NumericBonusPinsPerGame.Size = new System.Drawing.Size(147, 27);
            this.NumericBonusPinsPerGame.TabIndex = 114;
            this.NumericBonusPinsPerGame.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NumericBonusPinsPerGame.Validating += new System.ComponentModel.CancelEventHandler(this.NumericBonusPinsPerGame_Validating);
            this.NumericBonusPinsPerGame.Validated += new System.EventHandler(this.NumericBonusPinsPerGame_Validated);
            // 
            // ErrorProviderSweeperDivision
            // 
            this.ErrorProviderSweeperDivision.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProviderSweeperDivision.ContainerControl = this;
            // 
            // SweeperDivisionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LabelMaximumHandicapPerGame);
            this.Controls.Add(this.NumericBonusPinsPerGame);
            this.Controls.Add(this.TextboxDivisionName);
            this.Controls.Add(this.LabelName);
            this.Name = "SweeperDivisionControl";
            this.Size = new System.Drawing.Size(535, 65);
            ((System.ComponentModel.ISupportInitialize)(this.NumericBonusPinsPerGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProviderSweeperDivision)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox TextboxDivisionName;
    private Controls.LabelControl LabelName;
    private Controls.LabelControl LabelMaximumHandicapPerGame;
    private Controls.NumericControl NumericBonusPinsPerGame;
    private ErrorProvider ErrorProviderSweeperDivision;
}
