
namespace NewEnglandClassic.Controls;
public partial class SelectSquadControl : UserControl, ISelectedIds
{
    public SelectSquadControl(SquadId id, string displayText, bool selected)
    {
        InitializeComponent();

        Id = id;
        DisplayText = displayText;
        Selected = selected;
    }

    public SquadId Id { get; set; }

    public string DisplayText
    {
        get => nameCheckBox.Text;
        set => nameCheckBox.Text = value;
    }

    public bool Selected
    {
        get => nameCheckBox.Checked;
        set => nameCheckBox.Checked = value;
    }
}

internal interface ISelectedIds
{
    SquadId Id { get; }

    bool Selected { get; }
}