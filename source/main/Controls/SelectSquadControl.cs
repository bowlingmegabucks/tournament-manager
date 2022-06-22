
namespace NewEnglandClassic.Controls;
public partial class SelectSquadControl : UserControl, ISelectedIds
{
    public SelectSquadControl(Guid id, string displayText, bool selected)
    {
        InitializeComponent();

        Id = id;
        DisplayText = displayText;
        Selected = selected;
    }

    public Guid Id { get; set; }

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

public interface ISelectedIds
{
    Guid Id { get; }

    bool Selected { get; }
}