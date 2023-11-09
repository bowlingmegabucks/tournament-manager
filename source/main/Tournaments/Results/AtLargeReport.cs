using System.Globalization;
using QuestPDF.Fluent;

namespace NortheastMegabuck.Tournaments.Results;
internal class AtLargeReport : ResultReportBase<IAtLargeViewModel>
{
    public AtLargeReport(string division, IList<IAtLargeViewModel> results)
        : base("Finals Seeding", null, division, results)
    { }

    protected override void ComposeColumnDefinitionDescriptor(TableColumnsDefinitionDescriptor columns)
    { 
        columns.ConstantColumn(40);
        columns.ConstantColumn(10);
        columns.RelativeColumn(8);
        columns.RelativeColumn();
        columns.RelativeColumn(1);
        columns.RelativeColumn(2);
    }

    protected override void ComposeHeaderDescriptor(TableCellDescriptor header)
    { 
        header.Cell().Element(HeaderStyle).Text("Place");
        header.Cell().Element(HeaderStyle).Text("Squad");
        header.Cell().Element(HeaderStyle).Text("Name");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("Total");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("HG");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("Casher");
    }

    protected override void PopulateTableData(ICollection<IAtLargeViewModel> results, TableDescriptor table)
    {
        var cashLogo = ConvertImage(Properties.Resources.DollarSign);

        foreach (var result in results)
        {
            table.Cell().Element(CellStyle).AlignMiddle().Text($"#{result.Place}");
            table.Cell().Element(CellStyle).AlignMiddle().Text($"#{result.SquadDate}");
            table.Cell().Element(CellStyle).AlignMiddle().Text(result.BowlerName);
            table.Cell().Element(CellStyle).AlignMiddle().AlignCenter().Text(result.Score.ToString(CultureInfo.CurrentCulture));
            table.Cell().Element(CellStyle).AlignMiddle().AlignCenter().Text(result.HighGame.ToString(CultureInfo.CurrentCulture));
            table.Cell().Height(50).Element(CellStyle).AlignCenter().ShowIf(result.PreviousCasher).Image(cashLogo).FitArea();
        }
    }
}
