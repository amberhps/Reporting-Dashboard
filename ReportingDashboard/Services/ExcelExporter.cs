using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;

namespace ReportingDashboard.Services
{
    public class ExcelExporter
    {
        public static void ExportToExcel<T>(IEnumerable<T> data, string fileName, string sheetName, string[] excludeProperties)
        {
            using var stream = File.Create(fileName);
            using var spreadsheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook);

            var workbookPart = spreadsheet.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = new Stylesheet()
            {
                Fonts = new Fonts(new Font()),
                Fills = new Fills(new Fill()),
                Borders = new Borders(new Border()),
                CellFormats = new CellFormats(
                    new CellFormat(),
                    new CellFormat { NumberFormatId = 14, ApplyNumberFormat = true },
                    new CellFormat { NumberFormatId = 2, ApplyNumberFormat = true }
                )
            };
            stylesPart.Stylesheet.Save();

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            var sheets = spreadsheet.WorkbookPart!.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet { Id = spreadsheet.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = sheetName };
            sheets.Append(sheet);

            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>()!;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).AsEnumerable();
            properties = properties.Where(x => !excludeProperties.Contains(x.Name));


            var headerRow = new Row();
            foreach (var prop in properties)
            {
                headerRow.Append(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(prop.Name)
                });
            }
            sheetData.Append(headerRow);

            foreach (var item in data)
            {
                var row = new Row();
                foreach (var prop in properties)
                {
                    var value = prop.GetValue(item);
                    var (cellValue, cellType, styleIndex) = GetCellValue(value);

                    row.Append(new Cell
                    {
                        CellValue = cellValue,
                        DataType = cellType,
                        StyleIndex = styleIndex
                    });
                }
                sheetData.Append(row);
            }

            workbookPart.Workbook.Save();
        }

        private static (CellValue value, CellValues cellType, UInt32 styleIndex) GetCellValue(object? value)
        {
            return value switch
            {
                decimal a => (new CellValue(a), CellValues.Number, 2),
                DateTime b => (new CellValue(b.ToOADate()), CellValues.Number, 1),
                _ => (new CellValue(value?.ToString() ?? ""), CellValues.String, 0)
            };
        }
    }
}
