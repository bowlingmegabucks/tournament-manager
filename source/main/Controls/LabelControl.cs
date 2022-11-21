using System.ComponentModel;

namespace NortheastMegabuck.Controls;

public partial class LabelControl : UserControl
{
    /// <summary>
    ///
    /// </summary>
    public LabelControl()
    {
        InitializeComponent();
        TabStop = false;
    }

    //doing this for unit test to pass
    //when using PanelRequired.Visible directly not getting updated
    private bool _required;

    /// <summary>
    /// Show/hide required asterisk
    /// </summary>
    public bool Required
    {
        get => _required;
        set
        {
            _required = value;
            PanelRequired.Visible = value;
        }
    }

    /// <summary>
    /// Display text
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Bindable(true)]
    public override string Text
    {
        get => LabelText.Text;
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => LabelText.Text = value;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }

    /// <summary>
    ///
    /// </summary>
    public bool Bold
    {
        get => LabelText.Font.Bold;
        set
            => LabelText.Font = new Font(LabelText.Font, value ? FontStyle.Bold : FontStyle.Regular);
    }

    private void LabelText_TextChanged(object sender, System.EventArgs e)
        => SetSize();

    private void PanelRequired_VisibleChanged(object sender, System.EventArgs e)
        => SetSize();

    private void SetSize()
    {
        if (!AutoSize)
        {
            return;
        }

        LabelText.AutoSize = true;

        var labelWidth = LabelText.Width;

        PanelLabel.Width = labelWidth;

        var requiredWidth = Required ? PanelRequired.Width - 10 : 0;

        Width = labelWidth + requiredWidth;
    }
}
