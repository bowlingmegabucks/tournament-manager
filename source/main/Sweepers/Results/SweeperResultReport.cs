using System.Globalization;
using QuestPDF.Fluent;

namespace NortheastMegabuck.Sweepers.Results;
internal class SweeperResultReport : ResultReportBase<IViewModel>
{
    internal SweeperResultReport(string title, DateTime? bowlDate, ICollection<IViewModel> results)
        : base(title, bowlDate, results)
    { }

    protected override void ComposeColumnDefinitionDescriptor(TableColumnsDefinitionDescriptor columns)
    {
        columns.ConstantColumn(40);
        columns.RelativeColumn(10);
        columns.RelativeColumn();
        columns.RelativeColumn(1);
    }

    protected override void ComposeHeaderDescriptor(TableCellDescriptor header)
    {
        header.Cell().Element(HeaderStyle).Text("Place");
        header.Cell().Element(HeaderStyle).Text("Name");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("Total");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("HG");
    }

    protected override void PopulateTableData(ICollection<IViewModel> results, TableDescriptor table)
    {
        var cashers = results.Where(result => result.Casher);
        var nonCashers = results.Where(result => !result.Casher);

        foreach (var casher in cashers)
        { 
            table.Cell().Element(CellStyle).Text(casher.Place.ToString(CultureInfo.CurrentCulture));
            table.Cell().Element(CellStyle).Text(casher.BowlerName);
            table.Cell().Element(CellStyle).AlignCenter().Text(casher.Score.ToString(CultureInfo.CurrentCulture));
            table.Cell().Element(CellStyle).AlignCenter().Text(casher.HighGame.ToString(CultureInfo.CurrentCulture));
        }

        table.Cell().ColumnSpan(2).Element(SpaceStyle).AlignLeft().Text("Cash Line").Italic().FontSize(10);
        table.Cell().ColumnSpan(2).Element(SpaceStyle);

        foreach (var nonCasher in nonCashers)
        {
            table.Cell().Element(CellStyle).Text(nonCasher.Place.ToString(CultureInfo.CurrentCulture));
            table.Cell().Element(CellStyle).Text(nonCasher.BowlerName);
            table.Cell().Element(CellStyle).AlignCenter().Text(nonCasher.Score.ToString(CultureInfo.CurrentCulture));
            table.Cell().Element(CellStyle).AlignCenter().Text(nonCasher.HighGame.ToString(CultureInfo.CurrentCulture));
        }
    }
}
