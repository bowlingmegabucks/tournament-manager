using System.Drawing.Printing;
using System.Globalization;
using NortheastMegabuck.Properties;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NortheastMegabuck;

#if WINDOWS
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

        _logo = ConvertImage(Properties.Resources.NMT_Header);
    }

    private readonly ImageConverter _imageConverter = new();

    protected byte[] ConvertImage(Bitmap image)
        => _imageConverter.ConvertTo(image, typeof(byte[])) as byte[] ?? throw new InvalidOperationException("Cannot convert image to byte array");

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

            row.ConstantItem(300).Height(125).Image(_logo);
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
        => GeneratePDF(this, _title);

    public static void GeneratePDF(IDocument document, string title)
    {
#if DEBUG
        document.GeneratePdfAndShow();
#else
        using var saveFileDialog = new SaveFileDialog
        {
            Filter = Resources.PDF_File_Type,
            FilterIndex = 1,
            RestoreDirectory = true,
            FileName = $"{title}.pdf",
        };

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            //save file
            using var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            document.GeneratePdf(fileStream);
        }
#endif
    }

    public void Print()
        => Print(this);

    public static void Print(IDocument document)
    {
        //display print dialog
        using var printDialog = new PrintDialog
        {
            AllowSomePages = true,
            AllowSelection = false,
            AllowCurrentPage = true,
            AllowPrintToFile = false,
            ShowHelp = false,
            ShowNetwork = false,
            UseEXDialog = true,
        };

        if (printDialog.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        var imageConverter = new ImageConverter();

        var images = document.GenerateImages(new ImageGenerationSettings { ImageFormat = ImageFormat.Png, RasterDpi = 90 }).Select(x => imageConverter.ConvertFrom(x) as System.Drawing.Image).ToList();

        var counter = 0;

        //create print document
        using var printDocument = new PrintDocument();

        printDocument.PrintPage += (sender, args) =>
        {
            args.Graphics!.DrawImage(images[counter]!, 0, 0);
            counter++;
            args.HasMorePages = counter != images.Count;
        };

        printDocument.Print();
    }
}
#endif