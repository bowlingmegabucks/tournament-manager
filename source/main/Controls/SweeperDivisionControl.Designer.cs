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
            this.nameText = new System.Windows.Forms.TextBox();
            this.nameLabel = new NewEnglandClassic.Controls.LabelControl();
            this.bonusPinsPerGameLabel = new NewEnglandClassic.Controls.LabelControl();
            this.bonusPinsPerGameValue = new NewEnglandClassic.Controls.NumericControl();
            this.sweeperDivisionErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bonusPinsPerGameValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sweeperDivisionErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // nameText
            // 
            this.nameText.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nameText.Location = new System.Drawing.Point(3, 28);
            this.nameText.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.nameText.Name = "nameText";
            this.nameText.PlaceholderText = "Division Description";
            this.nameText.ReadOnly = true;
            this.nameText.Size = new System.Drawing.Size(299, 27);
            this.nameText.TabIndex = 104;
            this.nameText.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Bold = false;
            this.nameLabel.Location = new System.Drawing.Point(3, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Required = false;
            this.nameLabel.Size = new System.Drawing.Size(117, 19);
            this.nameLabel.TabIndex = 105;
            this.nameLabel.TabStop = false;
            this.nameLabel.Text = "Name:";
            // 
            // bonusPinsPerGameLabel
            // 
            this.bonusPinsPerGameLabel.AutoSize = true;
            this.bonusPinsPerGameLabel.Bold = false;
            this.bonusPinsPerGameLabel.Location = new System.Drawing.Point(315, 3);
            this.bonusPinsPerGameLabel.Name = "bonusPinsPerGameLabel";
            this.bonusPinsPerGameLabel.Required = false;
            this.bonusPinsPerGameLabel.Size = new System.Drawing.Size(189, 19);
            this.bonusPinsPerGameLabel.TabIndex = 115;
            this.bonusPinsPerGameLabel.TabStop = false;
            this.bonusPinsPerGameLabel.Text = "Bonus Pins Per Game:";
            // 
            // bonusPinsPerGameValue
            // 
            this.bonusPinsPerGameValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bonusPinsPerGameValue.Location = new System.Drawing.Point(357, 28);
            this.bonusPinsPerGameValue.Margin = new System.Windows.Forms.Padding(3, 4, 30, 10);
            this.bonusPinsPerGameValue.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.bonusPinsPerGameValue.Name = "bonusPinsPerGameValue";
            this.bonusPinsPerGameValue.Size = new System.Drawing.Size(147, 27);
            this.bonusPinsPerGameValue.TabIndex = 114;
            this.bonusPinsPerGameValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bonusPinsPerGameValue.Validating += new System.ComponentModel.CancelEventHandler(this.BonusPinsPerGameValue_Validating);
            this.bonusPinsPerGameValue.Validated += new System.EventHandler(this.BonusPinsPerGameValue_Validated);
            // 
            // sweeperDivisionErrorProvider
            // 
            this.sweeperDivisionErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.sweeperDivisionErrorProvider.ContainerControl = this;
            // 
            // SweeperDivisionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bonusPinsPerGameLabel);
            this.Controls.Add(this.bonusPinsPerGameValue);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.nameLabel);
            this.Name = "SweeperDivisionControl";
            this.Size = new System.Drawing.Size(535, 65);
            ((System.ComponentModel.ISupportInitialize)(this.bonusPinsPerGameValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sweeperDivisionErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private TextBox nameText;
    private Controls.LabelControl nameLabel;
    private Controls.LabelControl bonusPinsPerGameLabel;
    private Controls.NumericControl bonusPinsPerGameValue;
    private ErrorProvider sweeperDivisionErrorProvider;
}
