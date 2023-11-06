using System.Globalization;
using QuestPDF.Fluent;

namespace NortheastMegabuck.Squads.Results;
internal class SquadResultReport : ResultReportBase<IViewModel>
{
    private readonly bool _handicap;

    internal SquadResultReport(DateTime bowlDate, string division, bool handicap, ICollection<IViewModel> results)
        : base("Squad Results", bowlDate, division, results)
    {
        _handicap = handicap;
    }

    protected override void ComposeColumnDefinitionDescriptor(TableColumnsDefinitionDescriptor columns)
    {
        columns.ConstantColumn(40);
        columns.RelativeColumn(10);

        if (_handicap)
        {
            columns.RelativeColumn();
        }

        columns.RelativeColumn();
        columns.RelativeColumn(1);
    }

    protected override void ComposeHeaderDescriptor(TableCellDescriptor header)
    {
        header.Cell().Element(HeaderStyle).Text("Place");
        header.Cell().Element(HeaderStyle).Text("Name");

        if (_handicap)
        {
            header.Cell().Element(HeaderStyle).AlignCenter().Text("HDCP");
        }

        header.Cell().Element(HeaderStyle).AlignCenter().Text("Total");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("HG");
    }

    protected override void PopulateTableData(ICollection<IViewModel> results, TableDescriptor table)
    { 
        var advancers = results.Where(result => result.Advancer).ToList();
        var cashers = results.Where(result => result.Casher);
        var nonCashers = results.Where(result => !(result.Casher || result.Advancer));

        foreach (var advancer in advancers)
        {
            MapRow(table, advancer);
        }

        table.Cell().ColumnSpan(2).Element(SpaceStyle).AlignLeft().Text(advancers.Count == 0 ? "No Advancers" : "Cut Line").Italic().FontSize(10);
        table.Cell().ColumnSpan(_handicap ? (uint)3 : 2).Element(SpaceStyle);

        foreach (var casher in cashers)
        {
            MapRow(table, casher);
        }

        table.Cell().ColumnSpan(2).Element(SpaceStyle).AlignLeft().Text("Cash Line").Italic().FontSize(10);
        table.Cell().ColumnSpan(_handicap ? (uint)3 : 2).Element(SpaceStyle);

        foreach (var nonCasher in nonCashers)
        {
            MapRow(table, nonCasher);
        }
    }

    private void MapRow(TableDescriptor table, IViewModel result)
    {
        table.Cell().Element(CellStyle).Text(result.Place.ToString(CultureInfo.CurrentCulture));
        table.Cell().Element(CellStyle).Text(result.BowlerName);

        if (_handicap)
        {
            table.Cell().Element(CellStyle).AlignCenter().Text(result.Handicap.ToString(CultureInfo.CurrentCulture));
        }

        table.Cell().Element(CellStyle).AlignCenter().Text(result.Score.ToString(CultureInfo.CurrentCulture));
        table.Cell().Element(CellStyle).AlignCenter().Text(result.HighGame.ToString(CultureInfo.CurrentCulture));
    }
}
