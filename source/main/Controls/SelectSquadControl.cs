using System.ComponentModel;
using System.Runtime.Versioning;

namespace BowlingMegabucks.TournamentManager.Controls;

[SupportedOSPlatform("windows")]
internal sealed partial class SelectSquadControl 
    : UserControl, ISelectedIds
{
    public SelectSquadControl(SquadId id, string displayText, bool selected)
    {
        InitializeComponent();

        Id = id;
        DisplayText = displayText;
        Selected = selected;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SquadId Id { get; set; }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string DisplayText
    {
        get => nameCheckBox.Text;
        set => nameCheckBox.Text = value;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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