using System.Globalization;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NortheastMegabuck;
internal abstract class ResultReportBase<TViewModel> : IDocument
{
    private readonly ICollection<TViewModel> _results;
    private readonly string _title;
    private readonly DateTime? _bowlDate;
    private readonly string? _division;
    private readonly byte[] _logo;

    protected ResultReportBase(string title, DateTime? bowlDate, string? division, ICollection<TViewModel> results)
    {
        _title = title;
        _bowlDate = bowlDate;
        _division = division;
        _results = results;

        _logo = new ImageConverter().ConvertTo(Properties.Resources.NMT_Header, typeof(byte[])) as byte[] ?? throw new InvalidOperationException("Cannot convert image to byte array");
    }

    public void Compose(IDocumentContainer container)
        => container
            .Page(page =>
            {
                page.Margin(35);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeBody);

                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });

    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Black);

        container.Row(row =>
        {
            row.RelativeItem().AlignMiddle().Column(column =>
            {
                column.Item().AlignCenter().Text(_title).Style(titleStyle);

                if (_bowlDate.HasValue)
                {
                    column.Spacing(10);

                    column.Item().AlignCenter().Text(text =>
                    {
                        text.Span("Date: ").SemiBold();
                        text.Span(_bowlDate.Value.ToString("MM/dd/yyyy - hh:mm tt", CultureInfo.CurrentCulture));
                    });
                }

                if (!string.IsNullOrWhiteSpace(_division))
                {
                    column.Spacing(10);

                    column.Item().AlignCenter().Text(text =>
                    {
                        text.Span("Division: ").SemiBold();
                        text.Span(_division);
                    });
                }
            });

            row.ConstantItem(250).Height(125).Image(_logo);
        });
    }

    private void ComposeBody(IContainer container)
        => container.PaddingVertical(10).Column(column =>
        {
            column.Spacing(5);

            column.Item().Element(ComposeTable);
        });

    private void ComposeTable(IContainer container)
        => container.Table(table =>
        {
            table.ColumnsDefinition(ComposeColumnDefinitionDescriptor);
            table.Header(ComposeHeaderDescriptor);

            PopulateTableData(_results, table);
        });

    protected abstract void PopulateTableData(ICollection<TViewModel> results, TableDescriptor table);

    protected abstract void ComposeColumnDefinitionDescriptor(TableColumnsDefinitionDescriptor columns);
    protected abstract void ComposeHeaderDescriptor(TableCellDescriptor header);

    protected static IContainer HeaderStyle(IContainer container)
        => container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);

    protected static IContainer CellStyle(IContainer container)
        => container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);

    protected static IContainer SpaceStyle(IContainer container)
        => container.PaddingVertical(15);

    public void GeneratePDF()
        => GeneratePDF(this);

    public static void GeneratePDF(IDocument document)
    {
#if DEBUG
        document.GeneratePdfAndShow();
#else
        using var saveFileDialog = new SaveFileDialog
        {
            Filter = "PDF Files (*.pdf)|*.pdf",
            FilterIndex = 1,
            RestoreDirectory = true,
            FileName = $"{_title}.pdf",
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        { 
            //save file
            using var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            document.GeneratePdf(fileStream);
        }
#endif
    }
}
