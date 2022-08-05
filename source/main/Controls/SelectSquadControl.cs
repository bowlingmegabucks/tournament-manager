
namespace NewEnglandClassic.Controls;
internal partial class SelectSquadControl : UserControl, ISelectedIds
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
        get => CheckboxName.Text;
        set => CheckboxName.Text = value;
    }

    public bool Selected
    {
        get => CheckboxName.Checked;
        set => CheckboxName.Checked = value;
    }
}

internal interface ISelectedIds
{
    SquadId Id { get; }

    bool Selected { get; }
}