using System.Globalization;
using QuestPDF.Fluent;

namespace BowlingMegabucks.TournamentManager.Tournaments.Seeding;
internal class TournamentSeedingReport(string division, IList<IViewModel> results) : ResultReportBase<IViewModel>("Finals Seeding", null, division, results)
{
    protected override void ComposeColumnDefinitionDescriptor(TableColumnsDefinitionDescriptor columns)
    {
        columns.ConstantColumn(40);
        columns.RelativeColumn(10);
        columns.RelativeColumn();
        columns.RelativeColumn(1);
        columns.RelativeColumn(2);
    }

    protected override void ComposeHeaderDescriptor(TableCellDescriptor header)
    {
        header.Cell().Element(HeaderStyle).Text("Seed");
        header.Cell().Element(HeaderStyle).Text("Name");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("Total");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("HG");
        header.Cell().Element(HeaderStyle).AlignCenter().Text("Casher");
    }

    protected override void PopulateTableData(ICollection<IViewModel> results, TableDescriptor table)
    {
        var cashLogo = ConvertImage(Properties.Resources.DollarSign);

        var qualifiers = results.Where(result => result.Qualified);
        var nonQualifiers = results.Where(result => !result.Qualified);

        foreach (var qualifier in qualifiers)
        {
            MapRow(table, qualifier, cashLogo);
        }

        table.Cell().ColumnSpan(2).Element(SpaceStyle).AlignLeft().Text("Cut Line").Italic().FontSize(10);
        table.Cell().ColumnSpan(3).Element(SpaceStyle);

        foreach (var nonQualifier in nonQualifiers)
        {
            MapRow(table, nonQualifier, cashLogo);
        }
    }

    private static void MapRow(TableDescriptor table, IViewModel result, byte[] cashLogo)
    {
        table.Cell().Element(CellStyle).AlignMiddle().Text($"#{result.Seed}");
        table.Cell().Element(CellStyle).AlignMiddle().Text(result.BowlerName);
        table.Cell().Element(CellStyle).AlignMiddle().AlignCenter().Text(result.Score.ToString(CultureInfo.CurrentCulture));
        table.Cell().Element(CellStyle).AlignMiddle().AlignCenter().Text(result.HighGame.ToString(CultureInfo.CurrentCulture));
        table.Cell().Height(50).Element(CellStyle).AlignCenter().ShowIf(result.AtLargeCasher).Image(cashLogo).FitArea();
    }
}
