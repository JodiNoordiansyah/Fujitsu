using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FujitsuWebApp.Helper
{
    public class SpreadsheetGenerator<T> where T : class
    {
        private readonly SpreadsheetConfig _config;
        private readonly IWorkbook _workbook;
        private readonly IDictionary<int, string> _fields;

        public SpreadsheetGenerator(SpreadsheetConfig config)
        {
            _config = config;

            if (_config == null) throw new ArgumentNullException(nameof(config));
            if (_config.Extension != "xls" && _config.Extension != "xlsx") throw new ArgumentException("Filename must be in an excel format");

            if (_config.Extension == "xlsx") _workbook = new XSSFWorkbook();
            else if (_config.Extension == "xls") _workbook = new HSSFWorkbook();

            _fields = PopulateFields();
        }

        public SpreadsheetConfig Config
        {
            get { return _config; }
        }

        private static ICellStyle GetHeaderCellStyle(IWorkbook workbook)
        {
            var headerFont = workbook.CreateFont();
            headerFont.FontHeightInPoints = 11;
            headerFont.FontName = "Times New Roman";
            headerFont.Boldweight = (short)FontBoldWeight.Bold;
            headerFont.Color = IndexedColors.White.Index;

            var headerStyle = workbook.CreateCellStyle();
            headerStyle.FillForegroundColor = IndexedColors.BlueGrey.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.Alignment = HorizontalAlignment.Center;
            headerStyle.SetFont(headerFont);
            headerStyle.BorderTop = BorderStyle.Thin;
            headerStyle.BorderLeft = BorderStyle.Thin;
            headerStyle.BorderBottom = BorderStyle.Thin;
            headerStyle.BorderRight = BorderStyle.Thin;

            return headerStyle;
        }

        private static ICellStyle GetDataCellStyle(IWorkbook workbook)
        {
            var rowFont = workbook.CreateFont();
            rowFont.FontHeightInPoints = 10;
            rowFont.FontName = "Times New Roman";

            var rowStyle = workbook.CreateCellStyle();
            rowStyle.SetFont(rowFont);
            rowStyle.WrapText = true;
            rowStyle.VerticalAlignment = VerticalAlignment.Top;
            rowStyle.BorderTop = BorderStyle.Thin;
            rowStyle.BorderLeft = BorderStyle.Thin;
            rowStyle.BorderBottom = BorderStyle.Thin;
            rowStyle.BorderRight = BorderStyle.Thin;

            return rowStyle;
        }

        public byte[] Generate(IEnumerable<T> data)
        {
            var sheet = _workbook.CreateSheet(_config.SheetName);

            SetColumnWidth(sheet);
            PopulateHeader(sheet);
            PopulateData(sheet, data);

            using var stream = new MemoryStream();
            _workbook.Write(stream);
            return stream.ToArray();
        }

        private void SetColumnWidth(ISheet sheet)
        {
            var columnIndex = _config.StartColumn;
            foreach (var keyValuePair in _fields)
            {
                var property = typeof(T).GetProperty(keyValuePair.Value);
                if (property == null) continue;

                var cellWidth = 0;
                var propAttribute = SpreadsheetConfig.GetAttribute(property);
                if (propAttribute != null) cellWidth = propAttribute.ColumnWidth;

                sheet.SetColumnWidth(columnIndex, cellWidth);

                columnIndex++;
            }
        }

        private void PopulateHeader(ISheet sheet)
        {
            var headerRow = sheet.CreateRow(_config.StartRow);
            var headerStyle = GetHeaderCellStyle(_workbook);

            var columnIndex = _config.StartColumn;
            foreach (var keyValuePair in _fields)
            {
                var property = typeof(T).GetProperty(keyValuePair.Value);
                if (property == null) continue;

                var cellValue = string.Empty;
                var propAttribute = SpreadsheetConfig.GetAttribute(property);
                if (propAttribute != null) cellValue = propAttribute.DisplayName;

                var headerCell = headerRow.CreateCell(columnIndex);
                headerCell.SetCellValue(cellValue);
                headerCell.CellStyle = headerStyle;
                columnIndex++;
            }
        }

        private void PopulateData(ISheet sheet, IEnumerable<T> data)
        {
            var rowIndex = _config.StartRow + 1;
            var dataStyle = GetDataCellStyle(_workbook);

            foreach (T item in data)
            {
                var row = sheet.CreateRow(rowIndex);

                var columnIndex = _config.StartColumn;
                foreach (var keyValuePair in _fields)
                {
                    var property = typeof(T).GetProperty(keyValuePair.Value);
                    var propAttribute = SpreadsheetConfig.GetAttribute(property);

                    if (property == null) continue;

                    var dataCell = row.CreateCell(columnIndex);
                    dataCell.CellStyle = dataStyle;

                    if (property.PropertyType == typeof(bool))
                    {
                        dataCell.SetCellValue((bool)property.GetValue(item) ? "Yes" : "No");
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        var date = (DateTime?)property.GetValue(item);
                        if (date.HasValue && !date.Value.Equals(DateTime.MinValue) && !date.Value.Equals(new DateTime(1900, 1, 1)))
                        {
                            dataCell.SetCellValue(date.Value.ToString("dd-MMM-yyyy"));
                        }
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        dataCell.SetCellValue((double)property.GetValue(item));
                    }
                    else if (property.PropertyType == typeof(decimal))
                    {
                        var value = (decimal)property.GetValue(item);

                        if (string.IsNullOrEmpty(propAttribute.MoneyCurrency))
                        {
                            var dataValue = decimal.ToDouble(value);
                            dataCell.SetCellValue(dataValue);
                        }
                        else
                        {
                            CultureInfo ci = new CultureInfo(propAttribute.MoneyCurrency);
                            var dataValue = string.Format(ci, "{0:C}", value);
                            dataCell.SetCellValue(dataValue);
                        }
                    }
                    else if (property.PropertyType == typeof(int))
                    {
                        var value = property.GetValue(item);
                        dataCell.SetCellValue(Convert.ToInt32(value));
                    }
                    else
                    {
                        dataCell.SetCellValue(property.GetValue(item)?.ToString());
                    }

                    columnIndex++;
                }
                rowIndex++;
            }
        }

        private SortedDictionary<int, string> PopulateFields()
        {
            var fields = new SortedDictionary<int, string>();

            foreach (var property in typeof(T).GetProperties())
            {
                var propAttribute = SpreadsheetConfig.GetAttribute(property);
                if (propAttribute != null)
                {
                    fields.Add(propAttribute.Order, property.Name);
                }
            }

            return fields;
        }
    }
}
